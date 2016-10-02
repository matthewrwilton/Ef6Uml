namespace Ef6Uml.Uml
{
    public class Association
    {
        public Association(Class to)
        {
            To = to;
        }

        public Class To { get; }
    }
}
