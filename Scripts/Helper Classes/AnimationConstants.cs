namespace YourFirst2DGame.Scripts.Helper;

/// <summary>
/// Contains animations name for the game
/// </summary>
public static class AnimationConstants
{
    /// <summary>
    /// Contains animation names for the player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Reference to the player<c>Walk</c>animation name
        /// </summary>
        public const string WALK = "Walk";


        /// <summary>
        /// Reference to the player<c>Up</c>animation name
        /// </summary>
        public const string UP = "Up";
    }


    /// <summary>
    /// Contains animation names for the mob enemy
    /// </summary>
    public class Mob
    {
        /// <summary>
        /// Reference to the mob<c>Fly</c>animation name
        /// </summary>
        public const string FLY = "Fly";


        /// <summary>
        /// Reference to the mob<c>Swim</c>animation name
        /// </summary>
        public const string SWIM = "Swim";


        /// <summary>
        /// Reference to the mob<c>Walk</c>animation name
        /// </summary>
        public const string WALK = "Walk";
    }
}