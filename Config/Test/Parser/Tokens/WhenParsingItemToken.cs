using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Config.Parser.Tokens;
using FluentAssertions;

namespace Test.Parser.Tokens
{
    [TestClass]
    public class WhenParsingItemToken
    {
        [TestMethod]
        public void ValidValuesShouldBeParsedOk()
        {
            ItemToken token;

            token = new ItemToken(StreamReaderFromString("foo=bar"));
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = new ItemToken(StreamReaderFromString("foo  = bar"));
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = new ItemToken(StreamReaderFromString("foo  = bar\n123 = 123"));
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = new ItemToken(StreamReaderFromString("foo  = =bar="));
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("=bar=");
        }

        [TestMethod]
        public void CommentedPartShouldNotBeParsed()
        {
            ItemToken token;

            token = new ItemToken(StreamReaderFromString("foo=bar; commentary"));
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = new ItemToken(StreamReaderFromString("foo  = bar ; commentary ; ;"));
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");
        }

        [TestMethod]
        [ExpectedException(typeof (FormatException))]
        public void InValidValuesShouldNotBeParsedOk()
        {
            new ItemToken(StreamReaderFromString("foo"));
        }

        [TestMethod]
        public void EscapedSpaceShouldBeParsedOk()
        {
            ItemToken token;

            token = new ItemToken(StreamReaderFromString("foo=bar \\ "));
            token.Name.ShouldBeEquivalentTo("foo");
            // TODO: maybe \\ should be gone?
            token.Value.ShouldBeEquivalentTo("bar \\ ");
        }

        private static StreamReader StreamReaderFromString(string s)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
            return new StreamReader(stream);
        }
    }
}
