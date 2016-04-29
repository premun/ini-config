using System;
using System.IO;
using System.Text;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigTests.IniFiles.Parser.Tokens
{
	[TestClass]
	public class WhenParsingItemToken
	{
		[TestMethod]
		public void ValidValuesShouldBeParsedOk()
		{
			OptionToken token;

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
			OptionToken token;

			token = ItemTokenFromString("foo=bar; commentary");
			token.Name.ShouldBeEquivalentTo("foo");
			token.Value.ShouldBeEquivalentTo("bar");

			token = ItemTokenFromString("foo  = bar ; commentary ; ;");
			token.Name.ShouldBeEquivalentTo("foo");
			token.Value.ShouldBeEquivalentTo("bar");
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void InValidValuesShouldNotBeParsedOk()
		{
			ItemTokenFromString("foo");
		}

		[TestMethod]
		public void EscapedSpaceShouldBeParsedOk()
		{
			OptionToken token;

			token = ItemTokenFromString(@"foo=bar \ ");
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

		[TestMethod]
		public void EscapedSemicolonShouldBeParsedOk()
		{
			OptionToken token;

			token = ItemTokenFromString(@"foo=ba\;\;r\; \ ");
			token.Name.ShouldBeEquivalentTo("foo");
			token.Value.ShouldBeEquivalentTo("ba;;r;  ");

			token = ItemTokenFromString(@"foo= \ \ bar \ \;commenteray ");
			token.Name.ShouldBeEquivalentTo("foo");
			token.Value.ShouldBeEquivalentTo(@"  bar  ;commenteray");
		}

		private static OptionToken ItemTokenFromString(string s)
		{
			var ms = new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
			var sr = new StreamReader(ms);
			return new OptionToken(sr);
		}
	}
}
