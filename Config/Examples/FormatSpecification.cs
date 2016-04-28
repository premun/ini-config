using Config.Format;

namespace Examples
{
	/// <summary>
	/// Example of how a config file format can be specified.
	/// There are two required sections (Server, HTTP) and one optional (Paths).
	/// </summary>
	public class FormatSpecification
    {
		public enum Domains
		{
			Com,
			Eu,
			Fr
		}

		public readonly ConfigFormatSpecifier FormatSpecifier = new ConfigFormatSpecifier()
			.AddSection("Server", true)
				.AddOption("hostname", true)
				.AddOption("port", x => (int) x > 0 && (int) x < 65536, defaultValue: 3306)
				.AddOption("domain", typeof (Domains), defaultValue: Domains.Com)
			.AddSection("HTTP", true)
				.AddOption("timeout", defaultValue: 5000)
				.AddOption("use_https", defaultValue: false)
			.FinishDefinition();
    }
}
