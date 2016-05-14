using Config.Format;
using Config.Format.OptionSpecifiers;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigTests.Format
{
	[TestFixture]
	public class WhenSpecifyingFormat
	{
		private enum Domains
		{
			Com,
			Eu,
			Fr
		}


		[Test]
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
			
			formatSpecifier["Server"].Should().NotBeNull();
			formatSpecifier["HTTP"].Should().NotBeNull();

			var serverSection = formatSpecifier["Server"];
			serverSection.Name.ShouldBeEquivalentTo("Server");
			serverSection.Required.ShouldBeEquivalentTo(true);

			serverSection["hostname"].Should().NotBeNull();
			serverSection["hostname"].Required.Should().BeTrue();
			serverSection["port"].Required.Should().BeFalse();
			((ConstraintOptionSpecifier<int>) serverSection["port"]).DefaultValue.ShouldBeEquivalentTo(3306);
			serverSection["domain"].Should().BeOfType<EnumOptionSpecifier<Domains>>();
			((EnumOptionSpecifier<Domains>) serverSection["domain"]).DefaultValue.ShouldBeEquivalentTo(Domains.Eu);

			var httpSection = formatSpecifier["HTTP"];
			httpSection.Name.ShouldBeEquivalentTo("HTTP");
			httpSection.Required.ShouldBeEquivalentTo(true);

			httpSection["use_https"].Should().NotBeNull();
			httpSection["use_https"].Required.Should().BeFalse();
			((BoolOptionSpecifier) httpSection["use_https"]).DefaultValue.ShouldBeEquivalentTo(null);
		}
	}
}
