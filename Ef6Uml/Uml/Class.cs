namespace Ef6Uml.Uml
{
    public class Class
    {
        public Class(string name, Model model)
        {
            Name = name;
            Model = model;
        }

        public string Name { get; }

        public Model Model { get; }

        public static bool operator ==(Class c1, Class c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Class c1, Class c2)
        {
            return !c1.Equals(c2);
        }

        public Class AggregationOf(Class of)
        {
            Model.HasRelationship(this, of, RelationshipType.Aggregation);
            return this;
        }

        public Class AssociatedWith(Class to)
        {
            Model.HasRelationship(this, to, RelationshipType.Association);
            return this;
        }

        public Class ComposedOf(Class of)
        {
            Model.HasRelationship(this, of, RelationshipType.Composition);
            return this;
        }

        public Class InheritingFrom(Class from)
        {
            Model.HasRelationship(from, this, RelationshipType.Inheritance);
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Class comparison = obj as Class;

            return comparison.Name == this.Name;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Name.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
