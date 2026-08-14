using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light _light;

    void Awake()
    {
        _light = GetComponent<Light>();
    }

    public void Toggle()
    {
        _light.enabled = !_light.enabled;
    }
}
