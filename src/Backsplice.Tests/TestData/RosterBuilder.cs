using Bogus;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backsplice.Tests.TestData
{
    internal class RosterBuilder
    {
        private readonly List<string[]> rows = new List<string[]>();
        private readonly Faker faker = new Faker();

        public RosterBuilder UseSeed(int seed) { faker.Random = new Randomizer(seed); return this; }

        // A registration for the given offering; values not supplied are generated.
        public RosterBuilder Add(string description, string lastName = null,
                                 string firstName = null, string group = null)
        {
            rows.Add(new[]
            {
                lastName ?? faker.Name.LastName(),
                firstName ?? faker.Name.FirstName(),
                group ?? $"Troop {faker.Random.Number(1, 9999)}, {faker.Random.String2(4).ToUpper()} ({faker.Random.Number(1000000, 9999999)})",
                description,
            });
            return this;
        }

        // A row exactly as given, for unusual layouts; null leaves the cell out.
        public RosterBuilder AddRaw(params string[] cells) { rows.Add(cells); return this; }

        public MemoryStream BuildStream()
        {
            var stream = new MemoryStream();
            using (var doubleknotRoster = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                // Create the workbook, one worksheet, and a shared string table.
                // Write the header row, then each row in `rows` as row 2, 3, ...,
                // adding each non-null value to the shared string table and
                // skipping null cells entirely.
                // Create the file

                var sheetData = new SheetData();
                var sharedStringTable = new SharedStringTable();
                var cellValueBuilder = new CellValueBuilder(sharedStringTable);

                // Add data to the workbook beginning with the header
                var headerRow = new Row(new List<OpenXmlElement> {
                    new Cell
                    {
                        CellReference = "A1",
                        CellValue = cellValueBuilder.Build("Name"),
                        DataType = CellValues.SharedString
                    },
                    new Cell
                    {
                        CellReference = "B1",
                        CellValue = cellValueBuilder.Build("Troop"),
                        DataType = CellValues.SharedString
                    },
                    new Cell
                    {
                        CellReference = "C1",
                        CellValue = cellValueBuilder.Build("Group Name (Registration)"),
                        DataType = CellValues.SharedString
                    },
                    new Cell
                    {
                        CellReference = "D1",
                        CellValue = cellValueBuilder.Build("Description"),
                        DataType = CellValues.SharedString
                    }
                })
                { RowIndex = 1 };
                sheetData.AppendChild(headerRow);

                uint rowIndex = 2;
                string[] columnLetters = { "A", "B", "C", "D" };
                foreach (var row in rows)
                {
                    var rosterRow = new Row()
                    { RowIndex = rowIndex };
                    sheetData.AppendChild(rosterRow);

                    uint columnIndex = 0;
                    foreach (var cellValue in row)
                    {
                        if (cellValue == null)
                        {
                            columnIndex++;
                            continue;
                        }

                        var columnLetter = columnLetters[columnIndex];
                        rosterRow.AppendChild(
                            new Cell
                            {
                                CellReference = columnLetter + rowIndex,
                                CellValue = cellValueBuilder.Build(cellValue),
                                DataType = CellValues.SharedString
                            }
                        );

                        columnIndex++;
                    }
                    rowIndex++;
                }
                var columns = new Columns(
                    new Column { Min = 1, Max = 1 },
                    new Column { Min = 2, Max = 2 },
                    new Column { Min = 3, Max = 3 },
                    new Column { Min = 4, Max = 4 });
                var worksheet = new Worksheet(columns, sheetData);
                var workbook = new Workbook { FileVersion = new FileVersion { ApplicationName = "Microsoft Office Excel" } };
                var workbookPart = doubleknotRoster.AddWorkbookPart();
                workbookPart.Workbook = workbook;
                var sharedStringTablePart = workbookPart.AddNewPart<SharedStringTablePart>();
                sharedStringTablePart.SharedStringTable = sharedStringTable;
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = worksheet;

                var worksheetPartId = workbookPart.GetIdOfPart(worksheetPart);
                Sheets sheets = new Sheets();
                Sheet sheet = new Sheet { Name = "DoubleknotExport", SheetId = 1, Id = worksheetPartId };
                sheets.Append(sheet);
                workbook.Append(sheets);

                worksheet.Save();
                doubleknotRoster.Save();
            }

            stream.Position = 0;
            return stream;
        }

        private class CellValueBuilder
        {
            private SharedStringTable sharedStringTable;
            private Dictionary<string, string> indexTable;

            public CellValueBuilder(SharedStringTable sharedStringTable)
            {
                this.sharedStringTable = sharedStringTable;
                indexTable = new Dictionary<string, string>();
            }

            public CellValue Build(string value)
            {
                if (!indexTable.ContainsKey(value))
                {
                    indexTable.Add(value, sharedStringTable.Elements<SharedStringItem>().Count().ToString());
                    sharedStringTable.AppendChild(new SharedStringItem(new Text(value)));
                }

                return new CellValue(indexTable[value]);
            }
        }
    }
}
