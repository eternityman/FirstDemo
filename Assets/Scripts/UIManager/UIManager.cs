using System.Collections.Generic;

public class UIManager : BaseMonoInstance<UIManager>
{
    #region 事件区



    #endregion

    #region 属性区



    #endregion

    #region 变量区

    /// <summary>
    /// ui缓存
    /// </summary>
    private Stack<UIBase> _uiStact = new(10);

    /// <summary>
    /// 加载提示UI
    /// </summary>
    private UIBase _loadingUI;

    #endregion

    #region 函数区

    //--------------------Public 公有函数--------------------//


    //--------------------Protect 保护函数--------------------//


    //--------------------Private 私有函数--------------------//


    #endregion
}