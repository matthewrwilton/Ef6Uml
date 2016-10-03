using System;
using System.Linq;
using System.Text;
using Ef6Uml.Uml;

namespace Ef6Uml.Output.Yuml
{
    public class YumlOutputter : IOutputter
    {
        public string Output(Class input)
        {
            var output = new StringBuilder();

            if (input.Relationships.Any())
            {
                foreach (var relationship in input.Relationships)
                {
                    OutputRelationship(output, input, relationship);
                }
            }
            else
            {
                OutputClassName(output, input);
            }

            return output.ToString().Trim();
        }

        private void OutputClassName(StringBuilder output, Class c)
        {
            output.Append($"[{c.Name}]");
        }

        private void OutputRelationship(StringBuilder output, Class from, Relationship relationship)
        {
            OutputClassName(output, from);
            
            switch (relationship.Type)
            {
                case RelationshipType.Association:
                    output.Append("->");
                    break;
                case RelationshipType.Aggregation:
                    output.Append("+->");
                    break;
                case RelationshipType.Composition:
                    output.Append("++->");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(relationship), $"Relationship type '{relationship.Type}' is not handled.");
            }

            var to = relationship.With;

            OutputClassName(output, to);
            output.Append("\r\n");

            foreach (var nestedRelationship in to.Relationships)
            {
                OutputRelationship(output, to, nestedRelationship);
            }
        }
    }
}
