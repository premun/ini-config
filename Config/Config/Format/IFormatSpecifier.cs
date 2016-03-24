namespace Config.Format
{
	public interface IFormatSpecifier
	{
		void SetOptional(string sectionName);
		void SetRequired(string sectionaName);

		void SetOptional(string sectionName, string itemKey);
		void SetRequired(string sectionaName, string itemKey);
	}
}