﻿using System;
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
		[Test]
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

		[Test]
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

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void InValidValuesShouldNotBeParsedOk()
		{
			ItemTokenFromString("foo");
		}

		[Test]
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

		[Test]
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
			return OptionToken.FromStream(sr);
		}
	}
}
