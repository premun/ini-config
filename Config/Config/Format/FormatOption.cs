using System;
using System.Collections.Generic;
using Config.Options;

namespace Config.Format
{
    public class FormatOption<T> : IFormatOption<T> where T : Option
    {
        public FormatOption(string name, object defaultValue = null)
        {
            Name = name;
	        DefaultValue = defaultValue;
        }

		#region Implementation of IFormatOption

		public string Name { get; set; }

		public object DefaultValue { get; set; }

		#endregion
	}

	public class FormatListOption<T> : List<T>, IFormatOption<T> where T : Option
	{
        public FormatListOption(string name)
        {
            Name = name;
        }

        #region Implementation of IFormatOption

        public string Name { get; set; }

        #endregion
    }
	
    public class FormatEnumOption<T> : IFormatOption<T> where T : struct, IConvertible
	{
		#region Implementation of IFormatOption

		public string Name { get; set; }

		#endregion

		public FormatEnumOption(string name)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            Name = name;
        }
    }
}