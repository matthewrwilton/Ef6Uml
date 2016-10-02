using Ef6Uml.Uml;
using Ef6Uml.Output.Yuml;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;

namespace Ef6Uml.UnitTests.Output.Yuml
{
    public class YumlOutputterTests
    {
        [Fact]
        public void Single_Class()
        {
            var target = new YumlOutputter();
            var input = new Class("User", new Association[0]);

            var actual = target.Output(input);
            var expected = "[User]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Simple_Association()
        {
            var target = new YumlOutputter();
            var billingAddress = new Class("Billing Address", new Association[0]);
            var customer = new Class(
                "Customer", 
                new List<Association>
                {
                    new Association(billingAddress)
                });

            var actual = target.Output(customer);
            var expected = "[Customer]->[Billing Address]";

            actual.Should().Be(expected);
        }
    }
}
