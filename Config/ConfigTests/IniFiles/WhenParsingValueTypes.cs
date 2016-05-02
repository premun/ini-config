using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Config.Format;
using Config.Format.OptionSpecifiers;
using Config.IniFiles;
using Config.IniFiles.Parser;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigTests.IniFiles
{
	[TestClass]
	public class WhenParsingValueTypes
	{
		private enum Colors
		{
			Black,
			White,
			Green
		}

		private readonly ConfigFormatSpecifier _formatSpecifier = new ConfigFormatSpecifier()
			.AddSection("Scalars")
				.AddOption(new BoolOptionSpecifier("bool", defaultValue: true))
				.AddOption(new FloatOptionSpecifier("float", defaultValue: 0.75f))
				.AddOption(new EnumOptionSpecifier<Colors>("enum", defaultValue: Colors.Green))
				.AddOption(new IntOptionSpecifier("int", defaultValue: 4))
				.AddOption(new SignedOptionSpecifier("signed", defaultValue: 40L))
				.AddOption(new StringOptionSpecifier("string", defaultValue: "foo"))
				.AddOption(new UnsignedOptionSpecifier("unsigned", defaultValue: 50L))
			.AddSection("Constraints")
				.AddOption(new ConstraintOptionSpecifier<int>("int", x => 0 < x && x < 50, defaultValue: 1))
				.AddOption(new ConstraintOptionSpecifier<Colors>("enum", x => x != Colors.Black, defaultValue: Colors.White))
			.AddSection("Lists")
				.AddOption(new ListOptionSpecifier<int>("ints", defaultValue: new[] { 6, 9, 42 }))
			.FinishDefinition();

		[TestMethod]
		public void ParsingScalarsShouldWork()
		{
			const string configData = @"
[Scalars]
bool = f ; bool commentary
float = 1.2
enum = Black
int=100
signed=60
string=  bar ;xyz
unsigned = 70 
";
			var parser = GetTokenParser(configData);
			var builder = new IniFileConfigBuilder(parser);
			var config = builder.Build(_formatSpecifier);

			builder.Ok.Should().BeTrue();

			config.Sections.Count().ShouldBeEquivalentTo(1);
			var section = config["Scalars"];
			
			section["bool"].Bool.ShouldBeEquivalentTo(false);
			section["float"].Float.ShouldBeEquivalentTo(1.2f);
			((Colors) section["enum"].Data).ShouldBeEquivalentTo(Colors.Black); // TODO: ziskavani enumu asi neni uplne dobry
			section["int"].Int.ShouldBeEquivalentTo(100);
			section["signed"].Signed.ShouldBeEquivalentTo(60L);
			section["string"].String.ShouldBeEquivalentTo("bar");
			section["unsigned"].Unsigned.ShouldBeEquivalentTo(70L);
		}

		[TestMethod]
		public void ParsingConstraintsShouldWork()
		{
			const string configData = @"
[Constraints]
int = 40
enum = Green 
";
			var parser = GetTokenParser(configData);
			var builder = new IniFileConfigBuilder(parser);
			var config = builder.Build(_formatSpecifier);

			builder.Ok.Should().BeTrue();

			config.Sections.Count().ShouldBeEquivalentTo(1);
			var section = config["Constraints"];

			section["int"].Int.ShouldBeEquivalentTo(40);
			((Colors) section["enum"].Data).ShouldBeEquivalentTo(Colors.Green);
		}

		[TestMethod]
		public void ParsingListsShouldWork()
		{
			const string configData = @"
[Lists]
ints = 40, 50
enums = Green, Black, Black
";
			var parser = GetTokenParser(configData);
			var builder = new IniFileConfigBuilder(parser);
			var config = builder.Build(_formatSpecifier);

			builder.Ok.Should().BeTrue();

			config.Sections.Count().ShouldBeEquivalentTo(1);
			var section = config["Lists"];

			var ints = section["ints"].IntList;
			ints.ElementAt(0).ShouldBeEquivalentTo(40);
			ints.ElementAt(1).ShouldBeEquivalentTo(50);
		}

		private static ITokenParser GetTokenParser(string configData)
		{
			var parser = MockFactory.TokenParser(configData);
			IList<Token> tokens = new List<Token>();
			Token token;
			while ((token = parser.GetNextToken()) != null)
			{
				tokens.Add(token);
			}

			return MockFactory.TokenParser(tokens);
		}
	}
}
