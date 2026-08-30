/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
namespace VogsBingoMod.Automarking
{
    internal static class AutomarkRosaryStringHandler
    {
        static AutomarkerDataInt FrayedStringsCurrentlyHeld = new([], "FrayedStringsCurrentlyHeld");
        static AutomarkerDataInt FrayedStringsBroken = new([], "FrayedStringsBroken");
        static AutomarkerDataInt PurchasedStringsCurrentlyHeld = new([], "PurchasedStringsCurrentlyHeld");
        static AutomarkerDataInt NonPurchasedStringsCurrentlyHeld = new([], "NonPurchasedStringsCurrentlyHeld");
        static AutomarkerDataInt NonPurchasedStringsBroken = new([], "NonPurchasedStringsBroken");

        internal static void Initialize()
        {
            
        }

        internal static int GetFrayedStringsCurrentlyHeld()
        {
            return FrayedStringsCurrentlyHeld.Value;
        }
        internal static int GetFrayedStringsBroken()
        {
            return FrayedStringsBroken.Value;
        }
        internal static int GetPurchasedStringsCurrentlyHeld()
        {
            return PurchasedStringsCurrentlyHeld.Value;
        }
        internal static int GetNonPurchasedStringsCurrentlyHeld()
        {
            return NonPurchasedStringsCurrentlyHeld.Value;
        }
        internal static int GetNonPurchasedStringsBroken()
        {
            return NonPurchasedStringsBroken.Value;
        }

        internal static void AddFrayedString()
        {
            FrayedStringsCurrentlyHeld.Value++;
            Automarker.CheckIfGoalCompleted(GoalID.HaveSixRosaryStringsnopurchasing);
        }

        internal static void BreakFrayedString()
        {
            FrayedStringsBroken.Value++;
            FrayedStringsCurrentlyHeld.Value--;
            Automarker.CheckIfGoalCompleted(GoalID.BreakEightRosaryStringsnopurchasing);
        }

        internal static void AddPurchasedString()
        {
            PurchasedStringsCurrentlyHeld.Value++;
        }

        internal static void AddNonPurchasedString()
        {
            NonPurchasedStringsCurrentlyHeld.Value++;
            Automarker.CheckIfGoalCompleted(GoalID.HaveSixRosaryStringsnopurchasing);
        }

        internal static void BreakString()
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
