using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Config.Parser.Tokens;
using FluentAssertions;

namespace Test.Parser.Tokens
{
    [TestClass]
    public class WhenParsingSectionHeaderToken
    {
        [TestMethod]
        public void ValidValuesShouldBeParsedOk()
        {
            SectionHeaderToken token;

            token = new SectionHeaderToken(StreamReaderFromString("[foo]"));
            token.Name.ShouldBeEquivalentTo("foo");

            token = new SectionHeaderToken(StreamReaderFromString("foo]"));
            token.Name.ShouldBeEquivalentTo("foo");

            token = new SectionHeaderToken(StreamReaderFromString("[foo bar]]"));
            token.Name.ShouldBeEquivalentTo("foo bar");

            token = new SectionHeaderToken(StreamReaderFromString("[foo]\n123 = 123"));
            token.Name.ShouldBeEquivalentTo("foo");

            token = new SectionHeaderToken(StreamReaderFromString("[foo]  [bar]"));
            token.Name.ShouldBeEquivalentTo("foo");
        }

        [TestMethod]
        public void CommentedPartShouldNotBeParsed()
        {
            SectionHeaderToken token;

            token = new SectionHeaderToken(StreamReaderFromString("[foo]; commentary"));
            token.Name.ShouldBeEquivalentTo("foo");

            token = new SectionHeaderToken(StreamReaderFromString("[foo] ; commentary ; ;"));
            token.Name.ShouldBeEquivalentTo("foo");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void UnmatchedBracketShouldNotBeParsedOk()
        {
            new SectionHeaderToken(StreamReaderFromString("[foo bar\n123 = 456"));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EmptyNameShouldNotBeParsedOk()
        {
            new SectionHeaderToken(StreamReaderFromString("[]"));
        }

        private static StreamReader StreamReaderFromString(string s)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
            return new StreamReader(stream);
        }
    }
}
