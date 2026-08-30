/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace SilksongBingoMod.Automarking
{
    internal class AutomarkerDataInt
    {
        internal string Name;
        internal int _value;
        internal int Value {get=>_value; set{_value=value; Automarker.CheckIfGoalsCompleted(AutomarkValues, _value);}}
        internal AutomarkIntValue[] AutomarkValues;
        public static implicit operator int(AutomarkerDataInt data) => data.Value;
        internal AutomarkerDataInt(AutomarkIntValue[] automarkValues, string name, int startValue = 0)
        {
            this._value = startValue;
            this.AutomarkValues = automarkValues;
            this.Name = name;
            AutomarkerData.ints.Add(this);
        }
        internal void ResetToDefault()
        {
            this.Value = 0;
        }
    }
}
