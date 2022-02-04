using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlayerAction : MonoBehaviour
{
    private Vector2 position;
    private Vector2 screenToWorldPointPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.GameScenes == GameMaster.Scene.PLAY)
        {
            gameObject.SetActive(true);

            position = Input.mousePosition;
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
            gameObject.transform.position = screenToWorldPointPosition;
        }
    }
    


}
