using System;
using System.Linq;
using Config;
using Config.IniFiles;
using Config.IniFiles.Errors;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigTests.IniFiles
{
	[TestClass]
	public class WhenBuildingInvalidConfig
	{
		[TestMethod]
		public void RelaxedBuildModeShouldNotThrow()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new SectionHeaderToken {Name = "Foo"},
				new SectionHeaderToken {Name = "Foo"}
			});

			var builder = new IniFileConfigBuilder(parser);
			builder.Build();

			builder.Ok.Should().BeFalse();
			builder.Errors.First().Should().BeOfType<DuplicateSectionError>();
		}

		[TestMethod]
		public void StrictBuildModeShouldNotThrow()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				}
			});

			var builder = new IniFileConfigBuilder(parser);
			Action build = () => {
				builder.Build(buildMode: BuildMode.Strict);
			};

			build.ShouldThrow<IniConfigException>();

			builder.Ok.Should().BeFalse();
			builder.Errors.First().Should().BeOfType<NoSectionError>();
		}

		[TestMethod]
		public void DuplicateSectionShouldRaiseError()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new SectionHeaderToken {Name = "Foo"},
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				},
				new SectionHeaderToken {Name = "Foo"}
			});

			var builder = new IniFileConfigBuilder(parser);

			Action build = () => {
				builder.Build(buildMode: BuildMode.Strict);
			};

			build.ShouldThrow<IniConfigException>().And.Errors.First().Should().BeOfType<DuplicateSectionError>();
			builder.Ok.Should().BeFalse();
		}

		[TestMethod]
		public void InvalidIdentifierShouldRaiseError()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new SectionHeaderToken {Name = "Foo"},
				new OptionToken
				{
					Name = "foo!",
					Value = "bar"
				}
			});

			var builder = new IniFileConfigBuilder(parser);

			Action build = () => {
				builder.Build(buildMode: BuildMode.Strict);
			};

			build.ShouldThrow<IniConfigException>().And.Errors.First().Should().BeOfType<InvalidIdentifierError>();
			builder.Ok.Should().BeFalse();
		}

		[TestMethod]
		public void NoSectionShouldRaiseError()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				},
				new SectionHeaderToken {Name = "Foo"}
			});

			var builder = new IniFileConfigBuilder(parser);

			Action build = () => {
				builder.Build(buildMode: BuildMode.Strict);
			};

			build.ShouldThrow<IniConfigException>();
			builder.Ok.Should().BeFalse();
			builder.Errors.First().Should().BeOfType<NoSectionError>();
		}
	}
}
