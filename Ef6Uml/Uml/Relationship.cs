namespace Ef6Uml.Uml
{
    public class Relationship
    {
        public static bool operator ==(Relationship r1, Relationship r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(Relationship r1, Relationship r2)
        {
            return !r1.Equals(r2);
        }

        public Relationship(Class from, Class to, RelationshipType type)
        {
            From = from;
            To = to;
            Type = type;
        }

        public Class From { get; }

        public Class To { get; }

        public RelationshipType Type { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Relationship comparison = obj as Relationship;

            return comparison.From == From &&
                comparison.To == To &&
                comparison.Type == Type;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + From.GetHashCode();
            hash = (hash * 7) + To.GetHashCode();
            hash = (hash * 7) + Type.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return $"{From} ({Type}) {To}";
        }
    }
}
