using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Config.IniFiles.Parser;
using Config.IniFiles.Parser.Tokens;
using Moq;

namespace ConfigTests
{
    internal static class MockFactory
    {
        /// <summary>
        ///     Returns a mocked token parser that will yield given tokens (and line numbers).
        /// </summary>
        /// <param name="tokens">Tokens to be returned when GetNextToken() is called</param>
        /// <param name="lineNumbers">Line numbers for each token</param>
        /// <returns>Mocked token parser</returns>
        internal static ITokenParser TokenParser(IEnumerable<Token> tokens,
            IList<int> lineNumbers = null)
        {
            if (lineNumbers == null)
            {
                lineNumbers = new List<int>();
                for (int i = 1; i <= tokens.Count() + 1; ++i)
                {
                    lineNumbers.Add(i);
                }
            }

            var mock = new Mock<ITokenParser>();

            var tokenQueue = new Queue<Token>(tokens);

            // Last token that ends the reading
            tokenQueue.Enqueue(null);

            mock
                .Setup(x => x.GetNextToken())
                .Returns(tokenQueue.Dequeue);

            mock
                .Setup(x => x.GetLine())
                .Returns(
                    lineNumbers.ElementAt(lineNumbers.Count - tokenQueue.Count));

            return mock.Object;
        }

        /// <summary>
        ///     Returns a TokenParser (not mock!) reading a given string.
        /// </summary>
        /// <param name="sourceString">Config data as string</param>
        /// <returns>TokenParser (real object)</returns>
        internal static ITokenParser TokenParser(string sourceString)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(sourceString ?? ""));
            return new TokenParser(new StreamReader(ms));
        }
    }
}