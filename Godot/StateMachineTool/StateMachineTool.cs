#if TOOLS
using Godot;
using System;

[Tool]
public partial class StateMachineTool : EditorPlugin
{
	public override void _EnterTree()
	{
		// 注册状态机相关的自定义类型
		var stateMachineScript = GD.Load<Script>("res://addons/StateMachine/Scripts/StateMachine.cs");
		var stateScript = GD.Load<Script>("res://addons/StateMachine/Scripts/State.cs");
        
		// 注册主要的状态机类型
		AddCustomType("StateMachine", "Node", stateMachineScript, null);
		// 注册状态基类
		AddCustomType("State", "Node", stateScript, null);
	}

	public override void _ExitTree()
	{
		// 清理时移除注册的类型
		RemoveCustomType("StateMachine");
		RemoveCustomType("State");
	}
}
#endif
