using OneFrame;
using UnityEngine;

public class ServiceExample : DependencyService
{
    public void Foo()
    {
        Debug.Log("<b>Service used</b>");

        DependencyLocator.Instance.UnRegister(this);
    }
}
