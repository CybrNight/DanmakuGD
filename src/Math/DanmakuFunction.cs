using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DanmakuGD;

/// <summary>
/// Stores two <see cref="Expression"/> for X and Y components
/// </summary>
public enum CoordType {
    Carteesian,
    Polar,
}

public enum CName {
    X = 'x',
    Y = 'y',
    R = 'r'
}


public partial class DanmakuFunction : Resource {
    public float T { get; set; }

    public CoordType CoordType { get; private set; } = CoordType.Carteesian;
    private Dictionary<string, float> variables = new Dictionary<string, float>();

    public DanmakuFunction(Dictionary<string, float> variables) {
        dimensions = new Dictionary<CName, Expression>();
        this.variables = variables;
    }

    private Dictionary<CName, Expression> dimensions;

    /// <summary>
    /// Gets the <see cref="Expression"/> that controls the X component
    /// </summary>
    public Expression XFunc {
        get {
            var result = default(Expression);
            var status = dimensions.TryGetValue(CName.X, out result);

            if (!status){
                GD.PushError("XFunc not initialized");
            }

            return result;
        }
    }

    public Expression YFunc {
        get {
            var result = default(Expression);
            var status = dimensions.TryGetValue(CName.Y, out result);

            if(!status) {
                GD.PushError("YFunc not initialized");
            }

            return result;
        }
    }

    public Expression RFunc {
        get {
            var result = default(Expression);
            var status = dimensions.TryGetValue(CName.R, out result);
            return result;
        }
    }

    /// <summary>
    /// Handles computing the value of each Expression and saving to property value
    /// </summary>
    /// <param name="bullet">The Bullet that owns this <see cref="DanmakuFunction"/></param> 
    /// <param name="args"></param>
    /// <returns></returns>
    public Vector2 Execute(NodeBullet bullet, Array[] args = default){
        float x, y = 0.0f;
        var values = variables.Values.ToArray();
        var collection = Variant.From(values).AsGodotArray();
        // If there is an RFunc then we can solve r^2 = cos(theta) + sin(theta)
        if(RFunc == null) {
            x = (float)XFunc.Execute(collection, bullet);
            y = -(float)YFunc.Execute(collection, bullet);

            return new Vector2(x, y);
        } else{
            var r = (float)RFunc.Execute(collection, bullet);

            x = Mathf.Cos(bullet.T) * r;
            y = -Mathf.Sin(bullet.T) * r;

            return new Vector2(x, y);
        }
    }



    public void Parse(CName cname, string expStr, string[] varNames = default){
        var exp = new DanmakuEquation();
        var status = exp.Parse(expStr, varNames);

        if(status != Error.Ok) {
            GD.PushError($"BulletFunction Error ({status}): Failed to set {expStr}");
            throw new Exception();
        }

        dimensions[cname] = exp;
    }

}