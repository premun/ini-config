using Config.Format;
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
					.AddOption("hostname", true)
					.AddOption("port", x => (int) x > 0 && (int) x < 65536, defaultValue: 3306)
					.AddOption("domain", typeof(Domains), defaultValue: Domains.Com)
				.AddSection("HTTP", true)
					.AddOption("timeout", defaultValue: 5000)
					.AddOption("use_https", defaultValue: false)
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
			serverOptions["port"].DefaultValue.ShouldBeEquivalentTo(3306);
			serverOptions["domain"].Should().BeOfType<EnumOptionSpecifier>();
			((EnumOptionSpecifier) serverOptions["domain"]).Enumeration.ShouldBeEquivalentTo(typeof(Domains));

			var httpSection = formatSpecifier.Sections["HTTP"];
			httpSection.Name.ShouldBeEquivalentTo("HTTP");
			httpSection.Required.ShouldBeEquivalentTo(true);

			var httpOptions = httpSection.Options;
			httpOptions.Count.ShouldBeEquivalentTo(2);
			httpOptions.ContainsKey("use_https").Should().BeTrue();
			httpOptions["use_https"].Required.Should().BeFalse();
			httpOptions["use_https"].DefaultValue.ShouldBeEquivalentTo(false);
		}
	}
}
