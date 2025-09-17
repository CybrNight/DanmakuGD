using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletMLLib;

namespace BulletMLLib;

public partial class BulletMLTask : RefCounted {

    public float _rand { get {
            return (float)GameManager.Rand.NextDouble();
        }
    }

    public float _rank { get {
            return (float)GameManager.Rank;
        }
    }

    public float _1 {
        get {
            return ((float)GetParamValue(1));
        }
    }
    public float _2 {
        get {
            return ((float)GetParamValue(2));
        }
    }
    public float _3 {
        get {
            return ((float)GetParamValue(3));
        }
    }
    public float _4 {
        get {
            return ((float)GetParamValue(4));
        }
    }
    public float _5 {
        get {
            return ((float)GetParamValue(5));
        }
    }
    public float _6 {
        get {
            return ((float)GetParamValue(6));
        }
    }
    public float _7 {
        get {
            return ((float)GetParamValue(7));
        }
    }
    public float _8 {
        get {
            return ((float)GetParamValue(8));
        }
    }
    public float _9 {
        get {
            return ((float)GetParamValue(9));
        }
    }
}
