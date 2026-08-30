/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace VogsBingoMod.Automarking
{
    internal class AutomarkerDataBool
    {
        internal const bool defaultValue = false;
        internal bool _value = defaultValue;
        internal bool Value {get=>_value; set{_value = value; Automarker.CheckIfGoalsCompleted(this.GoalIDs);}}
        internal GoalID[] GoalIDs;
        internal string Name {get; private set;}
        public static implicit operator bool(AutomarkerDataBool data) => data.Value;
        internal AutomarkerDataBool(GoalID[] goalIDs, string name, bool value = false)
        {
            this._value = value;
            this.GoalIDs = goalIDs;
            this.Name = name;
            AutomarkerData.bools.Add(this);
        }
        internal void ResetToDefault()
        {
            this.Value = false;
        }
    }
}
