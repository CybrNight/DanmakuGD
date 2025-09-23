using DanmakuGD;
using Godot;
using Godot.Collections;
using System;

namespace BulletMLLib
{
	/// <summary>
	/// This is an equation used in BulletML nodes.
	/// This is an eays way to set up the grammar for all our equations.
	/// </summary>
	public partial class BulletMLEquation : Expression
	{

		IBulletManager _manager;

		// Stores the current value of all variables in the equation
		private Dictionary<string, float> _variables = new Dictionary<string, float>();
		public string EquationString { get; private set; }

		/// <summary>
		/// Sets the value of variable in the equation
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetVar(string name, float value) {
			_variables[name] = value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="manager"></param>
		public void AddVar(string name, float value) {
			if(_variables.ContainsKey(name)) {
				GD.PushError($"{nameof(BulletMLEquation)}: {name} already exists in equation. Use SetVar() to avoid reparse");
			}
			_variables[name] = value;
		}

		// Parses 
		public void ParseEquation(string expression){
			/*
			 Convert the BulletML expression strings to match what Godot Expression expects
			 Allows for this code to maintain backwards compatiblity with original BulletML files
			 */
			var converted = expression.Replace('$', '_');
			EquationString = converted;
			var result = Parse(EquationString);	
			if (result != Error.Ok){
				throw new Exception($"{nameof(BulletMLEquation)}:Error parsing {expression} with exit code {result}");
			}
		}

		public float ExecuteEquation(BulletMLTask task){
			var result = (float)Execute([], task);
			
			// Log if the the execution failed
			if (HasExecuteFailed()){
				GD.PushError($"{nameof(BulletMLEquation)}: Failed to execute {EquationString} on task {task}");
				return 0;
			}

			return result;
		}

		public BulletMLEquation()
		{
			
		}
	}
}

