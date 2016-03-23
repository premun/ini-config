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

            token = ItemTokenFromString("foo=bar");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = ItemTokenFromString("foo  = bar");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = ItemTokenFromString("foo  = bar\n123 = 123");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = ItemTokenFromString("foo  = =bar=");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("=bar=");
        }

        [TestMethod]
        public void CommentedPartShouldNotBeParsed()
        {
            ItemToken token;

            token = ItemTokenFromString("foo=bar; commentary");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = ItemTokenFromString("foo  = bar ; commentary ; ;");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");
        }

        [TestMethod]
        [ExpectedException(typeof (FormatException))]
        public void InValidValuesShouldNotBeParsedOk()
        {
            ItemTokenFromString("foo");
        }

        [TestMethod]
        public void EscapedSpaceShouldBeParsedOk()
        {
            ItemToken token;

            token = ItemTokenFromString(@"foo=bar \ ");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar  ");

            token = ItemTokenFromString(@"foo=bar \ \ ");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar   ");

            token = ItemTokenFromString(@"foo=bar \ \  ; commentary");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar   ");
            
            token = ItemTokenFromString(@"foo=bar \ \");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo(@"bar  \");

            token = ItemTokenFromString(@"foo=bar \ \;commenteray ");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo(@"bar  \");
        }

        private static ItemToken ItemTokenFromString(string s)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
            var sr = new StreamReader(ms);
            return new ItemToken(sr);
        }
    }
}
