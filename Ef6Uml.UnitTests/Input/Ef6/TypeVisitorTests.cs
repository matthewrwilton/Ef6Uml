using System.Collections.Generic;
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


        private class Associated
        {
            public Associate Associate { get; set; }
        }

        private class Associate { }

        [Fact]
        public void Adds_Associated_Types_To_Model()
        {
            var model = new Model();

            var target = new TypeVisitor(model);
            target.Visit(typeof(Associated));

            var associatedClass = new Class(nameof(Associated), model);
            var associateClass = new Class(nameof(Associate), model);
            var expectedClasses = new List<Class>
            {
                associatedClass,
                associateClass
            };

            var expectedRelationships = new List<Relationship>
            {
                new Relationship(associatedClass, associateClass, RelationshipType.Association)
            };

            model.Classes.Should().Equal(expectedClasses);
            model.Relationships.Should().Equal(expectedRelationships);
        }


        private class AssociateOne
        {
            public AssociateTwo AssociateTwo { get; set; }
        }

        private class AssociateTwo
        {
            public AssociateThree AssociateThree { get; set; }
        }

        private class AssociateThree { }

        [Fact]
        public void Adds_Nested_Associations_To_Model()
        {
            var model = new Model();

            var target = new TypeVisitor(model);
            target.Visit(typeof(AssociateOne));

            var associateOneClass = new Class(nameof(AssociateOne), model);
            var associateTwoClass = new Class(nameof(AssociateTwo), model);
            var associateThreeClass = new Class(nameof(AssociateThree), model);
            var expectedClasses = new List<Class>
            {
                associateOneClass,
                associateTwoClass,
                associateThreeClass
            };

            var expectedRelationships = new List<Relationship>
            {
                new Relationship(associateTwoClass, associateThreeClass, RelationshipType.Association),
                new Relationship(associateOneClass, associateTwoClass, RelationshipType.Association)
            };

            model.Classes.Should().Equal(expectedClasses);
            model.Relationships.Should().Equal(expectedRelationships);
        }


        private class AssociateEnd1
        {
            public AssociateEnd2 AssociateEnd2 { get; set; }
        }

        private class AssociateEnd2
        {
            public AssociateEnd1 AssociateEnd1 { get; set; }
        }

        [Fact]
        public void Handles_Circular_Associations()
        {
            var model = new Model();

            var target = new TypeVisitor(model);
            target.Visit(typeof(AssociateEnd1));

            var associateEnd1Class = new Class(nameof(AssociateEnd1), model);
            var associateEnd2Class = new Class(nameof(AssociateEnd2), model);
            var expectedClasses = new List<Class>
            {
                associateEnd1Class,
                associateEnd2Class
            };

            var expectedRelationships = new List<Relationship>
            {
                new Relationship(associateEnd2Class, associateEnd1Class, RelationshipType.Association),
                new Relationship(associateEnd1Class, associateEnd2Class, RelationshipType.Association)
            };

            model.Classes.Should().Equal(expectedClasses);
            model.Relationships.Should().Equal(expectedRelationships);
        }


        public class Aggregate
        {
            public IList<Aggregated> Aggregation { get; set; }
        }

        public class Aggregated { }

        [Fact]
        public void Adds_Aggregated_Types_To_Model()
        {
            var model = new Model();

            var target = new TypeVisitor(model);
            target.Visit(typeof(Aggregate));
            
            var aggregateClass = new Class(nameof(Aggregate), model);
            var aggregatedClass = new Class(nameof(Aggregated), model);
            var expectedClasses = new List<Class>
            {
                aggregateClass,
                aggregatedClass
            };

            var expectedRelationships = new List<Relationship>
            {
                new Relationship(aggregateClass, aggregatedClass, RelationshipType.Aggregation)
            };

            model.Classes.Should().Equal(expectedClasses);
            model.Relationships.Should().Equal(expectedRelationships);
        }
    }
}
