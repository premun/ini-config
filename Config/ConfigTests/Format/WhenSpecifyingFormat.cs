using Config.Format;
using Config.Format.OptionSpecifiers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigTests.Format
{
	[TestClass]
	public class WhenSpecifyingFormat
	{
		private enum Domains
		{
			Com,
			Eu,
			Fr
		}

		[TestMethod]
		public void SpecificationShouldBeSaved()
		{
			var formatSpecifier = new ConfigFormatSpecifier()
			.AddSection("Server", true)
				.AddOption(new StringOptionSpecifier("hostname", true))
				.AddOption(new ConstraintOptionSpecifier<int>("port", x => x > 0 && x < 65536, defaultValue: 3306))
				.AddOption(new EnumOptionSpecifier<Domains>("domain", defaultValue: Domains.Eu))
			.AddSection("HTTP", true)
				.AddOption(new IntOptionSpecifier("timeout", defaultValue: 5000))
				.AddOption(new BoolOptionSpecifier("use_https"))
			.FinishDefinition();

			formatSpecifier.Sections.Count.ShouldBeEquivalentTo(2);
			formatSpecifier.Sections.ContainsKey("Server").Should().BeTrue();
			formatSpecifier.Sections.ContainsKey("HTTP").Should().BeTrue();

			var serverSection = formatSpecifier.Sections["Server"];
			serverSection.Name.ShouldBeEquivalentTo("Server");
			serverSection.Required.ShouldBeEquivalentTo(true);

			var serverOptions = serverSection.Options;
			serverOptions.Count.ShouldBeEquivalentTo(3);
			serverOptions.ContainsKey("hostname").Should().BeTrue();
			serverOptions["hostname"].Required.Should().BeTrue();
			serverOptions["port"].Required.Should().BeFalse();
			((ConstraintOptionSpecifier<int>) serverOptions["port"]).DefaultValue.ShouldBeEquivalentTo(3306);
			serverOptions["domain"].Should().BeOfType<EnumOptionSpecifier<Domains>>();
			((EnumOptionSpecifier<Domains>) serverOptions["domain"]).DefaultValue.ShouldBeEquivalentTo(Domains.Eu);

			var httpSection = formatSpecifier.Sections["HTTP"];
			httpSection.Name.ShouldBeEquivalentTo("HTTP");
			httpSection.Required.ShouldBeEquivalentTo(true);

			var httpOptions = httpSection.Options;
			httpOptions.Count.ShouldBeEquivalentTo(2);
			httpOptions.ContainsKey("use_https").Should().BeTrue();
			httpOptions["use_https"].Required.Should().BeFalse();
			((BoolOptionSpecifier) httpOptions["use_https"]).DefaultValue.ShouldBeEquivalentTo(false);
		}
	}
}
