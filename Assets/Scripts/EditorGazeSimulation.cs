using UnityEngine;

public class EditorGazeSimulator : MonoBehaviour
{
    public Camera mainCamera;  // Die Main Camera des XR Origin
    public float rayLength = 50f;

    void Update()
    {
        // Strahl aus der Kamera
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out var hit, rayLength))
        {
            Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * rayLength, Color.red);

            // Linksklick = Auswahl
            if (Input.GetMouseButtonDown(0))
            {
                hit.collider.SendMessage("Select", SendMessageOptions.DontRequireReceiver);
            }
        }

        // Mausbewegung = Blickrichtung simulieren
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        mainCamera.transform.Rotate(-mouseY, mouseX, 0);
    }
}