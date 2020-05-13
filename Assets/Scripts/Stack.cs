using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    public GameObject tile;
    public Text scoreText;

    public Transform startPositionTile;

    GameObject lastTile;
    GameObject currentTile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createTileAndMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator createTileAndMove()
    {
        currentTile = Instantiate(tile, startPositionTile.position, Quaternion.identity);

        while (true)
        {
            currentTile.transform.DOMoveX(currentTile.transform.position.x + 60, 15f);
            yield return new WaitForSeconds(5f);
            currentTile.transform.DOMoveX(currentTile.transform.position.x - 60, 15f);
            yield return new WaitForSeconds(5f);

        }
    }
}
