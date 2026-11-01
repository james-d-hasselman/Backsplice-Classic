# 0001. Testing Approach

**Status:** Accepted

**Date:** 2026-10-18

**Implemented:** 2026-10-25

**Addresses:** [TD1](../architecture.md#112-technical-debt) (no automated tests). Prepares for the
redesign ADRs that follow, which rely on these tests.

## Contents

- [Context](#context)
- [Decision](#decision)
- [Considered Options](#considered-options)
- [Consequences](#consequences)
- [Done When](#done-when)
- [Documentation Updates](#documentation-updates)

## Context

[↑ Contents](#contents)

Backsplice has no working tests ([TD1](../architecture.md#112-technical-debt)). The UnitTests project
contains one empty test method and references the Visual Studio 2010 test
framework (`Microsoft.VisualStudio.QualityTools.UnitTestFramework`), which
does not run with current tooling.

The redesign will change code that has no tests, so tests are needed first:
to confirm the problems listed in baseline section [11](../architecture.md#11-risks-and-technical-debt), to pin down existing
behavior that must not change, and to specify new interfaces.

Several things make Backsplice hard to test today:

- The wrapper classes (DoubleKnotRoster, ProgramPaperwork, ExcelSpreadsheet)
  open files by path, so every test needs a real `.xlsx` file (baseline [8.2](../architecture.md#82-spreadsheet-access)).
- Feature logic lives in form event handlers ([TD4](../architecture.md#112-technical-debt)).
- Sorting and printing need Excel ([TD5](../architecture.md#112-technical-debt)), which the developer
  no longer has.
- Settings are read from static properties ([TD7](../architecture.md#112-technical-debt)).
- The wrapper and domain classes are `internal`, so another assembly cannot
  use them without changes.

The repository is public, and real Doubleknot exports contain scouts' names.

## Decision

[↑ Contents](#contents)

**Framework and project.**

- Use xUnit v3, with the `xunit.v3`, `xunit.runner.visualstudio`, and
  `Microsoft.NET.Test.Sdk` NuGet packages, plus `Bogus` and
  `DocumentFormat.OpenXml` (the same version as the application). As xUnit
  v3 requires, the test project builds as an executable.
- Replace the UnitTests project with an SDK-style test project,
  `src/Backsplice.Tests/Backsplice.Tests.csproj`, targeting .NET Framework
  4.8. Tests run in Visual Studio's Test Explorer, and with `dotnet test`
  once the application project builds with the `dotnet` tools.
- Delete the empty test method.
- Give the test project access to the application's internal classes with
  `[assembly: InternalsVisibleTo("Backsplice.Tests")]`, rather than making
  them public.

**Test data.**

- Roster workbooks are built in code by a test helper, using the OpenXML SDK,
  with the same six columns and layout as a Doubleknot export (baseline [8.3](../architecture.md#83-doubleknot-roster-format)).
  Each test builds only the rows it needs.
- Names, troop numbers, and similar values that a test does not care about
  are generated with Bogus (the `Bogus` NuGet package). Each generator is
  given a fixed seed on its own instance (`UseSeed`), never the global
  `Randomizer.Seed`, so data is the same on every run and tests running in
  parallel do not affect each other.
- Values a test depends on are written explicitly, not generated: program
  descriptions and periods, "Summer Camp Week" rows, provisional campers,
  duplicate registrations, and edge cases such as names containing spaces
  or apostrophes.
- Paperwork templates are stored as files in
  `src/Backsplice.Tests/TestData/Templates`, copied to the test output
  folder at build time: a copy of the repository's `Default Blank.xlsx`, plus
  any synthetic templates made for tests.
- All test data is synthetic. No real export, roster copy, or paperwork is
  committed to the repository.

**Kinds of tests.**

- **Unit tests** use in-memory data only and run everywhere.
- **File tests** that need a file on disk use a new temporary directory per
  test and delete it afterwards.
- **Tests that need Excel or a printer** are marked
  `[Trait("Category", "RequiresExcel")]` or
  `[Trait("Category", "RequiresPrinter")]`, and are run by hand on a machine
  that has them. They are excluded from the usual test run by trait.

**Seams.**

Code is made testable with small, behavior-preserving changes made before any
behavior changes:

- Classes that open files gain constructors that take a `Stream`.
- Logic in form event handlers is moved into methods on new transitional
  classes that have no WinForms dependency and can be called from tests.
  These classes are a temporary home for the logic, to be replaced by the
  redesigned building blocks in later ADRs.
- Excel, the file system, and settings are reached through interfaces where a
  test needs to replace them.
- These changes use the IDE's automated refactorings, one refactoring per
  commit, with no hand edits in the same commit.

**Where tests run.** Tests run locally, before each commit that changes
behavior and before each release. CI is not otherwise changed for now, but
the unused ".NET Core Desktop" workflow (`.github/workflows/dotnet-desktop.yml`)
is removed. It was an unadapted GitHub starter template that built a
packaging project that does not exist (baseline [7.3](../architecture.md#73-build-and-release), [TD12](../architecture.md#112-technical-debt)).

## Considered Options

[↑ Contents](#contents)

- **Framework:** MSTest v2, xUnit, and NUnit were considered. All three
  support .NET Framework 4.8 and modern .NET, so any of them would carry over
  to a future migration. MSTest v2 would have kept the existing test
  attributes. xUnit was chosen because it is widely used for modern .NET,
  including by the .NET team's own projects, and it runs test classes in
  parallel by default.
- **Test data:** building everything in code, or storing everything as files.
  Rosters vary from test to test, so building them in code keeps each test
  self-contained. Templates are designed in a spreadsheet program and are
  hard to build in code, so they are stored as files.
- **Project format:** keeping the old project format would have needed
  `vstest.console` or Visual Studio to run tests. The SDK-style format also
  works with `dotnet test`, once the projects it builds are SDK-style too.
- **Real data:** checking the new sorter against past years' Excel-sorted
  roster copies. Rejected: the sort only needs to group rows of the same
  offering, keep their order, and ignore case, which synthetic tests can
  specify fully. The formats in real exports are documented in baseline [8.3](../architecture.md#83-doubleknot-roster-format)
  and reproduced in synthetic data.
- **CI:** running tests on every push and before each release. Deferred,
  because the application is small, has a single developer, and is updated
  infrequently.

## Consequences

[↑ Contents](#contents)

- Tests can be written for the roster, paperwork, and domain classes without
  Excel and without real data.
- Feature logic still cannot be tested until it is moved out of the forms;
  each feature's first tests come with its seams.
- Because xUnit runs test classes in parallel, tests must not share state:
  no shared files, settings, or static data between test classes. The
  temporary directory per file test supports this. Tests that need Excel or
  a printer are kept in one test collection so they do not run in parallel
  with each other.
- Tests that need Excel or a printer can be written, but are not part of the
  usual run, and the developer currently cannot run the Excel ones.
- Nothing prevents a release from being built while tests fail; running them
  is a manual step. To be revisited if CI is set up.

## Done When

[↑ Contents](#contents)

- `src/Backsplice.Tests` exists as an SDK-style project targeting .NET
  Framework 4.8, using xUnit v3 and Bogus, and the old UnitTests project is
  removed from the solution.
- The application assembly declares
  `[assembly: InternalsVisibleTo("Backsplice.Tests")]`.
- DoubleKnotRoster has a constructor that takes a Stream.
- A roster builder builds a roster workbook that DoubleKnotRoster opens and
  reads correctly, shown by a passing test.
- `Default Blank.xlsx` is in `TestData/Templates` and is copied to the test
  output folder.
- `.github/workflows/dotnet-desktop.yml` is deleted.
- The documentation updates below are made.

## Documentation Updates

[↑ Contents](#contents)

When accepted and implemented, update the architecture document:

- Section 7.3 (Build and Release): the test project and how tests are run;
  remove the paragraph about the ".NET Core Desktop" workflow.
- [TD1](../architecture.md#112-technical-debt) (section 11.2): remove or reword.
- [TD12](../architecture.md#112-technical-debt) (section 11.2): remove the part about the ".NET Core Desktop"
  workflow.