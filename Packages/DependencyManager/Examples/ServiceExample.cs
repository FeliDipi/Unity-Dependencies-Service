using OneFrame.Dependencies.Runtime;
using UnityEngine;

namespace OneFrame.Dependencies.Examples
{
    public class ServiceExample : DependencyService
    {
        protected new void Awake()
        {
            base.Awake();
            Debug.Log("Custom Awake Called");
        }

        public void Foo()
        {
            Debug.Log("<b>Service used</b>");

            DependencyLocator.Instance.UnRegister(this);
        }
    }
}
