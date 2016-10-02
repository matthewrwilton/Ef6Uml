using System.Linq;
using Ef6Uml.Uml;

namespace Ef6Uml.Output.Yuml
{
    public class YumlOutputter : IOutputter
    {
        public string Output(Class input)
        {
            if (input.Associations.Any())
            {
                return string.Join("\r\n", input.Associations.Select(association => $"[{input.Name}]->[{association.To.Name}]"));
            }
            else
            {
                return $"[{input.Name}]";
            }
        }
    }
}
