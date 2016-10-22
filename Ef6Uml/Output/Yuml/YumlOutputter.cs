using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ef6Uml.Uml;

namespace Ef6Uml.Output.Yuml
{
    public class YumlOutputter : IOutputter
    {
        public string Output(Model model)
        {
            var worker = new YumlOutputterWorker();
            return worker.Output(model);
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

            public string Output(Model model)
            {
                foreach (var c in model.Classes)
                {
                    VisitClass(c, model);
                }

                return _output.ToString().Trim();
            }

            public void VisitClass(Class c, Model model)
            {
                if (_visitedClasses.Contains(c))
                {
                    return;
                }

                _visitedClasses.Add(c);

                var relationshipsFromClass = model.GetRelationshipsFromClass(c);
                if (relationshipsFromClass.Count > 0)
                {
                    foreach (var relationship in relationshipsFromClass)
                    {
                        VisitClass(relationship.To, model);
                        OutputRelationship(relationship);
                    }
                }
                else
                {
                    if (model.GetRelationshipsToClass(c).Count == 0)
                    {
                        OutputClassName(c);
                        _output.Append("\r\n");
                    }
                }
            }

            private void OutputClassName(Class c)
            {
                _output.Append($"[{c.Name}]");
            }

            private void OutputRelationship(Relationship relationship)
            {
                OutputClassName(relationship.From);

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
                
                OutputClassName(relationship.To);

                _output.Append("\r\n");
            }
        }
    }
}
