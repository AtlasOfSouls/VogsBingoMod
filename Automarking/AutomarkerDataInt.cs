/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace VogsBingoMod.Automarking
{
    internal class SaveDataInt
    {
        internal string Name;
        internal int _value;
        internal int Value {get=>_value; set
        {
            _value=value;
            VogsBingoModPlugin.LogInfo($"Updated value {Name} to {_value}.");
            foreach (AutomarkIntValue automarkIntValue in AutomarkValues)
            {
                VogsBingoModPlugin.LogInfo($"Goal {Name} marks at {automarkIntValue.markValue} for goal {automarkIntValue.goalToMark}");
            }
            Automarker.CheckIfGoalsCompleted(AutomarkValues, _value);
        }}
        internal AutomarkIntValue[] AutomarkValues;
        public static implicit operator int(SaveDataInt data) => data.Value;
        internal SaveDataInt(AutomarkIntValue[] automarkValues, string name, int startValue = 0)
        {
            this._value = startValue;
            this.AutomarkValues = automarkValues;
            this.Name = name;
        }
        internal void ResetToDefault()
        {
            this.Value = 0;
        }
    }
}
