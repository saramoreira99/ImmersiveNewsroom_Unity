using UnityEngine;

public class RotatingScreensOrbitFixed : MonoBehaviour
{
    [Header("Screens")]
    public GameObject[] screens;       // Screens um die Kamera
    public Transform centerPoint;      // Kamera oder Spieler
    public float radius = 3f;          // Abstand vom Mittelpunkt

    [Header("Höhe")]
    public bool autoHeight = true;
    public float manualHeight = 1.5f;  // nur relevant, wenn autoHeight = false

    [Header("Rotation der Gruppe")]
    public bool rotateGroup = true;
    public float orbitSpeed = 20f;     // Grad pro Sekunde

    [Header("Tilt / Neigung")]
    public float tiltAngle = 10f;      // leichte Neigung nach unten

    // Enum muss **außerhalb** von Header definiert werden
    public enum FrontAxis { X, Y, Z, NegX, NegY, NegZ }

    [Header("Front-Achse")]
    public FrontAxis front = FrontAxis.Z; // Welche Achse zeigt nach vorne?

    private float[] angles;            // aktuelle Winkel für jeden Screen

    void Start()
    {
        if (screens == null || screens.Length == 0) return;

        angles = new float[screens.Length];

        // Gleichmäßige Startverteilung auf 360°
        for (int i = 0; i < screens.Length; i++)
        {
            angles[i] = i * 360f / screens.Length;
        }
    }

    void Update()
    {
        if (screens == null || centerPoint == null) return;

        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i] == null) continue;

            // Orbit-Winkel aktualisieren
            if (rotateGroup)
                angles[i] += orbitSpeed * Time.deltaTime;

            // Position auf Kreis berechnen
            float rad = angles[i] * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad) * radius, 0f, Mathf.Sin(rad) * radius);
            Vector3 targetPos = centerPoint.position + offset;

            // Höhe anwenden
            targetPos.y = autoHeight ? centerPoint.position.y : manualHeight;
            screens[i].transform.position = targetPos;

            // Richtung zum Mittelpunkt
            Vector3 dirToCenter = centerPoint.position - screens[i].transform.position;

            // Front-Achse korrigieren
            Vector3 forward = dirToCenter;
            switch (front)
            {
                case FrontAxis.X:     forward = new Vector3(-dirToCenter.z, dirToCenter.y, dirToCenter.x); break;
                case FrontAxis.NegX:  forward = new Vector3(dirToCenter.z, dirToCenter.y, -dirToCenter.x); break;
                case FrontAxis.Y:     forward = new Vector3(dirToCenter.x, -dirToCenter.z, dirToCenter.y); break;
                case FrontAxis.NegY:  forward = new Vector3(dirToCenter.x, dirToCenter.z, -dirToCenter.y); break;
                case FrontAxis.Z:     forward = dirToCenter; break;
                case FrontAxis.NegZ:  forward = -dirToCenter; break;
            }

            // Rotation setzen
            if (forward != Vector3.zero)
                screens[i].transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

            // Tilt anwenden
            screens[i].transform.Rotate(Vector3.right, tiltAngle, Space.Self);
        }
    }
}