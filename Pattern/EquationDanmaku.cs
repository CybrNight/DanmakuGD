using Godot;
using System;
using System.Collections.Generic;
using static Godot.Projection;

namespace DanmakuGD;

/// <summary>
/// Stores two <see cref="Expression"/> for X and Y components
/// </summary>
public enum CoordType{ 
    Carteesian,
    Polar,
}

public enum CName{
    X = 'x',
    Y = 'y',
    R = 'r'
}


/// <summary>
/// Base type for <see cref="EquationDanmaku"/>
/// Defines a curve(s) for bullets to follow
/// </summary>
[GlobalClass]
public partial class EquationDanmaku : Danmaku {

    public List<DanmakuEquation> functions = new List<DanmakuEquation>();

    [Export(PropertyHint.MultilineText)]
    public string FunctionString { get; set; }

    [Export]
    public float Speed { get; set; } = 10f;

    [Export]
    public DanmakuExpression p;

    public Node2D Owner { get; private set; }

    public void Parse() {
        functions = new List<DanmakuEquation>();
        var lines = FunctionString.Split(";", StringSplitOptions.RemoveEmptyEntries);

        // Treat each line in the FunctionString as it's own EquationSystem
        foreach(var line in lines) {
            var bf = new DanmakuEquation();
            var expressions = line.Trim().Split("|");
            GD.Print(expressions.Length);
            foreach (var e in expressions){
                bf.Parse((CName)(e[0]), e.Substring(2));
                functions.Add(bf);
            }
        }

        Data.Instance.CacheFunction(PatternID, this);
    }
}

