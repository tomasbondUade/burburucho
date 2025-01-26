using UnityEngine;

public class LandControl : MonoBehaviour
{
    [SerializeField] private float speed;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        sr.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
