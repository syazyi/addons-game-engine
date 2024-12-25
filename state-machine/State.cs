using Godot;

namespace Cosmos.Script.StateMachine;

/// <summary>
/// 状态机状态
/// 用户继承该类自定义状态
/// 用户需要手动设置好信号链接（暂时不知道自动链接方法）
/// </summary>
public partial class State : Node
{
    /// <summary>
    /// 状态机
    /// </summary>
    protected StateMachine OwnerStateMachine;
    
    /// <summary>
    /// 进入状态
    /// </summary>
    public virtual void EnterState()
    {
        
    }

    /// <summary>
    /// 退出状态
    /// </summary>
    public virtual void ExitState()
    {
        
    }

    [Signal]
    public delegate void OnChangedStateEventHandler(State lastState, StringName nextStateName);

    protected Node GetOwnerNode()
    {
        return OwnerStateMachine.OwnerNode;
    } 
    
    public override void _EnterTree()
    {
        base._EnterTree();
        
        OwnerStateMachine = GetParent<StateMachine>();
        
        //检测信号是否连接好
        if (GetSignalConnectionList(SignalName.OnChangedState).Count < 2)
        {
            GD.PrintErr(Name + "Signal OnChangedState dont connect!!!");
        }

        //禁止自己更新
        ProcessMode = ProcessModeEnum.Disabled;
    }
}