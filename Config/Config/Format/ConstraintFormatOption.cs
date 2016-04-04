using System;
using System.Collections.Generic;
using Config.Values;

namespace Config.Format
{
	public class ConstraintFormatOption<T> : FormatOption<T> where T : ConfigValue
	{
		public readonly Func<object, bool> Contraint;

		public ConstraintFormatOption(string name, Func<object, bool> contraint, object defaultValue = null)
			: base(name, defaultValue)
		{
			Contraint = contraint;
		}
	}

	public class EnumFormatOption<T> : FormatOption<T> where T : ConfigValue
	{
		private readonly IEnumerable<string> _allowedValues;

		public EnumFormatOption(string name, IEnumerable<string> allowedValues, object defaultValue = null)
			: base(name, defaultValue)
		{
			_allowedValues = allowedValues;
		}
	}
}