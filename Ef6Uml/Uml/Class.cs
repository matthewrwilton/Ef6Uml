using System.Collections.Generic;

namespace Ef6Uml.Uml
{
    public class Class
    {
        public Class(string name, IReadOnlyList<Relationship> relationships)
        {
            Name = name;
            Relationships = new List<Relationship>(relationships);
        }

        public string Name { get; }

        public IReadOnlyList<Relationship> Relationships { get; }
    }
}
