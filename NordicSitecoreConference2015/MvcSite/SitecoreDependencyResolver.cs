using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Framework.DependencyInjection;

namespace MvcSite
{
    public class BasicDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public BasicDependencyResolver(IServiceProvider servieProvider)
        {
            _serviceProvider = servieProvider;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return ActivatorUtilities.CreateInstance(_serviceProvider, serviceType);
            }

            catch (Exception) // Needed for Sitecore's logic.
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }
    }
}