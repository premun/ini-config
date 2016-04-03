namespace Config.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct)
    ]
    public class ConfigAttribute : System.Attribute
    {
        private string _file;

        public ConfigAttribute(string file)
        {
            this._file = file;
        }
    }
}