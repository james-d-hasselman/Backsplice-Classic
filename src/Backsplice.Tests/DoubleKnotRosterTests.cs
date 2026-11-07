using Backsplice.Tests.TestData;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backsplice.Tests
{
    public class DoubleKnotRosterTests
    {
        [Fact]
        public void Sort_Row2RegistrationOutOfOrder_IsMoved()
        {
            using (var stream = new RosterBuilder()
                            .Add("Brownsea (all periods)")
                            .Add("Archery MB (period 1)")
                            .BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                Assert.Equal(3, roster.Rows);
                Assert.Equal("Archery", roster.extractProgram(2));
                Assert.Equal("period 1", roster.extractPeriod(2));
                Assert.Equal("Brownsea", roster.extractProgram(3));
                Assert.Equal("all periods", roster.extractPeriod(3));
            }
        }

        // extractProgram drops "MB", so expected values omit it.
        [Theory]
        [InlineData(
            new[] { "Camping MB (period 1)", "Brownsea (all periods)", "Archery MB (period 1)" },
            new[] { "Archery (period 1)", "Brownsea (all periods)", "Camping (period 1)" })]
        [InlineData(
            new[] { "Brownsea (all periods)", "Camping MB (period 1)", "Archery MB (period 1)" },
            new[] { "Archery (period 1)", "Brownsea (all periods)", "Camping (period 1)" })]
        [InlineData(
            new[] { "Brownsea (all periods)", "Archery MB (period 1)", "Brownsea (all periods)" },
            new[] { "Archery (period 1)", "Brownsea (all periods)", "Brownsea (all periods)" })]
        public void Sort_DescriptionsOutOfOrder_OrdersAlphabetically(string[] descriptions, string[] expected)
        {
            var rosterBuilder = new RosterBuilder();
            foreach (var description in descriptions)
            {
                rosterBuilder.Add(description);
            }

            using (var stream = rosterBuilder.BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                var actual = Enumerable.Range(DoubleKnotRoster.StartRow, roster.Rows - DoubleKnotRoster.StartRow + 1)
                                       .Select(row => $"{roster.extractProgram(row)} ({roster.extractPeriod(row)})");

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Sort_DescriptionsOutOfOrder_RelativeOrderMaintained()
        {
            using (var stream = new RosterBuilder()
                .Add("Brownsea (all periods)", "Washington")
                .Add("Archery MB (period 1)")
                .Add("Brownsea (all periods)", "Adams")
                .BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                Assert.Equal("Brownsea", roster.extractProgram(3));
                Assert.Equal("Washington", roster.extractLastName(3));
                Assert.Equal("Brownsea", roster.extractProgram(4));
                Assert.Equal("Adams", roster.extractLastName(4));
            }
        }

        [Fact]
        public void Sort_DescriptionsDifferingInCase_OrdersIgnoringCase()
        {
            // A case-sensitive sort puts "Period 2" before "period 1", because
            // uppercase letters sort before lowercase. Ignoring case, "period 1" comes first.
            using (var stream = new RosterBuilder()
                .Add("Archery MB (Period 2)")
                .Add("Archery MB (period 1)")
                .BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                Assert.Equal(3, roster.Rows);
                Assert.Equal("period 1", roster.extractPeriod(2));
                Assert.Equal("Period 2", roster.extractPeriod(3));
            }
        }

        [Fact]
        public void Sort_RosterInOrder_NotModified()
        {
            using (var stream = new RosterBuilder()
                .Add("Archery MB (period 1)")
                .Add("Brownsea (all periods)")
                .Add("Camping MB (period 1)")
                .BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                Assert.Equal("Archery", roster.extractProgram(2));
                Assert.Equal("Brownsea", roster.extractProgram(3));
                Assert.Equal("Camping", roster.extractProgram(4));
            }
        }

        [Fact]
        public void Sort_HeaderOnlyRoster_LeavesHeaderOnly()
        {
            using (var stream = new RosterBuilder().BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                Assert.Equal(1, roster.Rows);
            }
        }

        [Fact]
        public void Sort_SingleRegistration_NotModified()
        {
            using (var stream = new RosterBuilder()
                .Add("Brownsea (all periods)")
                .BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                Assert.Equal(2, roster.Rows);
                Assert.Equal("Brownsea", roster.extractProgram(2));
            }
        }

        [Fact]
        public void Sort_RegistrationsSortBeforeHeaderText_LeavesHeaderInRow1()
        {
            // "Brownsea" sorts before the header text "Description", so a sort that
            // included row 1 would move the header below the registration.
            using (var stream = new RosterBuilder()
                .Add("Brownsea (all periods)")
                .BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                Assert.Equal(2, roster.Rows);
                Assert.Equal("Description", roster.extractProgram(1));
                Assert.Equal("Brownsea", roster.extractProgram(2));
            }
        }

        [Fact]
        public void Sort_RegistrationsOutOfOrder_MovesWholeRows()
        {
            using (var stream = new RosterBuilder()
                .Add("Brownsea (all periods)", "Adams", "John", "Troop 2, XXXX (1000000)")
                .Add("Archery (period 1)", "Washington", "George", "Troop 1, XXXX (1000000)")
                .BuildStream())
            using (var roster = new DoubleKnotRoster(stream, false))
            {
                roster.Sort();

                Assert.Equal("Archery", roster.extractProgram(2));
                Assert.Equal("Washington", roster.extractLastName(2));
                Assert.Equal("George", roster.extractFirstName(2));
                Assert.Equal("1", roster.extractTroopNumber(2));
                Assert.Equal("Brownsea", roster.extractProgram(3));
                Assert.Equal("Adams", roster.extractLastName(3));
                Assert.Equal("John", roster.extractFirstName(3));
                Assert.Equal("2", roster.extractTroopNumber(3));
            }
        }

        [Fact]
        public void Sort_RowsReordered_ProducesValidWorkbook()
        {
            using (var stream = new RosterBuilder()
                .Add("Camping MB (period 1)")
                .Add("Brownsea (all periods)")
                .Add("Archery MB (period 1)")
                .BuildStream())
            {
                using (var roster = new DoubleKnotRoster(stream, false))
                {
                    roster.Sort();
                }

                stream.Position = 0;
                using (var document = SpreadsheetDocument.Open(stream, false))
                {
                    var errors = new OpenXmlValidator().Validate(document, TestContext.Current.CancellationToken);

                    Assert.Empty(errors);
                }
            }
        }

        [Fact]
        public void Dispose_AfterSort_SavesSortedOrder()
        {
            using (var stream = new RosterBuilder()
                .Add("Brownsea (all periods)")
                .Add("Archery MB (period 1)")
                .BuildStream())
            {
                using (var roster = new DoubleKnotRoster(stream, false))
                {
                    roster.Sort();
                }

                stream.Position = 0;
                using (var reopened = new DoubleKnotRoster(stream, true))
                {
                    Assert.Equal(3, reopened.Rows);
                    Assert.Equal("Archery", reopened.extractProgram(2));
                    Assert.Equal("Brownsea", reopened.extractProgram(3));
                }
            }
        }
    }
}
