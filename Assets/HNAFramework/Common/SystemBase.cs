using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SystemBase<T, DataType> : MonoBehaviour 
    where T : MonoBehaviour 
    where DataType: ScriptableObject
{
    #region SELF_CREATING
    private static T s_instance;
    public static T Instance
    {
        get
        {
            return s_instance ?? new GameObject(typeof(T).ToString()).AddComponent<T>();
        }
    }

    private static bool m_bInitialized;
    public static bool Initialized
    {
        get
        {
            return m_bInitialized;
        }
    }

    protected void Awake()
    {
        if (s_instance != null)
        {
            Destroy(gameObject);
        }
        s_instance = this as T;

        data = Resources.Load<DataType>(typeof(DataType).ToString());
        m_bInitialized = true;

        DontDestroyOnLoad(gameObject);
    }

    protected void OnEnable()
    {
        if (s_instance == null) s_instance = this as T;
        if (!m_bInitialized) return;
    }

    protected void OnDestroy()
    {
        if (s_instance != this) return;
        s_instance = null;
        if (!m_bInitialized) return;
    }

    private DataType data;
    public DataType Data { get { return data; } }
    #endregion
}