public abstract class BaseCSharpInstance<T> where T : new()
{
    protected BaseCSharpInstance(){}
    #region 事件区



    #endregion

    #region 属性区

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
                if (_instance is BaseCSharpInstance<T> obj)
                {
                    obj._Init();
                }
            }
            
            return _instance;
        }
    }

    #endregion

    #region 变量区

    private static T _instance;

    #endregion

    #region 函数区

    //--------------------Public 公有函数--------------------//


    //--------------------Protect 保护函数--------------------//

    /// <summary>
    /// 初始化
    /// </summary>
    protected abstract void _Init();

    /// <summary>
    /// 清理
    /// </summary>
    protected abstract void _Clear();

    //--------------------Private 私有函数--------------------//


    #endregion
}