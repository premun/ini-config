using System;
using System.IO;
using System.Text;
using Config.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Config.Parser.Tokens;
using FluentAssertions;

namespace Test.Parser
{
    [TestClass]
    public class WhenParsingFile
    {
        [TestMethod]
        public void BasicCaseShouldBeParsedOk()
        {
            string content = "[foo]\n" +
                             "bar = 123 ; commentary\n" +
                             "  xyz =456 \n" +
							 "[foo 2]\n" +
							 " abc =   def;\n" +
                             "; another=commentary\n" +
                             "\n";

            TokenParser parser = TokenParserFromString(content);

			var token = parser.GetNextToken();
			token.Should().BeOfType<SectionHeaderToken>();
			token.As<SectionHeaderToken>().Name.ShouldBeEquivalentTo("foo");

			token = parser.GetNextToken();
			token.Should().BeOfType<ItemToken>();
			token.As<ItemToken>().Name.ShouldBeEquivalentTo("bar");
			token.As<ItemToken>().Value.ShouldBeEquivalentTo("123");

            token = parser.GetNextToken();
            token.Should().BeOfType<ItemToken>();
            token.As<ItemToken>().Name.ShouldBeEquivalentTo("xyz");
			token.As<ItemToken>().Value.ShouldBeEquivalentTo("456");
			
			token = parser.GetNextToken();
			token.Should().BeOfType<SectionHeaderToken>();
			token.As<SectionHeaderToken>().Name.ShouldBeEquivalentTo("foo 2");

			token = parser.GetNextToken();
			token.Should().BeOfType<ItemToken>();
			token.As<ItemToken>().Name.ShouldBeEquivalentTo("abc");
			token.As<ItemToken>().Value.ShouldBeEquivalentTo("def");

            token = parser.GetNextToken();
            token.Should().BeOfType<CommentToken>();
            token.As<CommentToken>().Content.ShouldBeEquivalentTo("; another=commentary");

            Assert.IsNull(parser.GetNextToken());
            Assert.IsNull(parser.GetNextToken());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void UnmatchedBracketsShouldNotBeParsedOk()
        {
            string content = "[foo\n" +
                             "bar = 123 ; commentary\n";

            TokenParser parser = TokenParserFromString(content);
            parser.GetNextToken();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void MissingEqualSignShouldNotBeParsedOk()
        {
            string content = "[foo] ;commentary\n" +
                             "bar123\n";

            TokenParser parser = TokenParserFromString(content);

            var token = parser.GetNextToken();
            token.Should().BeOfType<SectionHeaderToken>();
            token.As<SectionHeaderToken>().Name.ShouldBeEquivalentTo("foo");

            token = parser.GetNextToken();
            token.Should().BeOfType<CommentToken>();
            token.As<CommentToken>().Content.ShouldBeEquivalentTo(";commentary");

            token = parser.GetNextToken();
        }

        private static TokenParser TokenParserFromString(string s)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
            var sr = new StreamReader(ms);
            return new TokenParser(sr);
        }
    }
}
