using Godot;
using Godot.Collections;


/// <summary>
/// 首先在需要状态机的对象下创建状态机实例
/// 定义该对象特有的各种状态（用户控制）
/// 将使用到的状态设置为状态机的子节点
/// </summary>
public partial class StateMachine : Node
{
    
    /// <summary>
    /// 子状态字典
    /// </summary>
    private Dictionary<StringName, State> _childrenState = new Dictionary<StringName, State>();

    /// <summary>
    /// 当前状态
    /// </summary>
    [Export]
    private State _curState;
    
    public Node OwnerNode { get; private set; }

    public override void _EnterTree()
    {
        base._EnterTree();

        OwnerNode = GetParent<Node>();
    }

    public override void _Ready()
    {
        var children = GetChildren();
        foreach (var state in children)
        {
            if (state is not State node) continue;
            
            _childrenState.Add(node.Name, node);
        }
        
        _curState?.EnterState();
    }

    public override void _Process(double delta)
    {
        _curState?._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _curState?._PhysicsProcess(delta);
    }
    
    /// <summary>
    /// 状态改变信号处理
    /// </summary>
    /// <param name="lastState">上一个状态</param>
    /// <param name="nextStateName">下一个状态节点名称</param>
    private void OnChangedState(State lastState, StringName nextStateName)
    {
        if (lastState != _curState)
        {
            return;
        }

        if (_childrenState.TryGetValue(nextStateName, out var nextState) == false) return;
        
        _curState?.ExitState();
        nextState.EnterState();
        _curState = nextState;
    }

}