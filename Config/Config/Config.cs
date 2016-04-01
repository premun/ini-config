using System.Collections.Generic;

namespace Config.Implementation
{
    public class Config : IConfig
    {
        #region Implementation of IConfig

        public IConfigSection this[string sectionName]
        {
            get { throw new System.NotImplementedException(); }
        }

        public IConfigSection GetSection(string sectionName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IConfigSection> Sections { get; private set; }
        public bool AddSection(IConfigSection section)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveSection(IConfigSection section)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveSection(string sectionName)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}