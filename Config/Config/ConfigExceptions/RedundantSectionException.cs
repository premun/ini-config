namespace Config.ConfigExceptions
{
    public class RedundantSectionException : ConfigException
    {
        public RedundantSectionException(string sectionName)
            : base("Not specified section '" + sectionName + "' found")
        {
        }
    }
}