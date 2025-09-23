using BulletMLLib;
using Godot;
using Godot.Collections;

namespace DanmakuGD;

/// <summary>
/// <see cref="FunctionBullet"/> Represents a 
/// </summary>
public partial class FunctionBullet : NodeBullet
{

    public DanmakuFunction Function { get; set; }
    public Vector2 Offset { get; set; }

    public FunctionBullet(IBulletManager manager, DanmakuFunction function) : base(manager) { 
        _tick = 0;
        Function = function;
        Speed = 25;
        Position = Offset;
    }

    /// <summary>
    /// Update this bullet.  Called once every 1/60th of a second during runtime
    /// </summary>v
    public override void Update(float delta) {
        //Flag to tell whether or not this bullet has finished all its tasks
        _tick += ((float)Time.PhysicsDelta);


        Vector2 pos;
        Vector2 nextPos = new Vector2();

        if(Function == null) {
            return;
        }

        if(_tick < 8) {

            // Set the new Position with -y so that proper x and y are observed
            pos = Function.Execute(this);


            nextPos = Acceleration + pos.Rotated(ParentNode.Rotation) * 35;
            X = Mathf.Lerp(X, nextPos.X, delta * Speed);//MathHelper.SmoothStep(X, nextPos.X, Speed * Time.PhysicsDelta);
            Y = Mathf.Lerp(Y, nextPos.Y, delta * Speed); //MathHelper.SmoothStep(Y, nextPos.Y, Speed * Time.PhysicsDelta);
        } else if (_tick > 8 && _tick < 8.01f) {
            Speed = 50;
            Direction = GetAimDir();
        }else{
            var vel = (Acceleration + (Direction.ToVector2() * (Speed * TimeSpeed))) * Scale;
            X += vel.X;
            Y += vel.Y;
        }
           
        }


        
}
