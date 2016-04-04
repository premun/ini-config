using System;
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
}