using System;
using Ef6Uml.Uml;

namespace Ef6Uml.Input.Ef6
{
    internal class TypeVisitor
    {
        private readonly Model _model;

        public TypeVisitor(Model model)
        {
            _model = model;
        }

        public void Visit(Type type)
        {
            _model.HasClass(type.Name);
        }
    }
}
