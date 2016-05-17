using System;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigTests.IniFiles.Parser
{
    [TestFixture]
    public class WhenParsingFile
    {
        [Test]
        public void BasicCaseShouldBeParsedOk()
        {
            const string content = "[foo]\n" +
                                   "bar = 123 ; commentary\n" +
                                   "  xyz =456 \n" +
                                   "[foo 2]\n" +
                                   " abc =   def;\n" +
                                   "; another=commentary\n" +
                                   "\n";

            using (var parser = MockFactory.TokenParser(content))
            {
                var token = parser.GetNextToken();
                token.Should().BeOfType<SectionHeaderToken>();
                token.As<SectionHeaderToken>().Name.ShouldBeEquivalentTo("foo");

                token = parser.GetNextToken();
                token.Should().BeOfType<OptionToken>();
                token.As<OptionToken>().Name.ShouldBeEquivalentTo("bar");
                token.As<OptionToken>().Value.ShouldBeEquivalentTo("123");

                token = parser.GetNextToken();
                token.Should().BeOfType<OptionToken>();
                token.As<OptionToken>().Name.ShouldBeEquivalentTo("xyz");
                token.As<OptionToken>().Value.ShouldBeEquivalentTo("456");

                token = parser.GetNextToken();
                token.Should().BeOfType<SectionHeaderToken>();
                token.As<SectionHeaderToken>()
                    .Name.ShouldBeEquivalentTo("foo 2");

                token = parser.GetNextToken();
                token.Should().BeOfType<OptionToken>();
                token.As<OptionToken>().Name.ShouldBeEquivalentTo("abc");
                token.As<OptionToken>().Value.ShouldBeEquivalentTo("def");

                token = parser.GetNextToken();
                token.Should().BeOfType<CommentToken>();
                token.As<CommentToken>()
                    .Content.ShouldBeEquivalentTo("another=commentary");

                Assert.IsNull(parser.GetNextToken());
                Assert.IsNull(parser.GetNextToken());
            }
        }


        [Test]
        [ExpectedException(typeof (FormatException))]
        public void MissingEqualSignShouldNotBeParsedOk()
        {
            const string content = "[foo] ;commentary\n" +
                                   "bar123\n";

            using (var parser = MockFactory.TokenParser(content))
            {
                var token = parser.GetNextToken();
                token.Should().BeOfType<SectionHeaderToken>();
                token.As<SectionHeaderToken>().Name.ShouldBeEquivalentTo("foo");

                token = parser.GetNextToken();
                token.Should().BeOfType<CommentToken>();
                token.As<CommentToken>()
                    .Content.ShouldBeEquivalentTo("commentary");

                parser.GetNextToken();
            }
        }


        [Test]
        [ExpectedException(typeof (FormatException))]
        public void UnmatchedBracketsShouldNotBeParsedOk()
        {
            const string content = "[foo\n" +
                                   "bar = 123 ; commentary\n";

            using (var parser = MockFactory.TokenParser(content))
            {
                parser.GetNextToken();
            }
        }
    }
}