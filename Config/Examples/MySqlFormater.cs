﻿using System.Collections.Generic;
using Config.Format;
using Config.Options;

namespace Examples
{
	/// <summary>
	/// Helper format specification used in other examples.
	/// Specification of a made up MySQL config ini file that is.
	/// </summary>
	public class MySqlFormater : IFormatSpecifier
	{
	    private readonly List<IFormatSectionSpecifier> _optionalSections = new List<IFormatSectionSpecifier>();
	    private readonly List<IFormatSectionSpecifier> _requiredSections = new List<IFormatSectionSpecifier>
	    {
	        new FormatSectionSpecifier("MySQL")
	        {
	            RequiredOptions = new List<IFormatOption>
	            {
	                new FormatOption<StringOption>("hostname", "localhost"),
	                new FormatOption<StringOption>("username"),
	                new FormatOption<StringOption>("password"),
	                new FormatOption<StringOption>("schema")
	            },
	            OptionalOptions = new List<IFormatOption>
	            {
	                new FormatOption<IntOption>("port", 3507)
	            }
	        }
	    };

	    #region Implementation of IFormatSpecifier

	    public List<IFormatSectionSpecifier> RequiredSections
	    {
	        get { return _requiredSections; }
	    }

	    public List<IFormatSectionSpecifier> OptionalSections
	    {
	        get { return _optionalSections; }
	    }

	    #endregion
	}
}