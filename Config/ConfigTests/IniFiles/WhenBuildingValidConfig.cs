using Config;
using Config.IniFiles;
using Config.IniFiles.Parser.Tokens;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigTests.IniFiles
{
	[TestClass]
	public class WhenBuildingValidConfig
	{
		[TestMethod]
		public void BasicParsingShouldWork()
		{
			var parser = MockFactory.TokenParser(new Token[]
			{
				new SectionHeaderToken {Name = "Foo"},
				new OptionToken
				{
					Name = "foo",
					Value = "bar"
				},
				new OptionToken
				{
					Name = "foo 1",
					Value = "bar 1"
				},
				new SectionHeaderToken {Name = "Foo 2"},
				new OptionToken
				{
					Name = "foo 2",
					Value = "bar 2"
				}
			});

			var builder = new IniFileConfigBuilder(parser);
			var config = builder.Build(buildMode: BuildMode.Strict);

			builder.Ok.Should().BeTrue();

			config["Foo"].Should().NotBeNull();
			config["Foo"]["foo"].String.ShouldBeEquivalentTo("bar");
			config["Foo"]["foo 1"].String.ShouldBeEquivalentTo("bar 1");
			config["Foo 2"].Should().NotBeNull();
			config["Foo 2"]["foo 2"].String.ShouldBeEquivalentTo("bar 2");
		}
	}
}
