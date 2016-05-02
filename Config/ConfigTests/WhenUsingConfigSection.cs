using System.Linq;
using Config.Format;
using Config.Format.OptionSpecifiers;
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

		[TestMethod]
		public void GettingOptionKeysShouldWork()
		{
			var config = new Config.Config();

			var section = config.AddSection("Foo");
			section
				.Set("foo", 3)
				.Set("bar", "xyz");

			var keys = section.Keys();
			keys.ShouldBeEquivalentTo(new[] { "foo", "bar" });

			var formatSpecifier = new ConfigFormatSpecifier()
				.AddSection("Foo")
					.AddOption(new FloatOptionSpecifier("float", defaultValue: 0.4f))
					.AddOption(new IntOptionSpecifier("int", defaultValue: 4))
					.AddOption(new BoolOptionSpecifier("bool"))
				.FinishDefinition();

			config = new Config.Config(formatSpecifier);

			section = config.AddSection("Foo");
			section
				.Set("foo", 3)
				.Set("bar", "xyz");

			keys = section.Keys(true);
			keys.ShouldBeEquivalentTo(new[] { "foo", "bar", "float", "int" });

			keys = section.Keys(false);
			keys.ShouldBeEquivalentTo(new[] { "foo", "bar" });
		}
	}
}
