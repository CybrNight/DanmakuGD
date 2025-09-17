using Godot;
using System.Collections.Generic;
using System.Linq;
namespace DanmakuGD;

/// <summary>
/// Extension of <see cref="Expression"/>
/// </summary>
public partial class DanmakuEquation : Expression {

    /// <summary>
    /// The string of the currently parsed <see cref="Expression"/>/>
    /// </summary>
    public string ExpressionStr { get; private set; }

    public RefCounted BaseInstance { get; private set; }
    private Dictionary<string, Variant> variables = new Dictionary<string, Variant>();
    
    public Variant Value { get {
            return ExecuteExpression();
    } }

    /// <summary>
    /// Registers a new variable to be passed to the underlying <see cref="Expression"/>
    /// IF desired, it will auto-reparse the Expression
    /// </summary>
    public void AddVar(string vname, Variant value, bool reparse){
        variables[vname] = new Variant();
        variables[vname] = value;

        if(reparse) {
            var vars = variables.Keys.ToArray();
            Parse(ExpressionStr, vars);
        }
    }

    public void SetVar(string vname, Variant value) {
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
    public Variant ExecuteExpression() {
        var vars = variables.Values.ToArray();

        return ExecuteExpression(vars, BaseInstance);
    }

    /// <summary>
    /// Allows for direct call to Execute
    /// </summary>
    /// <param name="values"></param>
    /// <param name="baseInstance"></param>
    /// <returns></returns>
    private Variant ExecuteExpression(Variant[] values, GodotObject baseInstance=null){
        var valuesArr = Variant.From(values).AsGodotArray();
        return Execute(valuesArr, baseInstance);
    }


    [Export(PropertyHint.MultilineText)]
    public string ExpressionString { get; private set; }

    public Expression Expression { get; private set; }

    public DanmakuEquation(){ 

    }
}
