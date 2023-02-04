using UnityEngine;
using UnityEngine.Events;

public interface IRoot
{
    UnityEvent OnPulledRoot { get; set; }

    public void OnGrab(Transform grabSource);
    public void OnPull();
    public void OnRelease();
    public void RootPulled();
}
