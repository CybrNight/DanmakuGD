using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Godot.Projection;

namespace DanmakuGD;

/// <summary>
/// Base type for <see cref="EquationDanmaku"/>
/// Defines a curve(s) for bullets to follow
/// </summary>
[GlobalClass]
public partial class EquationDanmaku : Danmaku {

    [Export]
    public FloatVar[] Variables { get; private set; }

    [Export(PropertyHint.MultilineText)]
    public string EquationDefinitions { get; private set; }

    private Dictionary<string, Variant> variables = new Dictionary<string, Variant>();


    [Export]
    public float Speed { get; set; } = 10f;

    public Node2D Owner { get; private set; }

    /// <summary>
    /// List of all <see cref="DanmakuFunction"/> that make up this danmaku
    /// </summary>
    public List<DanmakuFunction> equations = new List<DanmakuFunction>();

    public void Parse() {
        equations = new List<DanmakuFunction>();

        // Store all defined variables for passing to Expression
        foreach(var floatVar in Variables) {
            variables[floatVar.VName] = floatVar.Value;
        }

        var equationDefs = EquationDefinitions.Split(';', StringSplitOptions.RemoveEmptyEntries);

        // Treat each line in the FunctionString as it's own EquationSystem
        foreach(var line in equationDefs) {
            var df = new DanmakuFunction();
            var expressions = line.Trim().Split("|");
            GD.Print(expressions.Length);
            foreach (var e in expressions){
                var vnames = variables.Keys.ToArray();
                df.Parse((CName)(e[0]), e.Substring(2), vnames);
                equations.Add(df);
            }
        }

        Data.Instance.CacheFunction(PatternID, this);
    }
}

