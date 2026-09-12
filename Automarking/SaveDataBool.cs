/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace VogsBingoMod.Automarking
{
    internal class SaveDataBool
    {
        internal bool Value {get=> this.GetValue(); set{SetValue(value);}}
        internal GoalID[] GoalIDs;
        internal string Name {get; private set;}
        public static implicit operator bool(SaveDataBool data) => data.Value;
        internal SaveDataBool(GoalID[] GoalIDs, string Name)
        {
            this.GoalIDs = GoalIDs;
            this.Name = Name;
        }
        internal bool GetValue()
        {
            return SceneData.instance.PersistentBools.GetValueOrDefault(VogsBingoModPlugin.PersistentName, this.Name);
        }

        internal void SetValue(bool value)
        {
            if (SceneData.instance.PersistentBools.TryGetValue(VogsBingoModPlugin.PersistentName, this.Name, out PersistentItemData<bool> persistent))
            {
                persistent.Value = value;
            } else
            {
                PersistentItemData<bool> newPersistent = new PersistentItemData<bool>
                {
                    SceneName = VogsBingoModPlugin.PersistentName,
                    ID = this.Name,
                    IsSemiPersistent = false,
                    Value = value
                };
                SceneData.instance.PersistentBools.SetValue(newPersistent);
            }
            Automarker.CheckIfGoalsCompleted(this.GoalIDs);
        }
    }
}
