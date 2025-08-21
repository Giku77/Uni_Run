using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float scrollSpeed = 10f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene. Please ensure it is tagged correctly.");
        }
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        if (transform.position.x < -20f && gameObject.CompareTag("Sky"))
        {
            transform.position = new Vector2(20f, transform.position.y);
        }
    }
}
