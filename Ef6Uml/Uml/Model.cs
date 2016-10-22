using System.Collections.Generic;
using System.Linq;

namespace Ef6Uml.Uml
{
    public class Model
    {
        private readonly List<Class> _classes;
        private readonly List<Relationship> _relationships;

        public Model()
        {
            _classes = new List<Class>();
            _relationships = new List<Relationship>();
        }

        public IReadOnlyList<Class> Classes => _classes;

        public IReadOnlyList<Relationship> Relationships => _relationships;

        public IReadOnlyList<Relationship> GetRelationshipsFromClass(Class from)
        {
            return _relationships.Where(r => r.From == from)
                .ToList();
        }

        public IReadOnlyList<Relationship> GetRelationshipsToClass(Class to)
        {
            return _relationships.Where(r => r.To == to)
                .ToList();
        }

        public Class HasClass(string name)
        {
            var c = new Class(name, this);

            if (!_classes.Contains(c))
            {
                _classes.Add(c);
            }

            return c;
        }

        public void HasRelationship(Class from, Class to, RelationshipType type)
        {
            _relationships.Add(new Relationship(from, to, type));
        }
    }
}
