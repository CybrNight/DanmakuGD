using BulletMLLib;
using Godot;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DanmakuGD;

[GlobalClass]
public abstract partial class DanmakuPattern : Resource
{

    /// <summary>
    /// ID for referencing this elsewhere<c>GDPattern</c>
    /// </summary>
    [Export]
    public string PatternID { get; private set; }

    [Export]
    public string BulletRefID { get; private set; }

    public virtual void Parse(){ }

}

[GlobalClass]
public partial class MLDanmaku : DanmakuPattern {
    /// <summary>
    /// Path to BulletML XML source file
    /// </summary>
    [Export(PropertyHint.File, "*.xml")]
    public string SourceFile { get; private set; }

    public override void Parse() {
        var pattern = new BulletPattern();
        pattern.ParseXML(SourceFile.Replace("res://", ""));
        Data.Instance.CacheMLPattern(PatternID, pattern);
    }
}
