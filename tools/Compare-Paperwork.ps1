<#
.SYNOPSIS
  Compares the scouts in every paperwork file of two Backsplice year folders.
.EXAMPLE
  .\Compare-Paperwork.ps1 -Expected D:\before\2013 -Actual D:\after\2013
#>
param(
    [Parameter(Mandatory)][string]$Expected,   # output of the original run
    [Parameter(Mandatory)][string]$Actual,     # output of Create Paperwork after ADR 0002
    [string]$OpenXmlDll = "$PSScriptRoot\..\src\Backsplice\bin\Debug\DocumentFormat.OpenXml.dll"
)

Add-Type -AssemblyName WindowsBase
Add-Type -Path $OpenXmlDll
$ss = 'DocumentFormat.OpenXml.Spreadsheet'

function Get-CellText($cell, $sst) {
    if ($cell.DataType -and $cell.DataType.HasValue -and
        $cell.DataType.Value -eq [DocumentFormat.OpenXml.Spreadsheet.CellValues]::SharedString) {
        return $sst.ChildElements[[int]$cell.CellValue.Text].InnerText.Trim()
    }
    if ($cell.InlineString) { return $cell.InlineString.InnerText.Trim() }
    if ($cell.CellValue)    { return $cell.CellValue.Text.Trim() }
    return $null
}

function Get-Scouts([string]$path) {
    $doc = [DocumentFormat.OpenXml.Packaging.SpreadsheetDocument]::Open($path, $false)
    try {
        $wb  = $doc.WorkbookPart
        $sst = if ($wb.SharedStringTablePart) { $wb.SharedStringTablePart.SharedStringTable } else { $null }
        $scouts = New-Object 'System.Collections.Generic.List[string]'
        $min = 13; $max = 31          # paperwork template: rows 13-31 on every sheet
        $sheetNo = 0

        foreach ($sheet in $wb.Workbook.Sheets.ChildElements) {
            $part = $wb.GetPartById($sheet.Id.Value)
            foreach ($row in $part.Worksheet.SheetData.ChildElements) {
                $n = [int]$row.RowIndex.Value
                $name = $null; $troop = $null
                foreach ($cell in $row.ChildElements) {
                    $col = ($cell.CellReference.Value -replace '\d', '')
                    if ($col -eq 'A') { $name  = Get-CellText $cell $sst }
                    if ($col -eq 'B') { $troop = Get-CellText $cell $sst }
                }
                # A plain spreadsheet has Name/Troop in row 1 and scouts from row 2
                if ($sheetNo -eq 0 -and $n -eq 1 -and $name -eq 'Name' -and $troop -eq 'Troop') {
                    $min = 2; $max = [int]::MaxValue
                }
                if ($n -lt $min -or $n -gt $max -or [string]::IsNullOrEmpty($name)) { continue }
                $scouts.Add("$name | $troop")
            }
            $sheetNo++
        }
        return ,$scouts
    }
    finally { $doc.Dispose() }
}

# Paperwork files are Week N\<Program>\<file>.xlsx (the roster copy sits directly in Week N).
function Get-Paperwork([string]$root) {
    $root = (Resolve-Path $root).Path.TrimEnd('\')
    Get-ChildItem $root -Recurse -File -Filter *.xlsx |
        ForEach-Object { $_.FullName.Substring($root.Length + 1) } |
        Where-Object { ($_ -split '\\').Count -ge 3 }
}

$expectedFiles = @(Get-Paperwork $Expected)
$actualFiles   = @(Get-Paperwork $Actual)
$failures = 0

foreach ($f in ($expectedFiles | Where-Object { $actualFiles -notcontains $_ })) {
    Write-Host "MISSING  $f" -ForegroundColor Red; $failures++
}
foreach ($f in ($actualFiles | Where-Object { $expectedFiles -notcontains $_ })) {
    Write-Host "EXTRA    $f" -ForegroundColor Red; $failures++
}

foreach ($f in ($expectedFiles | Where-Object { $actualFiles -contains $_ })) {
    $a = Get-Scouts (Join-Path $Expected $f)
    $b = Get-Scouts (Join-Path $Actual   $f)

    if (($a -join "`n") -ceq ($b -join "`n")) { continue }

    $failures++
    Write-Host "DIFFERS  $f  (expected $($a.Count) scouts, actual $($b.Count))" -ForegroundColor Red
    $limit = [Math]::Max($a.Count, $b.Count)
    for ($i = 0; $i -lt $limit; $i++) {
        $x = if ($i -lt $a.Count) { $a[$i] } else { '<none>' }
        $y = if ($i -lt $b.Count) { $b[$i] } else { '<none>' }
        if ($x -cne $y) { Write-Host "         row $($i + 1): expected '$x', actual '$y'"; break }
    }
}

Write-Host ""
Write-Host "Compared $($expectedFiles.Count) expected file(s); $failures difference(s)."
exit ([int]($failures -gt 0))