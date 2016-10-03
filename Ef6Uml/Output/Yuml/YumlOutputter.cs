using System;
using System.Linq;
using Ef6Uml.Uml;

namespace Ef6Uml.Output.Yuml
{
    public class YumlOutputter : IOutputter
    {
        public string Output(Class input)
        {
            if (input.Relationships.Any())
            {
                return string.Join("\r\n", input.Relationships.Select(relationship => OutputRelationship(input, relationship)));
            }
            else
            {
                return $"[{input.Name}]";
            }
        }

        private string OutputRelationship(Class from, Relationship relationship)
        {
            string joinString;
            switch(relationship.Type)
            {
                case RelationshipType.Association:
                    joinString = "->";
                    break;
                case RelationshipType.Aggregation:
                    joinString = "+->";
                    break;
                case RelationshipType.Composition:
                    joinString = "++->";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(relationship), $"Relationship type '{relationship.Type}' is not handled.");
            }

            return $"[{from.Name}]{joinString}[{relationship.With.Name}]";
        }
    }
}
