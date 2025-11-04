using UnityEngine;

public class ArticleManager : MonoBehaviour
{
    [Header("Prefab & Anzahl")]
    public GameObject articlePrefab;
    public int numberOfArticles = 8;
    public float radius = 5f;
    public float height = 1.5f;

    [Header("Rotation der gesamten Gruppe")]
    public bool rotateContainer = true;
    public float rotationSpeed = 20f;

    [Header("Ausrichtung der einzelnen Artikel")]
    public float frontYRotation = 180f;  // Frontface zur Mitte
    public float tiltAngle = 10f;        // Konstante Neigung nach unten

    void Start()
    {
        SpawnArticles();
    }

    void Update()
    {
        if (rotateContainer)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        foreach (Transform child in transform)
        {
            child.LookAt(Vector3.zero);

            child.Rotate(0f, frontYRotation, 0f, Space.Self);

            child.rotation = Quaternion.Euler(tiltAngle, child.rotation.eulerAngles.y, 0f);
        }
    }

    void SpawnArticles()
    {
        for (int i = 0; i < numberOfArticles; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfArticles;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            Vector3 pos = new Vector3(x, height, z);

            GameObject article = Instantiate(articlePrefab, pos, Quaternion.identity, transform);

            article.transform.LookAt(Vector3.zero);

            article.transform.Rotate(0f, frontYRotation, 0f, Space.Self);

            article.transform.rotation = Quaternion.Euler(tiltAngle, article.transform.rotation.eulerAngles.y, 0f);
        }
    }
}