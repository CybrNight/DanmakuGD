using System;

namespace BulletMLLib;

/// <summary>
/// This thing manages a few gameplay variables that used by the bulletml lib
/// </summary>
public static class GameManager
{
    public static Random Rand { get; private set; }
    public static float Rank { get; private set; }

    static GameManager(){
        Rand = new Random(Guid.NewGuid().GetHashCode());
        Rank = 1f;
    }

    //TODO: get rid of this class and move game difficulty in to bullet manager

    //TODO: bullet should store the difficulty when they are fired

    /// <summary>
    /// callback method to get the game difficulty.
    /// You need to set this at the start of the game
    /// </summary>
    static public FloatDelegate GameDifficulty;
}