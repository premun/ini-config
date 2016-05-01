using System.Linq;
using Config;
using Config.Format;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigTests
{
	[TestClass]
	public class WhenUsingConfigSection
	{
		[TestMethod]
		public void OptionOperationsShouldWork()
		{
			var specifier = new ConfigFormatSpecifier().AddSection("Foo").FinishDefinition();
			var config = new Config.Config(specifier);

			var section = config.AddSection("Foo");
			section
				.Set("foo", 3)
				.Set("bar", "xyz");

			section.Count().ShouldBeEquivalentTo(2);
			section["foo"].Int.ShouldBeEquivalentTo(3);
			section["bar"].String.ShouldBeEquivalentTo("xyz");

			const float f = 4.2f;
			section["foo"] = f;
			section["foo"].Float.ShouldBeEquivalentTo(f);

			section.Remove("123").Should().BeFalse();
			section.Count().ShouldBeEquivalentTo(2);
			section.Remove("bar").Should().BeTrue();
			section.Count().ShouldBeEquivalentTo(1);
			section["foo"].Float.ShouldBeEquivalentTo(f);
		}
	}
}
