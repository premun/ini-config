using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test")]

namespace Config.Parser.Tokens
{
    /// <summary>
    /// Represents a commented section that the parser read.
    /// </summary>
    internal class CommentToken : Token
    {
        public string Content { get; private set; }

        public CommentToken(StreamReader reader)
        {
            Content = reader.ReadLine();
        }
    }
}
