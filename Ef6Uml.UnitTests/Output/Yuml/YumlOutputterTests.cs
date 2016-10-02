using Ef6Uml.Uml;
using Ef6Uml.Output.Yuml;
using FluentAssertions;
using Xunit;

namespace Ef6Uml.UnitTests.Output.Yuml
{
    public class YumlOutputterTests
    {
        [Fact]
        public void Single_Class()
        {
            var target = new YumlOutputter();
            var input = new Class("User");

            var actual = target.Output(input);
            var expected = "[User]";

            actual.Should().Be(expected);
        }
    }
}
