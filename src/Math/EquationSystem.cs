using Godot;
using System;
using System.Collections.Generic;

namespace DanmakuGD;


[GlobalClass]
public partial class EquationSystem : Resource {


    public float T { get; set; }
    public float Speed = 25;

    public CoordType CoordType { get; private set; } = CoordType.Carteesian;

    public EquationSystem() {
        components = new Dictionary<CName, Expression>();
    }

    private Dictionary<CName, Expression> components;

    /// <summary>
    /// Gets the <see cref="Expression"/> that controls the X component
    /// </summary>
    public Expression XFunc {
        get {
            Expression result;
            var status = components.TryGetValue(CName.X, out result);
            if(!status) {
                return default;
            }

            return result;
        }
    }

    public Expression YFunc {
        get {
            Expression result;
            var status = components.TryGetValue(CName.Y, out result);
            if(!status) {
                return default;
            }

            return result;
        }
    }

    public Expression RFunc {
        get {
            Expression result;
            var status = components.TryGetValue(CName.R, out result);
            if(!status) {
                return default;
            }

            return result;
        }
    }

    /// <summary>
    /// Handles computing the value of each Expression and saving to property value
    /// </summary>
    /// <param name="bullet">The Bullet that owns this <see cref="EquationSystem"/></param> 
    /// <param name="args"></param>
    /// <returns></returns>
    public Vector2 Execute(NodeBullet bullet, Array[] args = default){
        float x, y = 0.0f;
        // If there is an RFunc then we can solve r^2 = cos(theta) + sin(theta)
        if(RFunc == null) {
            x = (float)XFunc.Execute([], bullet);
            y = -(float)YFunc.Execute([], bullet);

            return new Vector2(x, y);
        } else{
            var r = (float)RFunc.Execute([], bullet);

            x = Mathf.Cos(bullet.T) * r;
            y = -Mathf.Sin(bullet.T) * r;

            return new Vector2(x, y);
        }
    }



    public void Parse(CName cname, string expStr){
        var exp = new Expression();
        var status = exp.Parse(expStr);

        if(status != Error.Ok) {
            GD.PushError($"BulletFunction Error ({status}): Failed to set {expStr}");
        }

        components[cname] = exp;
    }

}