using System;
using System.IO;
using System.Text;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigTests.IniFiles.Parser.Tokens
{
	[TestFixture]
	public class WhenParsingSectionHeaderToken
	{
		[Test]
		public void ValidValuesShouldBeParsedOk()
		{
			SectionHeaderToken token;

			token = TokenFromString("[foo]");
			token.Name.ShouldBeEquivalentTo("foo");

			token = TokenFromString("foo]");
			token.Name.ShouldBeEquivalentTo("foo");

			token = TokenFromString("[foo bar]]");
			token.Name.ShouldBeEquivalentTo("foo bar");

			token = TokenFromString("[foo]\n123 = 123");
			token.Name.ShouldBeEquivalentTo("foo");

			token = TokenFromString("[foo]  [bar]");
			token.Name.ShouldBeEquivalentTo("foo");
		}

		[Test]
		public void CommentedPartShouldNotBeParsed()
		{
			SectionHeaderToken token;

			token = TokenFromString("[foo]; commentary");
			token.Name.ShouldBeEquivalentTo("foo");

			token = TokenFromString("[foo] ; commentary ; ;");
			token.Name.ShouldBeEquivalentTo("foo");
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void UnmatchedBracketShouldNotBeParsedOk()
		{
			TokenFromString("[foo bar\n123 = 456");
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void EmptyNameShouldNotBeParsedOk()
		{
			TokenFromString("[]");
		}

		private static SectionHeaderToken TokenFromString(string s)
		{
			var ms = new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
			var sr = new StreamReader(ms);
			return SectionHeaderToken.FromStream(sr);
		}
	}
}
