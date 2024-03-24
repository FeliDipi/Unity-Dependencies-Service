using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace OneFrame
{
    [DefaultExecutionOrder(-1000)]
    public class DependencyLocator : MonoBehaviour
    {
        private static DependencyLocator _instance;
        public static DependencyLocator Instance 
        { 
            get
            {
                if(_instance == null)
                {
                    _instance = new GameObject(nameof(DependencyLocator)).AddComponent<DependencyLocator>();
                    _instance.hideFlags = HideFlags.NotEditable;
                }

                return _instance;
            }
        }

        private Dictionary<Type, Component> _services = new Dictionary<Type, Component>();

        public void Register(DependencyService service)
        {
            Type key = service.GetType();

            if (_services.ContainsKey(key)) return;

            _services[key] = service;

            Debug.Log($"<color=#27ae60><b>↓ [Register]:</b> {key.Name}</color>");
        }

        public void UnRegister(DependencyService service)
        {
            Type key = service.GetType();

            if (!_services.ContainsKey(key)) return;

            _services.Remove(key);

            Debug.Log($"<color=#e74c3c><b>↑ [UnRegister]:</b> {key.Name}</color>");
        }

        public Component Request<T>()
        {
            Type key = typeof(T);

            if (!_services.ContainsKey(key)) return default;

            Debug.Log($"<color=#3498db><b>← [Request]:</b> {key.Name}</color>");

            return _services[key];
        }
    }


    [DefaultExecutionOrder(-999)]
    public class DependencyService : MonoBehaviour
    {
        protected void Awake()
        {
            DependencyLocator.Instance.Register(this);
        }
    }
}


