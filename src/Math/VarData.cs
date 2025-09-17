using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanmakuGD;

[GlobalClass]
public abstract partial class VarData : Resource {
    public virtual string VName { get; protected set; }
}
