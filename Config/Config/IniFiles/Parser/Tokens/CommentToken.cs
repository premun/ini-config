using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFiles.Parser.Tokens
{
	/// <summary>
	/// Represents a commented section that the parser read.
	/// </summary>
	internal class CommentToken : Token
	{
		internal string Content { get; private set; }

		internal static readonly char CommentSymbol = ';';

		internal static CommentToken FromStream(StreamReader stream)
		{
			var content = stream.ReadLine();

			if (content.StartsWith(";"))
			{
				content = content.Substring(1).Trim();
			}

			return new CommentToken
			{
				Content = content
			};
		}
	}
}
