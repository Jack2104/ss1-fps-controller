using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Decreasing snappiness will decrease the time interval and thus the interpolation
    // will occur faster, thus resulting in faster movement but less smoothing
    [SerializeField] float snappiness = 100f;

    private float xAccumulator;
    private float yAccumulator;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Linearly interpolate from xAccumulator/yAccumulator to mouseX/mouseY
        xAccumulator = Mathf.Lerp(xAccumulator, mouseX, snappiness);
        yAccumulator += Mathf.Lerp(xAccumulator, mouseY, snappiness);

        transform.Rotate(0, xAccumulator, 0, Space.World);

        // Clamp vertical angle between -85 and 85 degrees. The rotation is directly set
        // as opposed to using Rotate() to allow for easy clamping
        yAccumulator = Mathf.Clamp(yAccumulator, -85, 85);
        Camera.main.transform.localEulerAngles = new Vector3(-yAccumulator, 0, 0);
    }
}

