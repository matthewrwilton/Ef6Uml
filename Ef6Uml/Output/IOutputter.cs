using Ef6Uml.Uml;

namespace Ef6Uml.Output
{
    public interface IOutputter
    {
        string Output(Model model);
    }
}
