using UnityEngine;

public class UIWobble : MonoBehaviour
{
    public float speed = 1f; // Speed of the wobble
    public float amount = 0.1f; // Amount of wobble
    private Vector3 originalScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float wobble = Mathf.PingPong(Time.time * speed, 1f) * amount;

        transform.localScale = originalScale + new Vector3(wobble, wobble, 0);
    }
}
