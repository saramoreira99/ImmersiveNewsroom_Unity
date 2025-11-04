using UnityEngine;

public class Article : MonoBehaviour
{
    public string articleTitle = "Default Title";

    public void Select()
    {
        Debug.Log("Artikel ausgewählt: " + articleTitle);
        
        GetComponent<Renderer>().material.color = Color.cyan;
    }
}
