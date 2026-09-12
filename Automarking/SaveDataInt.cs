/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls

namespace VogsBingoMod.Automarking
{
    internal class SaveDataInt
    {
        internal string Name;
        internal int Value {get=> GetValue(); set{SetValue(value);}}
        internal AutomarkIntValue[] AutomarkValues;
        public static implicit operator int(SaveDataInt data) => data.Value;
        internal SaveDataInt(AutomarkIntValue[] AutomarkValues, string Name)
        {
            this.AutomarkValues = AutomarkValues;
            this.Name = Name;
        }

        internal int GetValue()
        {
            return SceneData.instance.PersistentInts.GetValueOrDefault(VogsBingoModPlugin.PersistentName, this.Name);
        }

        internal void SetValue(int value)
        {
            if (SceneData.instance.PersistentInts.TryGetValue(VogsBingoModPlugin.PersistentName, this.Name, out PersistentItemData<int> persistent))
            {
                persistent.Value = value;
            } else
            {
                PersistentItemData<int> newPersistent = new PersistentItemData<int>
                {
                    SceneName = VogsBingoModPlugin.PersistentName,
                    ID = this.Name,
                    IsSemiPersistent = false,
                    Value = value
                };
                SceneData.instance.PersistentInts.SetValue(newPersistent);
            }
            Automarker.CheckIfGoalsCompleted(this.AutomarkValues, value);
        }
    }
}
