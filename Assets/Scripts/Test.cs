/*
    author: wzs
    date:   2024-12-13T03:51:29
*/

using System;
using NPBehave;
using UnityEngine;
using Action = NPBehave.Action;

public class Test : MonoBehaviour
{

    #region 事件

    #endregion

    #region 属性

    #endregion

    #region 变量

    public int value;

    private Root behaviourTree;
    private Blackboard blackboard;
    
    #endregion

    #region 生命周期

    private void Awake()
    {
        blackboard = new Blackboard(UnityContext.GetSharedBlackboard("TestBlackBoard"), UnityContext.GetClock());
        Node action = new Action(() => Debug.Log("Hello World! " + blackboard.Get<int>("test")));
        Node bc1n = new Action(()=>Debug.Log("Smaller!" + value));
        Node bc2n = new Action(()=>Debug.Log("Equal!" + value));
        Node bc3n = new Action(()=>Debug.Log("Greater!" + value));
        Node action2 = new WaitUntilStopped();
        Node main = new Sequence(action);
        Node blackBoardCondition1 =
            new BlackboardCondition("test", Operator.IS_SMALLER,3, Stops.IMMEDIATE_RESTART, bc1n);
        Node blackBoardCondition2 =
            new BlackboardCondition("test", Operator.IS_EQUAL,3, Stops.IMMEDIATE_RESTART, bc2n);
        Node blackBoardCondition3 =
            new BlackboardCondition("test", Operator.IS_GREATER,3, Stops.IMMEDIATE_RESTART, bc3n);
        behaviourTree = new Root(blackboard,new Service(0.01f,_CheckBlackBoardUpdate,new Selector(blackBoardCondition1,blackBoardCondition2,blackBoardCondition3,main)));
        
        behaviourTree.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            value++;
        }
    }

    #endregion

    #region 公有方法

    //----------------------Public----------------------

    #endregion

    #region 保护方法

    //----------------------Protect----------------------

    #endregion

    #region 私有方法

    //----------------------Private----------------------

    private void _CheckBlackBoardUpdate()
    {
        int tmpValue = blackboard.Get<int>("test");
        blackboard.Set("test",value);
        if (tmpValue != value)
        {
            blackboard.Enable();
        }
        else
        {
            blackboard.Disable();
        }
    }
    
    #endregion

    
}