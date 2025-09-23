using BulletMLLib;
using Godot;
using System.Threading.Tasks;

namespace DanmakuGD;

public enum MoverType{
    BulletML,
    Circle,
    Elipse,
    Sin,
    Throw,
}

/// <summary>
/// Defines a <see cref="NodeBullet"/> that will follow the
/// </summary>
public partial class MLBullet : NodeBullet
{
    private double tick;

    public MoverType Type { get; set; }

    public MLBullet(IBulletManager myBulletManager)
        : base(myBulletManager) { }

    public override void Update(float delta) {
        //Flag to tell whether or not this bullet has finished all its tasks
        for(int i = 0; i < Tasks.Count; i++) {
            Tasks[i].Run(this);
        }

        //only do this stuff if the bullet isn't done, cuz sin/cosin are expensive
        Vector2 vel = (Acceleration + (Direction.ToVector2() * (Speed * TimeSpeed))) * Scale;
        X += vel.X;
        Y += vel.Y;
        BulletNode.Rotation += (delta) * Speed;
    }

    public override void PostUpdate()
    {

    }
}
