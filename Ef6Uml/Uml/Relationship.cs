namespace Ef6Uml.Uml
{
    public class Relationship
    {
        public Relationship(Class from, Class to, RelationshipType type)
        {
            From = from;
            To = to;
            Type = type;
        }

        public Class From { get; }

        public Class To { get; }

        public RelationshipType Type { get; }

        public override string ToString()
        {
            return $"{From} ({Type}) {To}";
        }
    }
}
