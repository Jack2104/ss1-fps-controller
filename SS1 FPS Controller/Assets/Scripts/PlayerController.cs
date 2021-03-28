using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float snappiness = 10f;

    private float xAccumulator;
    private float yAccumulator;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float currentVerticalAngle = Camera.main.transform.localEulerAngles.x;

        // Convert the angle to a signed angle; change the angle range from [0, 360] to [-180, 180]
        currentVerticalAngle = (currentVerticalAngle > 180) ? currentVerticalAngle - 360 : currentVerticalAngle;

        // Linearly interpolate from xAccumulator/yAccumulator to mouseX/mouseY
        // Time.deltaTime makes the camera movement frame-rate independent
        xAccumulator = Mathf.Lerp(xAccumulator, mouseX, snappiness * Time.deltaTime);
        yAccumulator = Mathf.Lerp(yAccumulator, mouseY, snappiness * Time.deltaTime) * CalculateMultiple(currentVerticalAngle);

        transform.Rotate(0, xAccumulator, 0, Space.World); // Rotate the player itself
        Camera.main.transform.Rotate(-yAccumulator, 0, 0); // Rotate the camera
    }

    private float CalculateMultiple(float verticalAngle)
    {
        // Find the absolute value so that negative angles can be inputted (reflect the graph in the Y-axis)
        verticalAngle = Mathf.Abs(verticalAngle);

        // Mathf.Exp returns e raised to the given power (which is -0.05 * verticalAngle, where
        // verticalAngle is the X-value of the function f(x) = 1.011233793e^(-0.05x) - 0.011233793f
        return 1.011233793f * Mathf.Exp(-0.05f * verticalAngle) - 0.011233793f;
    }
}

