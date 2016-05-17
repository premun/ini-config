using System;
using System.IO;
using System.Text;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigTests.IniFiles.Parser.Tokens
{
    [TestFixture]
    public class WhenParsingOptionToken
    {
        private static OptionToken ItemTokenFromString(string s)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
            var sr = new StreamReader(ms);
            return OptionToken.FromStream(sr);
        }

        [Test]
        public void CommentedPartShouldNotBeParsed()
        {
            var token = ItemTokenFromString("foo=bar; commentary");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");

            token = ItemTokenFromString("foo  = bar ; commentary ; ;");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar");
        }

        [Test]
        public void EscapedSemicolonShouldBeParsedOk()
        {
            var token = ItemTokenFromString(@"foo=ba\;\;r\; \ ");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("ba;;r;  ");

            token = ItemTokenFromString(@"foo= \ \ bar \ \;commenteray ");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo(@"  bar  ;commenteray");
        }

        [Test]
        public void EscapedSpaceShouldBeParsedOk()
        {
            var token = ItemTokenFromString(@"foo=bar \ ");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar  ");

            token = ItemTokenFromString(@"foo=bar \ ");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar  ");

            token = ItemTokenFromString(@"foo= \ bar \ \ ");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo(" bar   ");

            token = ItemTokenFromString(@"foo= bar \ \  ; commentary");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo("bar   ");

            token = ItemTokenFromString(@"foo=  bar \ \");
            token.Name.ShouldBeEquivalentTo("foo");
            token.Value.ShouldBeEquivalentTo(@"bar  \");
        }

        [Test]
        [ExpectedException(typeof (FormatException))]
        public void InValidValuesShouldNotBeParsedOk()
        {
            ItemTokenFromString("foo");
        }

        [Test]
        public void ValidValuesShouldBeParsedOk()
        {
            var token = ItemTokenFromString("foo=bar");
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
    }
}