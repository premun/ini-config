using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFileParser.Tokens
{
	/// <summary>
	/// Represents a config file section [Header] that the parser read.
	/// </summary>
	class SectionHeaderToken : Token
	{
		public string Name { get; private set; }

		// TODO: Exceptions in ctor?
		public SectionHeaderToken(StreamReader reader)
		{
			StringBuilder sb = new StringBuilder();

			// Consume opening square bracket
			if (reader.Peek() == '[')
			{
				reader.Read();
			}

			int c = reader.Read();
			while (c != ']')
			{
				if (c == '\n')
				{
					throw new FormatException("Section header without closing bracket found!");
				}

				sb.Append((char) c);
				c = reader.Read();
			}

			if (sb.Length == 0)
			{
				throw new FormatException("Section header with empty name encountered!");
			}

			Name = sb.ToString();
		}
	}
}
