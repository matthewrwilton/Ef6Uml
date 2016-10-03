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
            var user = new ClassBuilder()
                .WithName("User");

            var actual = target.Output(user);
            var expected = "[User]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Simple_Association()
        {
            var target = new YumlOutputter();
            var billingAddress = new ClassBuilder()
                .WithName("Billing Address");
            var customer = new ClassBuilder()
                .WithName("Customer")
                .WithAssociationTo(billingAddress);

            var actual = target.Output(customer);
            var expected = "[Customer]->[Billing Address]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Output_Correct_For_Single_Aggregation()
        {
            var target = new YumlOutputter();
            var point = new ClassBuilder()
                .WithName("Point");
            var location = new ClassBuilder()
                .WithName("Location")
                .WithAggregationOf(point);

            var actual = target.Output(location);
            var expected = "[Location]+->[Point]";

            actual.Should().Be(expected);
        }

        private class ClassBuilder
        {
            private List<Relationship> _relationships = new List<Relationship>();
            private string _name = "";

            public static implicit operator Class(ClassBuilder classBuilder)
            {
                return classBuilder.Build();
            }

            public Class Build()
            {
                return new Class(_name, _relationships);
            }

            public ClassBuilder WithName(string name)
            {
                _name = name;
                return this;
            }

            public ClassBuilder WithAssociationTo(Class to)
            {
                _relationships.Add(new Relationship(to, RelationshipType.Association));
                return this;
            }

            public ClassBuilder WithAggregationOf(Class of)
            {
                _relationships.Add(new Relationship(of, RelationshipType.Aggregation));
                return this;
            }
        }
    }
}
