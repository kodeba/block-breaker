using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float screenWidthInUnits = 16f;

    [SerializeField]
    private float minX = 1f;

    [SerializeField]
    private float maxX = 15f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float posX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        posX = Mathf.Clamp(posX, minX, maxX);
        float posY = transform.position.y;
        Vector2 pos = new Vector2(posX, posY);
        transform.position = pos;
    }
}
