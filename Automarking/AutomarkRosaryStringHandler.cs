/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace VogsBingoMod.Automarking
{
    internal class AutomarkRosaryStringHandler
    {
        internal SaveDataInt FrayedStringsCurrentlyHeld = new([], "FrayedStringsCurrentlyHeld");
        internal SaveDataInt FrayedStringsBroken = new([], "FrayedStringsBroken");
        internal SaveDataInt PurchasedStringsCurrentlyHeld = new([], "PurchasedStringsCurrentlyHeld");
        internal SaveDataInt NonPurchasedStringsCurrentlyHeld = new([], "NonPurchasedStringsCurrentlyHeld");
        internal SaveDataInt NonPurchasedStringsBroken = new([], "NonPurchasedStringsBroken");

        internal int GetFrayedStringsCurrentlyHeld()
        {
            return FrayedStringsCurrentlyHeld.Value;
        }
        internal int GetFrayedStringsBroken()
        {
            return FrayedStringsBroken.Value;
        }
        internal int GetPurchasedStringsCurrentlyHeld()
        {
            return PurchasedStringsCurrentlyHeld.Value;
        }
        internal int GetNonPurchasedStringsCurrentlyHeld()
        {
            return NonPurchasedStringsCurrentlyHeld.Value;
        }
        internal int GetNonPurchasedStringsBroken()
        {
            return NonPurchasedStringsBroken.Value;
        }

        internal void AddFrayedString()
        {
            FrayedStringsCurrentlyHeld.Value++;
            Automarker.CheckIfGoalCompleted(GoalID.HaveSixRosaryStringsnopurchasing);
        }

        internal void BreakFrayedString()
        {
            FrayedStringsBroken.Value++;
            FrayedStringsCurrentlyHeld.Value--;
            Automarker.CheckIfGoalCompleted(GoalID.BreakEightRosaryStringsnopurchasing);
        }

        internal void AddPurchasedString()
        {
            PurchasedStringsCurrentlyHeld.Value++;
        }

        internal void AddNonPurchasedString()
        {
            NonPurchasedStringsCurrentlyHeld.Value++;
            Automarker.CheckIfGoalCompleted(GoalID.HaveSixRosaryStringsnopurchasing);
        }

        internal void BreakString()
        {
            if (PurchasedStringsCurrentlyHeld <= 0 || (NonPurchasedStringsCurrentlyHeld > 0 && Automarker.BoardHasGoal(GoalID.BreakEightRosaryStringsnopurchasing) && !Automarker.BoardHasGoal(GoalID.HaveSixRosaryStringsnopurchasing)))
            {
                //break non-shop string
                NonPurchasedStringsBroken.Value++;
                NonPurchasedStringsCurrentlyHeld.Value--;
                Automarker.CheckIfGoalCompleted(GoalID.BreakEightRosaryStringsnopurchasing);
            } else
            {
                //break shop string
                PurchasedStringsCurrentlyHeld.Value--;
            }
        }
    }
}
