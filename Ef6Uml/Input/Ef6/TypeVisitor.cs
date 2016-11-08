﻿using System;
using System.Linq;
using System.Reflection;
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

        public Class Visit(Type type)
        {
            var className = type.Name;

            var modelClass = _model.Classes.SingleOrDefault(c => c.Name == className);
            if (modelClass != null)
            {
                return modelClass;
            }

            modelClass = _model.HasClass(className);

            var properties = type.GetMembers()
                .Select(member => member as PropertyInfo)
                .Where(propertyInfo => propertyInfo != null);

            foreach (var property in properties)
            {
                var propertyClass = Visit(property.PropertyType);
                modelClass.AssociatedWith(propertyClass);
            }

            return modelClass;
        }
    }
}
