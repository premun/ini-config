using System;

namespace Config.ConfigExceptions
{
	public class MissingSectionException : ConfigException
	{
		public MissingSectionException(string sectionName)
			: base("Required section '" + sectionName + "' not found")
		{
		}
	}
}

