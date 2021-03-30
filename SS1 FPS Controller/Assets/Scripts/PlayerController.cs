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
        xAccumulator = Mathf.Lerp(xAccumulator, mouseX, snappiness * Time.deltaTime);

        // Apply the vertical camera movement multiple if the vertical angle <= -75 degrees or >= 75 degrees
        if (currentVerticalAngle >= 75 || currentVerticalAngle <= -75)
            yAccumulator = Mathf.Lerp(yAccumulator, mouseY, snappiness * Time.deltaTime) * CalculateMultiple(currentVerticalAngle);
        else
            yAccumulator = Mathf.Lerp(yAccumulator, mouseY, snappiness * Time.deltaTime);

        transform.Rotate(0, xAccumulator, 0, Space.World); // Rotate the player itself
        Camera.main.transform.Rotate(-yAccumulator, 0, 0); // Rotate the camera
    }

    private float CalculateMultiple(float verticalAngle)
    {
        // Find the absolute value so that negative angles can be inputted (reflect the graph in the Y-axis)
        verticalAngle = Mathf.Abs(verticalAngle);
        float power = -0.299999998254f * (verticalAngle - 75);

        // Mathf.Exp returns e raised to the given power
        return 1.011233793f * Mathf.Exp(power) - 0.011233793f;
    }
}

