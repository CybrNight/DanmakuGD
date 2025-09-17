using Godot;
using System;

namespace DanmakuGD;

// Main Singleton class for DanmakuGD
public partial class DM : Node{


    /// <summary>
    /// Global reference to the currently active Player Node2D
    /// </summary>
    public Node2D PlayerNode { get { 
        if (IsInstanceValid(_playerNode)){
            return _playerNode;
        }
        return null;
    } }
    private Node2D _playerNode;
}
