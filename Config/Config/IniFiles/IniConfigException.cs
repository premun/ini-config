using System.Collections.Generic;
using Config.ConfigExceptions;
using Config.IniFiles.Errors;

namespace Config.IniFiles
{
	public class IniConfigException : ConfigException
	{
		public IEnumerable<FormatError> Errors { get; private set; }

		public IniConfigException(IEnumerable<FormatError> errors) : base("Error while parsing ini config file")
		{
			Errors = errors;
		}

		public IniConfigException(FormatError error)
			: base("Error while parsing ini config file: " + error.Message + " (line " + error.Line + ")")
		{
			Errors = new[] {error};
		}
	}
}
