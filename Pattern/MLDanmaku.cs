using GDBulletML;
using Godot;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DanmakuGD;

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
