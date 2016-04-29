using System.Collections.Generic;
using System.Linq;
using Config.IniFiles;
using Config.IniFiles.Parser;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConfigTests.IniFiles
{
	[TestClass]
	public class WhenBuildingConfig
	{
		[TestMethod]
		public void BasicParsingShouldWork()
		{
			var parser = GetTokenParserMock(new Token[]
			{
				new SectionHeaderToken {Name = "Foo"},
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				}
			});

			var builder = new IniFileConfigBuilder(parser);
			var config = builder.Build();

			var section = config["Foo"];
			section.Should().NotBeNull();
			section["foo"].String.ShouldBeEquivalentTo("bar");
		}

		private static ITokenParser GetTokenParserMock(IEnumerable<Token> tokens, IEnumerable<int> lineNumbers = null)
		{
			if (lineNumbers == null)
			{
				lineNumbers = new List<int>();
				for (int i = 0; i < tokens.Count(); ++i)
				{
					((List<int>) lineNumbers).Add(++i);
				}
			}

			var mock = new Mock<ITokenParser>();

			var tokenQueue = new Queue<Token>(tokens);
			var lineNumbersQueue = new Queue<int>(lineNumbers);

			mock.Setup(x => x.GetNextToken()).Returns(tokenQueue.Dequeue);
			mock.Setup(x => x.GetLine()).Returns(lineNumbersQueue.Dequeue);

			return mock.Object;
		}
	}
}
