/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace VogsBingoMod.Automarking
{
    internal class SaveDataBitmask
    {
        internal string Name;
        internal uint bitmask = 0;
        internal AutomarkIntValue[] AutomarkValues {get; private set;}
        internal void AddFlag(uint flag)
        {
            bitmask = bitmask | flag;
            this.CheckForAutomarks();
        }
        internal SaveDataBitmask(AutomarkIntValue[] automarkValues, string name)
        {
            this.Name = name;
            this.AutomarkValues = automarkValues;
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
