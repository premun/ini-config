using System.IO;

namespace Config.Attribute
{
    public static class Config
    {
        public static void Init()
        {
            // Find all attributes and load data.
        }

        public static void Save(object configClass)
        {
            var hasConfigAttr = configClass.GetType()
                .IsDefined(typeof (ConfigAttribute), false);

            if (!hasConfigAttr)
            {
                throw new InvalidDataException();
            }

            // Save config
        }
    }
}