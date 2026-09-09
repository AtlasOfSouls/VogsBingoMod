/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace VogsBingoMod.Automarking
{
    public class SaveDataBool
    {
        internal const bool defaultValue = false;
        internal bool _value = defaultValue;
        public bool Value {get=>_value; set{_value = value;VogsBingoModPlugin.LogInfo($"Updated value {Name} to {_value}."); Automarker.CheckIfGoalsCompleted(this.GoalIDs);}}
        public GoalID[] GoalIDs;
        public string Name {get; private set;}
        public static implicit operator bool(SaveDataBool data) => data.Value;
        internal SaveDataBool(GoalID[] GoalIDs, string Name)
        {
            this.GoalIDs = GoalIDs;
            this.Name = Name;
        }
        public SaveDataBool(GoalID[] GoalIDs, string Name, bool Value)
        {
            this._value = Value;
            this.GoalIDs = GoalIDs;
            this.Name = Name;
        }
        internal void ResetToDefault()
        {
            this.Value = false;
        }
    }
}
