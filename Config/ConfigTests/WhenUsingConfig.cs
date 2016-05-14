using System.Linq;
using Config;
using Config.Format;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigTests
{
	[TestFixture]
	public class WhenUsingConfig
	{
		[Test]
		public void SectionOperationsShouldWork()
		{
			var specifier = new ConfigFormatSpecifier().AddSection("Foo").FinishDefinition();
			var config = new Config.Config(specifier);

			var section = config.AddSection("Foo");
			config["Foo"].ShouldBeEquivalentTo(section);
			((ConfigSection) section).FormatSpecifier.ShouldBeEquivalentTo(specifier["Foo"]);

			config.AddSection(section).ShouldBeEquivalentTo(true);
			config["Foo"].ShouldBeEquivalentTo(section);

			config.Sections.Count().ShouldBeEquivalentTo(1);

			config.RemoveSection(section);
			config.Sections.Count().ShouldBeEquivalentTo(0);
			config["Foo"].Should().BeNull();

			config.AddSection(section);
			config.Sections.Count().ShouldBeEquivalentTo(1);
			config.RemoveSection("Foo");
			config.Sections.Count().ShouldBeEquivalentTo(0);
		}
	}
}
