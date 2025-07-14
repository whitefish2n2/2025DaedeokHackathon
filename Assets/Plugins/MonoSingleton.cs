using System;
using UnityEngine;

namespace Plugins
{
    /// <summary>
    /// dontDestroyOnLoad 포함 singleton
    /// </summary>
    /// <typeparam name="T">상속자 자신</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        private static readonly object _lock = new object();
        public static bool IsInitialized { get; private set; }

        public static T Instance
        {
            get
            {
                if (!_instance) Debug.LogError($"[MonoSingleton<{typeof(T).Name}>] is not initialized!");
                return _instance;
            }
        }


        public static bool TryGetInstance(out T instance)
        {
            instance = _instance;
            return IsInitialized;
        }

        protected virtual void Awake()
        {
            lock (_lock)
            {
                if (_instance && _instance != this)
                {
                    Destroy(gameObject);
                    return;
                }

                _instance = (T) this;
                DontDestroyOnLoad(gameObject);
                IsInitialized = true;
                Initialize();
            }
        }

        protected virtual void Start()
        {
            LateInit();
        }

        /// <summary>
        /// 초기화 코드 - Awake()에서 실행
        /// </summary>
        protected virtual void Initialize() { }

        protected virtual void LateInit() { }

        protected virtual void OnDestroy()
        {
            if (_instance != this) return;
            _instance = null;
            IsInitialized = false;
        }
    }
}