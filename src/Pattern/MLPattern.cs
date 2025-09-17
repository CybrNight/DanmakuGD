using BulletMLLib;
using Godot;
using System;

namespace DanmakuGD;

public abstract partial class Pattern : Resource
{

    /// <summary>
    /// ID for referencing this elsewhere<c>GDPattern</c>
    /// </summary>
    [Export]
    public string PatternID { get; private set; }

    [Export]
    public string BulletRefID { get; private set; }

}

[GlobalClass]
public partial class MLPattern : Pattern {
    /// <summary>
    /// Path to BulletML XML source file
    /// </summary>
    [Export(PropertyHint.File, "*.xml")]
    public string SourceFile { get; private set; }
}
