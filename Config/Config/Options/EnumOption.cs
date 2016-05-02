using System;

namespace Config.Options
{
	public class EnumOption<T> : Option<T> where T : struct, IConvertible
	{
		internal T Value;

		public EnumOption(string value)
		{
			RawValue = (T) Enum.Parse(typeof(T), value);
		}

		public EnumOption(T value)
		{
			Value = value;
		}
	}
}