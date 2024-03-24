using OneFrame;
using UnityEngine;

public class Example : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Dependency<Camera> _dependencyA;
    [SerializeField] private Dependency<Camera> _dependencyB;
    [SerializeField] private Dependency<ServiceExample> _dependencyC;

    [Header("Other References")]
    [SerializeField] private Rigidbody2D _rb;

    private void Start()
    {
        Debug.Log($"<b>Dependency A: {_dependencyA.Resolve()}</b>");
        Debug.Log($"<b>Dependency B: {_dependencyB.Resolve()}</b>");
        Debug.Log($"<b>Dependency C: {_dependencyC.Resolve()}</b>");

        _dependencyC.Value.Foo();
    }
}
