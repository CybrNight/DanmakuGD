using System;

namespace GDBulletML;

/// <summary>
/// This thing manages a few gameplay variables that used by the bulletml lib
/// </summary>
public static class Globals
{
    private static Random _rand;

    public static float RandFloat { get { return (float)_rand.NextDouble(); }}
    public static float Rank { get; private set; }
    public static float TimeScale { get; set; } = (1.0f / 60.0f);

    static Globals(){
        _rand = new Random(Guid.NewGuid().GetHashCode());
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