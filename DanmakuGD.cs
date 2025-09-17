#if TOOLS
using Godot;
using System;

namespace DanmakuGD;

[Tool]
public partial class DanmakuGD : EditorPlugin
{

	private static Time time;

	public override void _EnterTree()
	{

		var bSpawner = ResourceLoader.Load<Script>("res://addons/DanmakuGD/src/Spawner/BulletSpawner.cs");

		// Add BulletSpawner to Resource list
		AddCustomType("BulletSpawner", "Node2D", bSpawner, new Texture2D());

		// Initialization of the plugin goes here.
		//AddCustomType("BulletSpawner", "Node2D", );
		AddAutoloadSingleton("DanEvents", "res://addons/DanmakuGD/src/dan_events.gd");
        AddAutoloadSingleton("Data", "res://addons/DanmakuGD/src/Singleton/Data.tscn");
        AddAutoloadSingleton("GlobalTime", "res://addons/DanmakuGD/src/Singleton/Time.cs");

        time = Time.Instance;
    }

	public override void _ExitTree()
	{
		//Cleanup the Resource menu
		RemoveCustomType("BulletSpawner");

		// Cleanup all autoloads
		RemoveAutoloadSingleton("Data");
		RemoveAutoloadSingleton("GlobalTime");
	}
}
#endif
