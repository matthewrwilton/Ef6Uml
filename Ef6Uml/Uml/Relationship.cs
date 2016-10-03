namespace Ef6Uml.Uml
{
    public class Relationship
    {
        public Relationship(Class with, RelationshipType type)
        {
            With = with;
            Type = type;
        }

        public Class With { get; }

        public RelationshipType Type { get; }

        public override string ToString()
        {
            return $"{With}({Type})";
        }
    }
}
