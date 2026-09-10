/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using Newtonsoft.Json;

namespace VogsBingoMod.Automarking
{
    public class SaveDataInt
    {
        public string Name;
        internal int _value;
        public int Value {get=>_value; set
        {
            _value=value;
            VogsBingoModPlugin.LogInfo($"Updated value {Name} to {_value}.");
            foreach (AutomarkIntValue automarkIntValue in AutomarkValues)
            {
                VogsBingoModPlugin.LogInfo($"Goal {Name} marks at {automarkIntValue.markValue} for goal {automarkIntValue.goalToMark}");
            }
            Automarker.CheckIfGoalsCompleted(AutomarkValues, _value);
        }}
        public AutomarkIntValue[] AutomarkValues;
        public static implicit operator int(SaveDataInt data) => data.Value;
        public SaveDataInt(AutomarkIntValue[] AutomarkValues, string Name)
        {
            this.AutomarkValues = AutomarkValues;
            this.Name = Name;
        }

        [JsonConstructor]
        public SaveDataInt(AutomarkIntValue[] AutomarkValues, string Name, int Value)
        {
            this._value = Value;
            this.AutomarkValues = AutomarkValues;
            this.Name = Name;
        }

        internal void ResetToDefault()
        {
            this.Value = 0;
        }
    }
}
