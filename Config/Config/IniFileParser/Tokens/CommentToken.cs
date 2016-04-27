using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFileParser.Tokens
{
	/// <summary>
	/// Represents a commented section that the parser read.
	/// </summary>
	internal class CommentToken : Token
	{
		public string Content { get; private set; }

		public static readonly char CommentSymbol = ';';

		public CommentToken(StreamReader reader)
		{
			Content = reader.ReadLine();
		}
	}
}
