using Ef6Uml.Uml;

namespace Ef6Uml.Output.Yuml
{
    public class YumlOutputter : IOutputter
    {
        public string Output(Class input)
        {
            return $"[{input.Name}]";
        }
    }
}
