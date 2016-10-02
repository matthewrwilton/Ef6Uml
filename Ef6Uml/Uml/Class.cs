using System.Collections.Generic;

namespace Ef6Uml.Uml
{
    public class Class
    {
        public Class(string name, IReadOnlyList<Association> assocations)
        {
            Name = name;
            Associations = new List<Association>(assocations);
        }

        public string Name { get; }

        public IReadOnlyList<Association> Associations { get; }
    }
}
