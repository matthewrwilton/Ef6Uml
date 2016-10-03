using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ef6Uml.Uml;

namespace Ef6Uml.Output.Yuml
{
    public class YumlOutputter : IOutputter
    {
        public string Output(Class input)
        {
            var worker = new YumlOutputterWorker();
            return worker.Output(input);
        }

        private class YumlOutputterWorker
        {
            private readonly StringBuilder _output;
            private readonly List<Class> _visitedClasses;

            public YumlOutputterWorker()
            {
                _output = new StringBuilder();
                _visitedClasses = new List<Class>();
            }

            public string Output(Class input)
            {
                OutputClass(input, true);

                return _output.ToString().Trim();
            }

            public void OutputClass(Class c, bool alwaysOutput = false)
            {
                if (_visitedClasses.Contains(c))
                {
                    return;
                }

                _visitedClasses.Add(c);

                if (c.Relationships.Any())
                {
                    foreach (var relationship in c.Relationships)
                    {
                        OutputRelationship(c, relationship);
                    }
                }
                else if (alwaysOutput)
                {
                    OutputClassName(c);
                }
            }

            private void OutputClassName(Class c)
            {
                _output.Append($"[{c.Name}]");
            }

            private void OutputRelationship(Class from, Relationship relationship)
            {
                OutputClassName(from);

                switch (relationship.Type)
                {
                    case RelationshipType.Association:
                        _output.Append("->");
                        break;
                    case RelationshipType.Aggregation:
                        _output.Append("+->");
                        break;
                    case RelationshipType.Composition:
                        _output.Append("++->");
                        break;
                    case RelationshipType.Inheritance:
                        _output.Append("^-");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(relationship), $"Relationship type '{relationship.Type}' is not handled.");
                }

                var to = relationship.With;

                OutputClassName(to);
                _output.Append("\r\n");

                OutputClass(to);
            }
        }
    }
}
