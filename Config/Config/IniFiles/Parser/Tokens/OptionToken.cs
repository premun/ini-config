﻿using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFiles.Parser.Tokens
{
	/// <summary>
	/// Represents a config key=value item that the parser read.
	/// </summary>
	internal class OptionToken : Token
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public string Comment { get; private set; }

		public static OptionToken FromStream(StreamReader stream)
		{
			var token = new OptionToken();

			string line = stream.ReadLine();

			if (!line.Contains("="))
			{
				throw new FormatException("Missing '=' in item declaration!");
			}

			string[] parts = line.Split(new[] { '=' }, 2);
			string value = TrimEndEscaped(parts[1].TrimStart());

			// Comment after value
			// foo = bar ; comment
			int commentStartPosition = FindCommentStartPosition(value);
			if (commentStartPosition != -1)
			{
				value = value.Substring(0, commentStartPosition);
				value = TrimEndEscaped(value);

				token.Comment = line.Substring(commentStartPosition + 1).Trim();
			}

			value = value.Replace("\\ ", " ");
			value = value.Replace("\\" + CommentToken.CommentSymbol, CommentToken.CommentSymbol + "");

			token.Name = parts[0].Trim();
			token.Value = value;

			return token;
		}

		private static int FindCommentStartPosition(string s)
		{
			int pos = s.IndexOf(CommentToken.CommentSymbol);
			while (pos > 0 && s[pos - 1] == '\\')
			{
				pos = s.IndexOf(CommentToken.CommentSymbol, pos + 1);
			}

			return pos;
		}

		private static string TrimEndEscaped(string s)
		{
			if (char.IsWhiteSpace(s[s.Length - 1]))
			{
				s = s.TrimEnd();
				return s.EndsWith("\\") ? s + ' ' : s;
			}

			return s;
		}
	}
}
