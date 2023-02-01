using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    [Range(0.1f, 2.0f)][SerializeField]
    private float scrollSpeed;
    private float offset;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentState != GameManager.gameState.Gameplay)
            return;
        offset += (scrollSpeed * Time.deltaTime) / 10f;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
