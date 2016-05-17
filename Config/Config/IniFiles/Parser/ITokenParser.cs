using System;
using System.IO;
using Config.IniFiles.Parser.Tokens;

namespace Config.IniFiles.Parser
{
    /// <summary>
    /// Parses given input to tokens.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
	public interface ITokenParser : IDisposable
	{
        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
		void Open(string file);

        /// <summary>
        /// Opens the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
		void Open(StreamReader stream);

        /// <summary>
        /// Gets the next token from the parsing input.
        /// </summary>
        /// <returns></returns>
		Token GetNextToken();

        /// <summary>
        /// Gets the next line from the parsing input.
        /// </summary>
        /// <returns></returns>
		int GetLine();
	}
}