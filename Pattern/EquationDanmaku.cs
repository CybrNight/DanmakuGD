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
    public string VariableDefinitions { get; private set; }

    [Export(PropertyHint.MultilineText)]
    public string EquationDefinitions { get; private set; }

    private Dictionary<string, float> variables = new Dictionary<string, float>();


    [Export]
    public float Speed { get; set; } = 10f;

    public Node2D Owner { get; private set; }

    /// <summary>
    /// List of all <see cref="DanmakuFunction"/> that make up this danmaku
    /// </summary>
    public List<DanmakuFunction> equations = new List<DanmakuFunction>();

    public void Parse() {
        equations = new List<DanmakuFunction>();
        var floatVars = new List<FloatVar>();
        var variableDefLines = VariableDefinitions.Split(";", StringSplitOptions.RemoveEmptyEntries);

        int i = 0;
        foreach (var line in variableDefLines){
            var varDef = line.Trim();
            // Malformed variable defintion, log and abort
            if (varDef.Length <= 2){
                throw new ArgumentNullException("Malformed Variable Defintion, Aborting Parse!");
            }

            // Get the variable name and the value
            var parts = varDef.Split('=');

            // We don't have any values, malformed statment
            if (parts.Length == 0){
                throw new ArgumentNullException("No argument value passed!");
            }

            var name = parts[0].ToString();
            var arg = default(float);
            if (!float.TryParse(parts[1], out arg)){
                throw new ArgumentException(parts[1] + " is not a float string!");
            }

            variables[name] = arg;
            var floatVar = new FloatVar(name, arg);
            floatVars.Add(floatVar);
        }

        Variables = floatVars.ToArray();

        // Store all defined variables for passing to Expression
        foreach(var floatVar in Variables) {
            variables[floatVar.VName] = floatVar.Value;
        }

        var equationDefs = EquationDefinitions.Split(';', StringSplitOptions.RemoveEmptyEntries);

        // Treat each line in the FunctionString as it's own EquationSystem
        foreach(var line in equationDefs) {
            var df = new DanmakuFunction(variables);
            var expressions = line.Trim().Split("|");
            foreach (var e in expressions){
                var vnames = variables.Keys.ToArray();
                
                df.Parse((CName)(e[0]), e.Substring(2), vnames);
                
                equations.Add(df);
            }
        }

        Data.Instance.CacheFunction(PatternID, this);
    }
}

