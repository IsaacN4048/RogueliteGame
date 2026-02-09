using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifetime;
    public float offsetX;
    public float offsetY;
    public Vector3 intensity;

    private void Start()
    {
        Destroy(gameObject, lifetime);

        transform.localPosition += new Vector3(Random.Range(-offsetX, offsetX),offsetY,0); //VERY NONPERFORMANT
    }


}
