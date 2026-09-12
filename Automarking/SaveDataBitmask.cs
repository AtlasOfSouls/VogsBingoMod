/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls

namespace VogsBingoMod.Automarking
{
    internal class SaveDataBitmask
    {
        internal string Name;
        internal AutomarkIntValue[] AutomarkValues {get; private set;}
        internal void AddFlag(uint flag)
        {
            uint newValue = (uint)(this.GetValue()) | flag;
            this.SetValue((int)newValue);
        }
        internal SaveDataBitmask(AutomarkIntValue[] AutomarkValues, string Name)
        {
            this.Name = Name;
            this.AutomarkValues = AutomarkValues;
        }

        internal void ResetFlags()
        {
            this.SetValue(0);
        }

        void CheckForAutomarks()
        {
            int bitCount = sizeof(uint) * 8;
            int flagCount = 0;
            for (int i = 0; i < bitCount; i++)
            {
                if((uint)(this.GetValue() & (1 << i)) > 0)
                {
                    flagCount++;
                }
            }
            Automarker.CheckIfGoalsCompleted(AutomarkValues, flagCount);
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
            this.CheckForAutomarks();
        }
    }
}
