using Backsplice.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Backsplice.Tests
{
    public class RosterBuilderTests
    {
        [Fact]
        public void BuildStream_WithRegistrations_WritesEachRegistrationInOrder() {
            // build a roster with explicit data
            var roster = new RosterBuilder()
                .Add("Archery MB (period 1) XXX 99 9999 12:00AM (1189432)", lastName: "Washington", firstName: "George", group: "Troop 1")
                .Add("Archery MB (period 1) XXX 99 9999 12:00AM (1189432)", lastName: "Adams", firstName: "John", group: "Troop 2")
                .Add("Archery MB (period 1) XXX 99 9999 12:00AM (1189432)", lastName: "Jefferson", firstName: "Thomas", group: "Troop 3")
                .BuildStream();

            var doubleKnotRoster = new DoubleKnotRoster(roster, true);

            // check that row count is correct
            Assert.Equal(4, doubleKnotRoster.Rows);

            // read it back with DoubleKnotRoster
            var result = new List<List<string>>();
            for (int i = DoubleKnotRoster.StartRow; i < doubleKnotRoster.Rows; i++)
            {
                var registration = new List<string>();
                registration.Append(doubleKnotRoster.extractFirstName(i));
                registration.Append(doubleKnotRoster.extractLastName(i));
                registration.Append(doubleKnotRoster.extractTroopNumber(i));
                registration.Append(doubleKnotRoster.extractProgram(i));
                registration.Append(doubleKnotRoster.extractPeriod(i));
            }

            // compare to expected result
            var expected = new List<List<string>>()
            {
                new List<string> {"George", "Washington", "1", "Archery", "period 1"},
                new List<string> {"John", "Adams", "2", "Archery", "period 1" },
                new List<string> {"Thomas", "Jefferson", "3", "Archery", "period 1"}
            };

            Assert.True(expected.Zip(result, (e, r) => e.SequenceEqual(r)).All(x => x));
        }
    }
}
