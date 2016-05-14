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

    public class RedundantOptionException : ConfigException
    {
        public RedundantOptionException(string sectionName, string optionName)
            : base("Not specified option '" + sectionName + "#" + optionName + "' found")
        {
        }
    }
}

