using UnityEngine;

namespace OneFrame.Dependencies.Runtime
{
    [DefaultExecutionOrder(-999)]
    public class DependencyService : MonoBehaviour
    {
        protected void Awake()
        {
            DependencyLocator.Instance.Register(this);
        }
    }
}


