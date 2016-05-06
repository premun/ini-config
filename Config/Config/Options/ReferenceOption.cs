﻿using System;

namespace Config.Options
{
	public class ReferenceOption : Option
	{
		public string Section { get; set; }

		public string Option { get; set; }

	    private readonly IConfig _parrentConfig;

		public ReferenceOption(string sectionName, string optionName, IConfig config)
		{
		    _parrentConfig = config;
		    Section = sectionName;
		    Option = optionName;
		}

	    #region Overrides of Option

	    public override object Data
	    {
	        get
	        {
	            return _parrentConfig[Section][Option].Data;
	        }
	        protected set
	        {
	            throw new InvalidOperationException("Cannot set value to reference option.");
	        }
	    }

	    #endregion
	}
}