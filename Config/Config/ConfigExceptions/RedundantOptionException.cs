namespace Config.ConfigExceptions
{
    public class RedundantOptionException : ConfigException
    {
        public RedundantOptionException(string sectionName, string optionName)
            : base("Not specified option '" + sectionName + "#" + optionName + "' found")
        {
        }
    }
}