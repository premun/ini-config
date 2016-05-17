namespace Config.Options.NumericValues
{
    public class DecimalValue : NumericValue
    {
        private readonly string _value;

        public DecimalValue(string value)
        {
            _value = value;
        }

        #region Overrides of NumericValue<int>

        public override string StringValue
        {
            get { return _value; }
        }

        public override int NumericSystem
        {
            get { return 10; }
        }

        #endregion
    }
}