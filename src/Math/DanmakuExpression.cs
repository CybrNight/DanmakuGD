using Godot;
using System.Collections.Generic;
using System.Linq;
namespace DanmakuGD;



public partial class DanmakuExpression : Expression {

    public string ExpressionStr { get; private set; }
    private Dictionary<string, Variant> variables = new Dictionary<string, Variant>();
    

    /// <summary>
    /// Registers a new variable to be passed to <see cref="Expression"/>
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

    public void SetVar(string name, Variant value){
        
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

    public void ExecuteExpression(){
        var vars = variables.Values.ToArray();

        Execute(Variant.From(vars).AsGodotArray());
    }

    public void SetVar(string vname, Variant value){
        variables[vname] = value;
    }

    public void GetValue

    [Export(PropertyHint.MultilineText)]
    public string ExpressionString { get; private set; }

    public Expression Expression { get; private set; }

    public DanmakuExpression(){
        Expression = new Expression();
    }

    public void AddVariable(string vname, Variant value){
        
    }

    public void Evaluate(){

    }
}
