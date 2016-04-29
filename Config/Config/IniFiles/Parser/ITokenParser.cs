using System;
using System.IO;
using Config.IniFiles.Parser.Tokens;

namespace Config.IniFiles.Parser
{
	public interface ITokenParser : IDisposable
	{
		void Open(string file);

		void Open(StreamReader stream);

		Token GetNextToken();

		int GetLine();
	}
}