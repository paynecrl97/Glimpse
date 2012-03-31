using System;
using System.Collections.Generic;
using Glimpse.Core2.Extensibility;

namespace Glimpse.Core2.Framework
{
    public abstract class ResourceEndpointConfiguration
    {
        public string GenerateUri(IResource resource, ILogger logger, IDictionary<string, string> requestTokenValues)
        {
            if (resource == null) throw new ArgumentNullException("resource");
            if (logger == null) throw new ArgumentNullException("logger");
            if (requestTokenValues == null) throw new ArgumentNullException("requestTokenValues");

            var parmeters = new Dictionary<string, string>();

            try
            {
                foreach (var key in resource.ParameterKeys)
                {
                    var value = requestTokenValues.ContainsKey(key)
                                    ? requestTokenValues[key]
                                    : string.Format("{{{0}}}", key);
                    parmeters.Add(key, value);
                }
            }
            catch(Exception exception)
            {
                logger.Warn(Resources.GenerateUriParameterKeysWarning, exception, resource.GetType());
            }

            string result = null;
            try
            {
                result = GenerateUri(resource.Name, parmeters, logger);
            }
            catch(Exception exception)
            {
                logger.Error(Resources.GenerateUriExecutionError, exception, GetType());
            }

            if (result != null)
                return result;

            return string.Empty;
        }

        protected abstract string GenerateUri(string resourceName, IEnumerable<KeyValuePair<string, string>> parameters, ILogger logger);
    }
}