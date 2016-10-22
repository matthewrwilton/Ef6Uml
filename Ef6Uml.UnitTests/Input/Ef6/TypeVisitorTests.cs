using Ef6Uml.Input.Ef6;
using Ef6Uml.Uml;
using FluentAssertions;
using Xunit;

namespace Ef6Uml.UnitTests.Input.Ef6
{
    public class TypeVisitorTests
    {
        private class Standalone { }

        [Fact]
        public void Adds_Standalone_Type_To_Model()
        {
            var model = new Model();

            var target = new TypeVisitor(model);
            target.Visit(typeof(Standalone));

            var expected = new Class(nameof(Standalone), model);

            model.Classes.Should().Contain(expected);
        }
    }
}
