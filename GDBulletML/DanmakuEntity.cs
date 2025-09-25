using Godot;
using GDBulletML;
using System.Collections.Generic;

public abstract partial class DanmakuEntity : RefCounted{

    public Vector2 Position { get; set; } 
    public Vector2 Direction { get; set; } = Vector2.Zero;


    /// <summary>
    /// Abstract property to get the X location of this bullet.
    /// measured in pixels from upper left
    /// </summary>
    /// <value>The horizontrla position.</value>
    public abstract float X {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the y parameter of the location
    /// measured in pixels from upper left
    /// </summary>
    /// <value>The vertical position.</value>
    public abstract float Y {
        get;
        set;
    }

    #region Properties

    /// <summary>
    /// The acceleration of this bullet
    /// </summary>
    /// <value>The accel, in pixels/frame^2</value>
    public Vector2 Acceleration { get; set; }

    /// <summary>
    /// Gets or sets the speed
    /// </summary>
    /// <value>The speed, in pixels/frame</value>


    public DanmakuEntity(){
        
    }
    #endregion
}