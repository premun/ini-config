using System;
using System.Collections.Generic;
using Config.Format.Errors;

namespace Config.Format
{
    /// <summary>
    /// Thrown when config format does not correspond with parsed config structure. 
    /// Contains list of all errors, that were encountered.
    /// </summary>
    public class ConfigFormatException : ConfigException
	{
        public IEnumerable<ConfigFormatError> ErrorList { get; set; }
    }
}