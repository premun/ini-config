using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.IniFiles.Parser.Tokens
{
    /// <summary>
    ///     Represents a config key=value item that the parser read.
    /// </summary>
    internal sealed class OptionToken : Token
    {
        /// <summary>
        ///     Gets or sets the name of the option.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the value of the option.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        ///     Gets the comment of the option.
        /// </summary>
        /// <value>
        ///     The comment.
        /// </value>
        public string Comment { get; private set; }

        /// <summary>
        ///     Creates option token from the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        /// <exception cref="System.FormatException">Missing '=' in item declaration!</exception>
        public static OptionToken FromStream(StreamReader stream)
        {
            var token = new OptionToken();

            string line = stream.ReadLine();

            if (!line.Contains("="))
            {
                throw new FormatException("Missing '=' in item declaration!");
            }

            string[] parts = line.Split(new[] {'='}, 2);
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