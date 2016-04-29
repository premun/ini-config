﻿using System.IO;
using System.Runtime.CompilerServices;
using Config.IniFiles.Parser.Tokens;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFiles.Parser
{
	/// <summary>
	/// Parses stream into tokens and returns them one by one.
	/// </summary>
	internal class TokenParser : ITokenParser
	{
		private readonly StreamReader _reader;
		private int _currentLine;

		internal TokenParser(StreamReader reader)
		{
			_reader = reader;
		}

		/// <summary>
		/// Reads new token from stream.
		/// </summary>
		/// <returns>Next token or null if file at the end.</returns>
		public Token GetNextToken()
		{
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
					return new SectionHeaderToken(_reader);

				case ';':
					return new CommentToken(_reader);

				default:
					return new OptionToken(_reader);
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
	}
}
