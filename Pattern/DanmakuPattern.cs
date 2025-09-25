using Godot;

namespace DanmakuGD;

[GlobalClass]
public abstract partial class DanmakuPattern : Resource {

    /// <summary>
    /// ID for referencing this elsewhere<c>GDPattern</c>
    /// </summary>
    [Export]
    public string PatternID { get; private set; }

    [Export]
    public string BulletRefID { get; private set; }

    public virtual void Parse() { }

}