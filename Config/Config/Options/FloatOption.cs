using System;
using System.Globalization;

namespace Config.Options
{
	public class FloatOption : Option<float>
	{
		public const string DecimalSeparator = ".";

		public FloatOption(float rawValue)
		{
			RawValue = rawValue;
		}

		public FloatOption(string value)
		{
			var culture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
			culture.NumberFormat.NumberDecimalSeparator = DecimalSeparator;

			// TODO: Tady je problem, ze kdyz nastavime tecku, jako oddelovac, tak to umi parsovat z configu
			//       Kdyz se ale da defaultValue, tak se ulozi s carkou pred tim, nez se to znova parsuje (ConfigSection indexer getter)
			try
			{
				RawValue = float.Parse(value, culture);
			}
			catch (FormatException)
			{
				RawValue = float.Parse(value);
			}
		}

		public static implicit operator FloatOption(float f)
		{
			return new FloatOption(f);
		}
	}
}
