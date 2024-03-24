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

        public void Register(Type key, Component value)
        {
            if (_services.ContainsKey(key)) return;

            _services[key] = value;
        }

        public Component Request<T>()
        {
            Type type = typeof(T);

            if (!_services.ContainsKey(type)) return default;

            return _services[type];
        }
    }


    [DefaultExecutionOrder(-999)]
    public class DependencyService : MonoBehaviour
    {
        protected void Awake()
        {
            DependencyLocator.Instance.Register(this.GetType(), this);
        }
    }
}


