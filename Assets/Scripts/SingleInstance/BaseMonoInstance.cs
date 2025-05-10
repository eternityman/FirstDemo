using UnityEngine;

public class BaseMonoInstance<T> : MonoBehaviour where T : Component
{
    #region 事件区



    #endregion

    #region 属性区

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
                else
                {
                    BaseMonoInstance<T> obj = _instance as BaseMonoInstance<T>;
                    if (obj != null)
                    {
                        obj._Init();
                    }
                }
            }

            return _instance;
        }
    }

    #endregion

    #region 变量区

    protected static T _instance;

    #endregion

    #region 函数区

    //--------------------Public 公有函数--------------------//

    private void Awake()
    {
        _Awake();
    }

    private void OnDestroy()
    {
        _Destroy();
    }

    //--------------------Protect 保护函数--------------------//

    /// <summary>
    /// awake
    /// </summary>
    protected virtual void _Awake()
    {
        
    }

    /// <summary>
    /// init
    /// </summary>
    protected virtual void _Init()
    {
        
    }

    /// <summary>
    /// destroy
    /// </summary>
    protected virtual void _Destroy()
    {
        
    }

    //--------------------Private 私有函数--------------------//


    #endregion
}