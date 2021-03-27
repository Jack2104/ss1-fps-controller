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
        float currentVerticalAngle = Mathf.Abs(Camera.main.transform.localEulerAngles.x);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Linearly interpolate from xAccumulator/yAccumulator to mouseX/mouseY
        // Time.deltaTime makes the camera movement frame-rate independent
        xAccumulator = Mathf.Lerp(xAccumulator, mouseX, snappiness * Time.deltaTime);
        yAccumulator = Mathf.Lerp(yAccumulator, mouseY, snappiness * Time.deltaTime);

        transform.Rotate(0, xAccumulator, 0, Space.World);
        Camera.main.transform.Rotate(-yAccumulator, 0, 0);

        // Clamp vertical angle between -90 and 90 degrees
        float verticalAngle = yAccumulator * CalculateMultiple(currentVerticalAngle);//Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 0, 360);
        Camera.main.transform.localEulerAngles = new Vector3(verticalAngle, 0, 0);
    }

    private float CalculateMultiple(float verticalAngle)
    {
        // Mathf.Exp returns e raised to the given power (which is -0.05 * verticalAngle, where
        // verticalAngle is the X-value of the function f(x) = 1.011233793e^(-0.05x) - 0.011233793f
        return 1.011233793f * Mathf.Exp(-0.05f * verticalAngle) - 0.011233793f;
    }
}

