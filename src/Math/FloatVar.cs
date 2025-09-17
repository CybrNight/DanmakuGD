using DanmakuGD;
using Godot;
using System;

[GlobalClass]
public partial class FloatVar : VarData {
    
    public FloatVar(){

    }

    public FloatVar(string vname, float value){
        VName = vname;
        Value = value;
    }

    [Export]
    public override string VName { get; protected set; }

    [Export(PropertyHint.Range, "-5, 5")]
    public float Value { get; set; }
}
