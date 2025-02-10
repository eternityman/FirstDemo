using System.Runtime.InteropServices;

public static class TestManager
{
    #region 事件区



    #endregion

    #region 属性区



    #endregion

    #region 变量区



    #endregion

    #region 函数区

    //--------------------Public 公有函数--------------------//

    public static void Test()
    {
        WX_GetPhoneNumber();
    }
    
    /// <summary>
    /// 调用js接口
    /// </summary>
    [DllImport("__Internal", EntryPoint = "WX_GetPhoneNumber")]
    private static extern void WX_GetPhoneNumber();

    //--------------------Protect 保护函数--------------------//


    //--------------------Private 私有函数--------------------//
    

    #endregion
}