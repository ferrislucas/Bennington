﻿using System;
using System.Collections.Generic;
using System.Linq;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Buttons
{
    public interface IEditPageButtonRetriever
    {
        IEnumerable<Button> GetActionButtons(Type type, object @object);
    }

    public class EditPageButtonRetriever : IEditPageButtonRetriever
    {
        private readonly IServiceLocator serviceLocator;

        public EditPageButtonRetriever(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public IEnumerable<Button> GetActionButtons(Type type, object @object)
        {
            if (@object == null) return new Button[] {};

            var modelType = type;
            var buttonRegistryType = GetTheButtonRegistryForThisType_OrBust(modelType);

            return ThisIsNotAValidButtonRegistryType(buttonRegistryType)
                       ? AnEmptySet()
                       : iGo(type, buttonRegistryType, @object);
        }

        private IEnumerable<Button> iGo(Type @interface, Type buttonRegistryType, Object @object)
        {
            var registry = CreateTheButtonRegistry(buttonRegistryType);
            return typeof(EditPageButtonRetriever)
                .GetMethod("PublicMethodForCallingAGenericMethod")
                .MakeGenericMethod(@interface)
                .Invoke(this, new[] { registry, @object }) as IEnumerable<Button>;
        }

        public IEnumerable<Button> PublicMethodForCallingAGenericMethod<T>(IEditPageButtonRegistry<T> buttonRegistry, T source) where T : class
        {
            return buttonRegistry.GetTheActionButtons(source);
        }

        private IEditPageButtonRegistry CreateTheButtonRegistry(Type buttonRegistryType)
        {
            return ((IEditPageButtonRegistry) serviceLocator.Resolve(buttonRegistryType));
        }

        private static IEnumerable<Button> AnEmptySet()
        {
            return new Button[] {};
        }

        private static bool ThisIsNotAValidButtonRegistryType(Type buttonRegistryType)
        {
            return buttonRegistryType == null;
        }

        private static Type GetTheButtonRegistryForThisType_OrBust(Type modelType)
        {
            try
            {
                return GetTheButtonRegistryForThisType(modelType);
            } catch
            {
                return null;
            }
        }

        private static Type GetTheButtonRegistryForThisType(Type modelType)
        {
            Type buttonRegistryType = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var type in assembly.GetTypes().Where(x => x.IsInterface == false && x.IsAbstract == false))
                {
                    var interfaces = type.GetInterfaces()
                        .FirstOrDefault(y => y.Name.StartsWith("IEditPageButtonRegistry`"));

                    if (interfaces == null) continue;
                    var genericArguments = interfaces.GetGenericArguments();
                    if (genericArguments.Any() && genericArguments[0] == modelType)
                        buttonRegistryType = type;
                }
            return buttonRegistryType;
        }
    }
}