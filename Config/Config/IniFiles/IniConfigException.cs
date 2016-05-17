using System.Collections.Generic;
using Config.ConfigExceptions;
using Config.IniFiles.Errors;

namespace Config.IniFiles
{
    /// <summary>
    /// Thrown if builder cannot loads config from an ini file.
    /// </summary>
    /// <seealso cref="Config.ConfigExceptions.ConfigException" />
	public class IniConfigException : ConfigException
	{
		public IEnumerable<FormatError> Errors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniConfigException"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
		public IniConfigException(IEnumerable<FormatError> errors) : base("Error while parsing ini config file")
		{
			Errors = errors;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="IniConfigException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
		public IniConfigException(FormatError error)
			: base("Error while parsing ini config file: " + error.Message + " (line " + error.Line + ")")
		{
			Errors = new[] {error};
		}
	}
}
