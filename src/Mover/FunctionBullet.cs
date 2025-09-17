using BulletMLLib;
using Godot;
using Godot.Collections;

namespace DanmakuGD;

/// <summary>
/// <see cref="FunctionBullet"/> Represents a 
/// </summary>
public partial class FunctionBullet : NodeBullet
{

    public EquationSystem Function { get; set; }
    public Vector2 Offset { get; set; }

    public FunctionBullet(IBulletManager manager, EquationSystem function) : base(manager) { 
        _tick = 0;
        Function = function;
        Speed = function.Speed;
        Position = Offset;
    }

    public override void PostUpdate() {
        var cam = BulletNode.GetViewport().GetCamera2D();

        var rect = cam.GetViewportRect();
    }

    private void SinMovement() {
        //Flag to tell whether or not this bullet has finished all its tasks
        _tick += ((float)Time.PhysicsDelta);


        Vector2 pos;

        if(Function == null) {
            return;
        }

        // Set the new Position with -y so that proper x and y are observed
        pos = Function.Execute(this);

        var nextPos = Acceleration + pos.Rotated(ParentNode.Rotation) * Speed;

        X = Mathf.Lerp(X, nextPos.X, Time.PhysicsDelta * Speed);//MathHelper.SmoothStep(X, nextPos.X, Speed * Time.PhysicsDelta);
        Y = Mathf.Lerp(Y, nextPos.Y, Time.PhysicsDelta * Speed); //MathHelper.SmoothStep(Y, nextPos.Y, Speed * Time.PhysicsDelta);
    }

    private void EllipseMovement(float skewX, float skewY) {
        //Flag to tell whether or not this bullet has finished all its tasks
        _tick += ((float)Time.PhysicsDelta);
        //only do this stuff if the bullet isn't done, cuz sin/cosin are expensive
        X += 20 * ((float)Mathf.Cos(2 * T)) / skewX;
        Y += 20 * ((float)Mathf.Sin(2 * T)) / skewY;
    }

    /// <summary>
    /// Update this bullet.  Called once every 1/60th of a second during runtime
    /// </summary>
    public override void Update() {
        SinMovement();
    }
}
