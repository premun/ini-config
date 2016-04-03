using System;
using System.Collections.Generic;

namespace Config.Format
{
    public class FormatOption<T> : IFormatOption<T>
    {
        public FormatOption(string name)
        {
            Name = name;
        }

        #region Implementation of IFormatOption

        public string Name { get; set; }

        #endregion
    }

    public class FormatListOption<T> : List<T>, IFormatOption<T>
    {
        public FormatListOption(string name)
        {
            Name = name;
        }

        #region Implementation of IFormatOption

        public string Name { get; set; }

        #endregion
    }

	// TODO
    public class FormatEnumOption<T> : IFormatOption<T> where T : struct, IConvertible
    {
        public FormatEnumOption(string name)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            Name = name;
        }

        #region Implementation of IFormatOption

        public string Name { get; set; }

        #endregion
    }
}