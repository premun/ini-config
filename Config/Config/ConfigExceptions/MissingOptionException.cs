using System;

namespace Config.ConfigExceptions
{
	public class MissingOptionException : ConfigException
	{
		public MissingOptionException(string sectionName, string optionName)
			: base("Required option '" + sectionName + "#" + optionName + "' not found")
		{
		}
	}
}

