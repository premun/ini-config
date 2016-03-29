using System.Collections.Generic;

namespace Config.Format
{
    /// <summary>
    /// Enables user to specify the format 
    /// </summary>
    public interface IFormatSpecifier : IList<IFormatSectionSpecifier>
    {
        void SetSection(IFormatSectionSpecifier section, bool required = true);

        /*
        void SetOptional(string sectionName);
		void SetRequired(string sectionaName);

		void SetOptional(string sectionName, string itemKey);
		void SetRequired(string sectionaName, string itemKey);
        
        TODO: metody na vraceni vsech pozadavku, aby je validator moh pouzit
        */
    }
}