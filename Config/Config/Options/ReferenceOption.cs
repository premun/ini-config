using System;
using System.Linq;
using Config.ConfigExceptions;

namespace Config.Options
{
	public sealed class ReferenceOption : Option
	{
		public string Section { get; set; }

		public string Option { get; set; }

	    private readonly IConfig _parrentConfig;

	    private bool _cycleIndicator;

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
	            if (_cycleIndicator)
	            {
	                throw new ReferenceCycleException(Section, Option);
	            }

		        if (!_parrentConfig[Section].Keys().Contains(Option))
		        {
			        throw new MissingReferencedException(Section, Option);
		        }

	            _cycleIndicator = true;
                var result = _parrentConfig[Section][Option].Data;
                _cycleIndicator = false;

                return result;
	        }
	        protected set
	        {
	            throw new InvalidOperationException("Cannot set value to reference option.");
	        }
	    }

	    #endregion
	}
}