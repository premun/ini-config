using System.Collections.Generic;

namespace Config.Implementation
{
    public class ConfigSection : Dictionary<string, IConfigOption>, IConfigSection
    {
        #region Implementation of IConfigSection

        public string Name { get; private set; }
        public string Description { get; set; }
        public T Get<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public T GetSafe<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Set(string key, object value)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}