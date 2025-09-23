using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DanmakuGD;

/// <summary>
/// Extension of built-in Godot <see cref="Expression"/> to support memoisation 
/// </summary>
public partial class DanmakuEquation : Expression {

    /// <summary>
    /// The string of the currently parsed <see cref="Expression"/>/>
    /// </summary>
    public string ExpressionStr { get; private set; }

    public RefCounted BaseInstance { get; private set; }
    private Dictionary<string, float> variables = new Dictionary<string, float>();
    private Dictionary<int, float> cache = new Dictionary<int, float>();
    
    public Variant Value { get {
        return ExecuteExpression();
    } }

    /// <summary>
    /// Registers a new variable to be passed to the underlying <see cref="Expression"/>
    /// IF desired, it will auto-reparse the Expression
    /// </summary>
    public void AddVar(string vname, float value, bool reparse){
        variables[vname] = value;

        if(reparse) {
            var vars = variables.Keys.ToArray();
            Parse(ExpressionStr, vars);
        }
    }

    public void SetVar(string vname, float value) {
        variables[vname] = value;
    }

    /// <summary>
    /// Parses the underlying <see cref="Expression"/> 
    /// Uses values stored in <see cref="variables"/>
    /// </summary>
    /// <param name="expression"></param>
    public void ParseExpression(string expression){
        var vars = variables.Keys.ToArray();

        Parse(expression, vars);
    }


    /// <summary>
    /// Returns the evaluated value of the <see cref="Expression"/> with saved values
    /// </summary>
    /// <returns></returns>
    public float ExecuteExpression() {
        var vars = variables.Values.ToArray();
        var key = vars.GetHashCode();
        var result = default(float);

        var status = cache.TryGetValue(key, out result);
        if (!status){
            cache[key] = ExecuteExpression(vars, BaseInstance);
        }
        return cache[key];
    }

    /// <summary>
    /// Allows for direct call to Execute
    /// </summary>
    /// <param name="values"></param>
    /// <param name="baseInstance"></param>
    /// <returns></returns>
    private float ExecuteExpression(float[] values, GodotObject baseInstance=null){
        var valuesArr = Variant.From(values).AsGodotArray();
        return ((float)Execute(valuesArr, baseInstance));
    }


    [Export(PropertyHint.MultilineText)]
    public string ExpressionString { get; private set; }

    public Expression Expression { get; private set; }

    public DanmakuEquation(){ 

    }
}
