using System.Collections.Generic;

namespace Config.Format
{
    /// <summary>
    /// Thrown when config format does not correspond with parsed config structure. 
    /// Contains list of all errors, that were encountered.
    /// </summary>
    public class ConfigFormatException : ConfigException
	{
        public IEnumerable<ConfigException> ErrorList { get; set; }
    }
}