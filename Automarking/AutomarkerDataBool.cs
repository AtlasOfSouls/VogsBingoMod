/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace VogsBingoMod.Automarking
{
    internal class SaveDataBool
    {
        internal const bool defaultValue = false;
        internal bool _value = defaultValue;
        internal bool Value {get=>_value; set{_value = value; Automarker.CheckIfGoalsCompleted(this.GoalIDs);}}
        internal GoalID[] GoalIDs;
        internal string Name {get; private set;}
        public static implicit operator bool(SaveDataBool data) => data.Value;
        internal SaveDataBool(GoalID[] goalIDs, string name, bool value = false)
        {
            this._value = value;
            this.GoalIDs = goalIDs;
            this.Name = name;
        }
        internal void ResetToDefault()
        {
            this.Value = false;
        }
    }
}
