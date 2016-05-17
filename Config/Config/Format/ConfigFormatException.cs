using System.Collections.Generic;
using Config.ConfigExceptions;

namespace Config.Format
{
    /// <summary>
    /// Thrown when config format does not correspond with parsed config structure. 
    /// Contains list of all errors, that were encountered.
    /// </summary>
    public class ConfigFormatException : ConfigException
	{
        /// <summary>
        /// Gets or sets the list of errors.
        /// </summary>
        /// <value>
        /// The error list.
        /// </value>
        public IEnumerable<ConfigException> ErrorList { get; set; }
    }
}