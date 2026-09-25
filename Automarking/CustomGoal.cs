using System;

namespace VogsBingoMod.Automarking
{
    /// <summary>
    /// Represents a Custom Goal object which can be registered via other mods to allow for custom automarking.
    /// </summary>
    public sealed class CustomGoal
    {
        /// <summary>
        /// The name this custom goal is associated with.
        /// </summary>
        public string goalName {get; private set;}
        /// <summary>
        /// The internal ID 
        /// </summary>
        public int goalID {get; private set;}

        /// <summary>
        /// Creates and registers a new bingo goal by name that can be marked by triggering Mark(), allowing for custom implementations.
        /// Will not work if the given goal name already has automarking support.
        /// </summary>
        /// <param name="goalName">The name of the goal that will be automatically marked.</param>
        /// <returns>Returns the resulting CustomGoal object if registration is successful. Returns null and prints the error if one occurs, such as the goal already having automarking support.</returns>
        public static CustomGoal? Create(string goalName)
        {
            try
            {
                return new CustomGoal(goalName);
            } catch (Exception e)
            {
                VogsBingoModPlugin.LogError(e);
                return null;
            }
        }
        
        /// <summary>
        /// Marks the goal that is associated with this CustomGoal object if it can be marked.
        /// </summary>
        /// <param name="logDebugInformation">Whether or not to print information about the marking process to the log.</param>
        /// <returns>Returns true if the mark data should be sent to the website.
        /// Note that this does not guarantee that the mark actually happens on the website, only that the request will attempt to be sent.</returns>
        public bool TryMark(bool logDebugInformation = false)
        {
            return Automarker.MarkIfAvailable(this.goalID, logDebugInformation);
        }

        internal CustomGoal(string goalName)
        {
            this.goalName = goalName;
            this.goalID = GoalHelper.RegisterCustomGoal(goalName);
        }
    }
}
