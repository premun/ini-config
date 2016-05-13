using System;

namespace Config.Options
{
	public sealed class BoolOption : Option<bool>
	{
		private readonly string _textValue;

		public BoolOption(bool data)
		{
			Data = data;
			_textValue = "false";
		}

		public BoolOption(string value)
		{
			_textValue = value;

			switch (value)
			{
				case "0":
				case "f":
				case "n":
				case "off":
				case "no":
				case "disabled":
				case "false":
				case "False":
					Data = false;
					break;

				case "1":
				case "t":
				case "y":
				case "on":
				case "yes":
				case "enabled":
				case "true":
				case "True":
					Data = true;
					break;

				default:
					throw new ArgumentOutOfRangeException(string.Format("Unknown boolean value '{0}'", value));
			}
		}

		public override string Serialize()
		{
			return _textValue;
		}

		public static implicit operator BoolOption(bool b)
		{
			return new BoolOption(b);
		}
	}
}
