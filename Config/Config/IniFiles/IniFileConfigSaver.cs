using System.IO;
using System.Linq;
using Config.Options;

namespace Config.IniFiles
{
	/// <summary>
	/// Example stub of an object that will store config into an ini file.
	/// </summary>
	/// TODO: Turn into internal?
	public class IniFileConfigSaver : IConfigSaver
	{
		private readonly string _path;

		private Verbosity _verbosity;

		private TextWriter _writer;

		public IniFileConfigSaver(string path = null)
		{
			_path = path;
		}

		public void SaveConfig(IConfig config, Verbosity verbosity)
		{
			using (var writer = new StreamWriter(_path))
			{
				SaveConfig(writer, config, verbosity);
			}
		}

		public void SaveConfig(TextWriter writer, IConfig config, Verbosity verbosity)
		{
			_verbosity = verbosity;
			_writer = writer;

			config.Sections.ToList().ForEach(WriteSection);
		}

		private void WriteSection(IConfigSection section)
		{
			_writer.WriteLine("[" + section.Name + "]");

			if (CommentsFlag && !string.IsNullOrEmpty(section.Comment))
			{
				_writer.WriteLine("; " + section.Comment);
			}

			foreach (var key in section.Keys(DefaultsFlag))
			{
				WriteSection(key, section[key]);
			}

			_writer.WriteLine();
		}

		private void WriteSection(string key, Option option)
		{
			_writer.Write("{0} = {1}", key, option.Serialize());

			if (CommentsFlag && !string.IsNullOrEmpty(option.Comment))
			{
				_writer.WriteLine("\t; " + option.Comment);
			}

			_writer.WriteLine();
		}

		private bool CommentsFlag
		{
			get { return (_verbosity & Verbosity.Comments) == Verbosity.Comments; }
		}

		private bool DefaultsFlag
		{
			get { return (_verbosity & Verbosity.Defaults) == Verbosity.Defaults; }
		}
	}
}