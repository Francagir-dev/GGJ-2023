using UnityEngine;

public interface IRoot
{
    public void onGrab(Transform grabSource);
    public void onPull();
    public void onRelease();
}
