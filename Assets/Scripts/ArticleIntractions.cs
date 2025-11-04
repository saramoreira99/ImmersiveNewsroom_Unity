using UnityEngine;

public class ArticleInteraction : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }


    void OnMouseEnter()
    {
        if (rend != null)
            rend.material.color = Color.red; 
    }

    void OnMouseExit()
    {
        if (rend != null)
            rend.material.color = originalColor;
    }

    void OnMouseDown()
    {
        Debug.Log("Artikel wurde ausgewählt!");
        transform.localScale *= 1.2f; 
    }
}
