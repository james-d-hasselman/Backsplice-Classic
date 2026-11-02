# 0002. Use the OpenXML SDK to Sort Doubleknot Rosters

**Status:** Accepted

**Date:** 2026-10-27

**Addresses:** [R2](../architecture.md#111-risks). Partly addresses [R27](../architecture.md#111-risks) (sorting only), [TD1](../architecture.md#112-technical-debt) (sorting tests),
[TD2](../architecture.md#112-technical-debt) (the description column in Create Paperwork), [TD4](../architecture.md#112-technical-debt) (sorting moved out of
the form), and [TD5](../architecture.md#112-technical-debt) (sorting no longer needs Excel).

**Supersedes:** Baseline [9.3](../architecture.md#93-use-the-openxml-sdk-for-spreadsheet-files), in part: keeping Excel COM Interop for sorting.

## Contents

- [Context](#context)
- [Decision](#decision)
- [Considered Options](#considered-options)
- [Consequences](#consequences)
- [Done When](#done-when)
- [Documentation Updates](#documentation-updates)

## Context

[↑ Contents](#contents)

Backsplice sorts the week's Doubleknot roster copy using Excel via COM Interop. The sorting logic lives in the SortRoster method of the CreatePaperwork form. The rows in the roster are sorted in ascending order by the value in column D (description), case insensitive. Sorting incorrectly treats row 2 of the roster as a header row and leaves it in place. If the offering in row 2 is not the first alphabetically then the paperwork for that offering will be missing a scout ([R2](../architecture.md#111-risks)).

The main feature of Backsplice is Create Paperwork (baseline [6.1](../architecture.md#61-create-paperwork)). Create Paperwork requires a sorted Doubleknot roster and sorting requires Excel. Create Paperwork cannot run at all without Excel. The inability to test Create Paperwork automatically makes it difficult to make any architectural changes since testing must be done manually.

A replacement sorting implementation must achieve the following:

* Have no dependency on Excel.
* Put all rows for the same offering next to each other. Grouping in Create Paperwork and Add Scout depends on it.
* Keep those rows in their original order. When SortScouts is off, paperwork lists scouts in the order in which they appear in the Doubleknot roster.
* Sort all registrations in alphabetical order by description, ignoring case. The Add Scout program list shows offerings in this order.
* Write the sorted Doubleknot roster copy back to disk. Add Scout uses the sorted roster copy to populate a list of program offerings with the number of scouts currently registered for each offering, and Add relies on the sorted roster copy to determine the correct location to insert the new registration.
* Row 1 of the Doubleknot roster is excluded from sorting. Row 1 is the header row, not a registration. The current implementation incorrectly excluded both row 1 and row 2 ([R2](../architecture.md#111-risks)).

## Decision

[↑ Contents](#contents)

**Where the sort lives.**

Sorting lives in the Sort method of the DoubleKnotRoster class. DoubleKnotRoster already knows the file format and uses the OpenXML SDK. Create Paperwork's separate definition of the description column is removed ([TD2](../architecture.md#112-technical-debt)).

**What it sorts.**

The Sort method leaves the header in row 1 of the roster in place and sorts the remaining rows from row 2 until the end of the roster. Row 2 is correctly treated as a registration and the scout in row 2 is never missing from the paperwork due to sorting ([R2](../architecture.md#111-risks)).

**Ordering rules.**

* Registrations are sorted in alphabetical order by description, case insensitive.
* Registrations with the same description maintain their original order relative to one another (stable sort).

**How Create Paperwork uses it.**

To sort a roster using the new implementation, Create Paperwork opens the roster in editable mode, calls Sort, calls Dispose which saves the changes back to the file, and then reopens the roster in read-only mode. This order of operations was chosen so that everything after the sort continues to work as it does today. The SortRoster method on the CreatePaperwork class is removed because this change reduces it to one line that calls the Sort method creating needless complexity.

## Considered Options

[↑ Contents](#contents)

* **Separate roster sorter**: Introduce a new class responsible for sorting rosters. This option was not chosen because it simply moves [TD2](../architecture.md#112-technical-debt) to a new class instead of addressing the problem. Without changes to DoubleKnotRoster’s interface, the sorter would have to read and write rows itself, making it a fifth piece of code with its own knowledge of the roster’s layout.
* **Registration based interface**: Convert the DoubleKnotRoster class to have an interface that operates on registrations instead of rows and columns. This interface would have an enumerator that returns Registration objects, an Add(Registration) method, and a Remove(Registration) method. Sorting would either be done internally by the enumerator (the enumerator would return the registrations in sorted order, which would also remove the need to save the sorted roster), or by the RosterSorter class. Adding a RosterSorter doesn't have the same problems in this case because the Registration based interface allows it to operate without any knowledge of the roster format. This solution wasn't chosen because changing DoubleKnotRoster's interface in this way would cause changes in nearly every other feature and not only sorting. A registration based interface is desirable eventually and that work will be left to a later ADR.
* **Excel sort behind an interface**: Introduce an interface that hides the dependency on Excel by allowing a test fake to be introduced. This option would have enabled testing Create Paperwork with the exception of sorting. This solution wasn't selected because it leaves sorting untestable without Excel and [R2](../architecture.md#111-risks) remains unfixed.
* **Order by program and period**: Sort registrations first alphabetically by program name and secondly by offering period. This is the ideal ordering. Sorting case insensitively by description is naive and only coincidentally sorts periods in chronological order. Doing this properly means using the same period rule as Find ("all" periods first, then numbered periods in ascending order, then everything else), and breaking ties between different offerings that the rule treats as the same period, such as "period 1" and "Periods 1 & 2". This wasn't selected because the scope of this ADR is to remove sorting from Excel, not to change how sorting works.

## Consequences

[↑ Contents](#contents)

* Create paperwork can be run without Excel
* Tests can be written for sorting a Doubleknot roster
* [R2](../architecture.md#111-risks) is fixed
* Create Paperwork's hard coding of column D is removed
* A failed sort no longer leaves Excel running since sorting no longer uses Excel
* A failed print may still leave Excel running ([R27](../architecture.md#111-risks)) since printing still uses Excel
* Feature logic still exists within the forms ([TD4](../architecture.md#112-technical-debt))
* Most of Backsplice is still uncovered by tests ([TD1](../architecture.md#112-technical-debt))
* Printing still requires Excel ([TD5](../architecture.md#112-technical-debt))
* The ordering of registrations in the sorted roster remains unchanged
* Create Paperwork opens the roster twice, once in write mode to sort it, and then again in read mode to create paperwork
* Backsplice assumes responsibility for rewriting and renumbering the rows during sorting, a responsibility that used to belong to Excel

## Done When

[↑ Contents](#contents)

### Tests Pass

* A registration in row 2 that has a description that is not first alphabetically is correctly sorted
* Registrations are sorted in alphabetical order by description, case insensitive
* Registrations with the same description maintain their original order relative to one another (stable sort)
* Sorting does not modify a roster that is already sorted
* Sorting does not modify a roster that contains only a header
* Sorting does not modify a roster that contains only a single registration
* Sorting does not move the header row (row 1)
* Sorting moves whole rows together
* A sorted roster remains readable
* Calling Dispose on a sorted roster saves the sorted order
* Running Create Paperwork with a previous year's roster produces the same output as before, when run with settings matching the original run, checked by a script, run locally, that compares the scouts in each paperwork file

### Code Changed

* The SortRoster method is removed from CreatePaperwork
* DoubleKnotRoster gains a new method called Sort
* References to Excel are removed from CreatePaperwork
* Create Paperwork opens the roster in editable mode, calls Sort, calls Dispose, and then reopens the roster in read-only mode

### Documentation Updated

* The documentation updates below are made.

## Documentation Updates

[↑ Contents](#contents)

* [3](../architecture.md#3-context-and-scope): remove "sort rosters" from the arrow pointing to MicrosoftExcel
* [5](../architecture.md#5-building-block-view): remove the arrow pointing from CreatePaperwork to MicrosoftExcel
* [5](../architecture.md#5-building-block-view): remove "to sort the roster copy and" from the Microsoft Excel bullet
* [6.1](../architecture.md#61-create-paperwork): in the note at the top of the sequence diagram, keep the copy of the roster to the Week N folder and remove the SortRoster step; add explicit messages showing the new sequence
* [7.1](../architecture.md#71-infrastructure): remove "sort, " from the note on the arrow pointing to Microsoft Excel
* [7.1](../architecture.md#71-infrastructure): remove "to sort the roster copy and " from the description of the Microsoft Excel element
* [7.4](../architecture.md#74-requirements-on-the-computer): remove references to Create Paperwork and sorting from the bullet naming Excel as a requirement
* [8.2](../architecture.md#82-spreadsheet-access) (Excel COM Interop): remove the parts about Create Paperwork and sorting
* [8.6](../architecture.md#86-ordering) (Create Paperwork): remove "using Excel" and the sentence about row 2 being treated as a header ([R2](../architecture.md#111-risks))
* [9.3](../architecture.md#93-use-the-openxml-sdk-for-spreadsheet-files): change the status to "Accepted, November 2010; partly superseded by ADR 0002 (sorting)"
* [11.1](../architecture.md#111-risks) ([R2](../architecture.md#111-risks)) : remove
* [11.1](../architecture.md#111-risks) ([R27](../architecture.md#111-risks)): remove sorting
* [11.2](../architecture.md#112-technical-debt) ([TD1](../architecture.md#112-technical-debt)): remove references to [R2](../architecture.md#111-risks) and sorting
* [11.2](../architecture.md#112-technical-debt) ([TD2](../architecture.md#112-technical-debt)) remove the last sentence
* [11.2](../architecture.md#112-technical-debt) ([TD5](../architecture.md#112-technical-debt)) remove references to Create Paperwork and sorting