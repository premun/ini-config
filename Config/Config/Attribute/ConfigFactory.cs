using System;
using System.IO;

namespace Config.Attribute
{
    public static class ConfigFactory
    {
        public static T Create<T>()
        {
            // Find all attributes and load data.
            throw new NotImplementedException();
        }

        public static void Save(object configClass)
        {
            var hasConfigAttr = configClass.GetType()
                .IsDefined(typeof (ConfigAttribute), false);

            if (!hasConfigAttr)
            {
                throw new InvalidDataException();
            }

            // SaveConfig config
        }
    }
}