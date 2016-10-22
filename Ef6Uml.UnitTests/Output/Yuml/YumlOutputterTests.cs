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
        public void Output_Correct_For_Single_Class()
        {
            var target = new YumlOutputter();

            var model = new Model();
            model.HasClass("User");

            var actual = target.Output(model);
            var expected = "[User]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Output_Correct_For_Single_Association()
        {
            var target = new YumlOutputter();

            var model = new Model();
            var billingAddressClass = model.HasClass("Billing Address");
            billingAddressClass = model.HasClass("Customer")
                .AssociatedWith(billingAddressClass);

            var actual = target.Output(model);
            var expected = "[Customer]->[Billing Address]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Output_Correct_For_Single_Aggregation()
        {
            var target = new YumlOutputter();

            var model = new Model();
            var pointClass = model.HasClass("Point");
            model.HasClass("Location")
                .AggregationOf(pointClass);

            var actual = target.Output(model);
            var expected = "[Location]+->[Point]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Output_Correct_For_Single_Composition()
        {
            var target = new YumlOutputter();

            var model = new Model();
            var pointClass = model.HasClass("Point");
            model.HasClass("Location")
                .ComposedOf(pointClass);

            var actual = target.Output(model);
            var expected = "[Location]++->[Point]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Output_Correct_For_Nested_Relationships()
        {
            var target = new YumlOutputter();

            var model = new Model();
            var pointClass = model.HasClass("Point");
            var locationClass = model.HasClass("Location")
                .ComposedOf(pointClass);
            model.HasClass("Company")
                .ComposedOf(locationClass);

            var actual = target.Output(model);
            var expected = "[Location]++->[Point]\r\n[Company]++->[Location]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Output_Correct_For_Circular_Relationships()
        {
            var target = new YumlOutputter();
            var model = new Model();
            var studentClass = model.HasClass("Student");
            var teacherClass = model.HasClass("Teacher")
                .AggregationOf(studentClass);
            studentClass.AssociatedWith(teacherClass);

            var actual = target.Output(model);
            var expected = "[Teacher]+->[Student]\r\n[Student]->[Teacher]";

            actual.Should().Be(expected);
        }

        [Fact]
        public void Output_Correct_For_Inheritance()
        {
            var target = new YumlOutputter();
            var model = new Model();
            var wagesClass = model.HasClass("Wages");
            var contractorClass = model.HasClass("Contractor")
                .InheritingFrom(wagesClass);

            var actual = target.Output(model);
            var expected = "[Wages]^-[Contractor]";

            actual.Should().Be(expected);
        }
    }
}
