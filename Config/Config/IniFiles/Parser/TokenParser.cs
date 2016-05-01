using System;
using System.IO;
using Config.IniFiles.Parser.Tokens;

namespace Config.IniFiles.Parser
{
	/// <summary>
	/// Parses stream into tokens and returns them one by one.
	/// </summary>
	internal class TokenParser : ITokenParser
	{
		private StreamReader _reader;
		private int _currentLine;

		public TokenParser()
		{
		}

		public TokenParser(string file)
		{
			Open(file);
		}

		public TokenParser(StreamReader stream)
		{
			Open(stream);
		}

		public void Open(string file)
		{
			Dispose();
			_reader = new StreamReader(file);
		}

		public void Open(StreamReader stream)
		{
			Dispose();
			_reader = stream;
		}

		/// <summary>
		/// Reads new token from stream.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when no file/stream opened.</exception>
		/// <exception cref="FormatException">Thrown when a parser error occured.</exception>
		/// <returns>Next token or null if file at the end.</returns>
		public Token GetNextToken()
		{
			if (_reader == null)
			{
				throw new InvalidOperationException("Open a file or stream before using GetNextToken()");
			}

			if (_reader.EndOfStream)
			{
				return null;
			}

			// Let's skip empty beginnings of lines
			int c = _reader.Peek();
			while (isWhiteSpace(c))
			{
				if (c == '\n')
				{
					_currentLine++;
				}

				_reader.Read();
				c = _reader.Peek();
			}

			// File ended with whitespaces / new lines
			if (_reader.EndOfStream)
			{
				return null;
			}

			_currentLine++;

			switch (c)
			{
				case '[':
					return SectionHeaderToken.FromStream(_reader);

				case ';':
					return CommentToken.FromStream(_reader);

				default:
					return OptionToken.FromStream(_reader);
			}
		}

		public int GetLine()
		{
			return _currentLine;
		}

		private bool isWhiteSpace(int c)
		{
			return char.IsWhiteSpace((char) c);
		}

		public void Dispose()
		{
			if (_reader != null)
			{
				_reader.Close();
				_reader.Dispose();
			}
		}
	}
}
