using System.Collections.Generic;

namespace Ef6Uml.Uml
{
    public class Class
    {
        private readonly List<Relationship> _relationships = new List<Relationship>();

        public Class(string name, IReadOnlyList<Relationship> relationships)
        {
            Name = name;
            _relationships = new List<Relationship>(relationships);
        }

        public string Name { get; }

        public IReadOnlyList<Relationship> Relationships => _relationships;

        public void AddRelationship(Relationship relationship)
        {
            _relationships.Add(relationship);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
