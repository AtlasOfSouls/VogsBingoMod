/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using Newtonsoft.Json;

namespace VogsBingoMod.Automarking
{
    public class SaveDataBitmask
    {
        public string Name;
        public uint bitmask = 0;
        public AutomarkIntValue[] AutomarkValues {get; private set;}
        internal void AddFlag(uint flag)
        {
            bitmask = bitmask | flag;
            this.CheckForAutomarks();
        }
        public SaveDataBitmask(AutomarkIntValue[] AutomarkValues, string Name)
        {
            this.Name = Name;
            this.AutomarkValues = AutomarkValues;
        }

        [JsonConstructor]
        public SaveDataBitmask(AutomarkIntValue[] AutomarkValues, string Name, uint bitmask)
        {
            this.Name = Name;
            this.AutomarkValues = AutomarkValues;
            this.bitmask = bitmask;
        }

        internal void ResetFlags()
        {
            this.bitmask = 0;
        }

        void CheckForAutomarks()
        {
            int bitCount = sizeof(uint) * 8;
            int flagCount = 0;
            for (int i = 0; i < bitCount; i++)
            {
                if((uint)(this.bitmask & (1 << i)) > 0)
                {
                    flagCount++;
                }
            }
            Automarker.CheckIfGoalsCompleted(AutomarkValues, flagCount);
        }
    }
}
