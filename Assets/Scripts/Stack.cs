using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    private const float bound_size = 10f; 

    public GameObject[] stack;

    int stackIndex;
    int scoreCount = 0;

    float tileTransition = 0f;
    float tileSpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        stack = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            stack[i] = transform.GetChild(i).gameObject;
        }
            stackIndex = transform.childCount - 1;
    }

    public void Click()
    {
        if (PlaceTile())
        {
            SpawnTile();
            scoreCount++;
        }
        else
        {
            GameOver();
        }

        MoveTile();
    }

    private void MoveTile()
    {

    }

    private void SpawnTile()
    {
        stackIndex--;
        if (stackIndex < 0)
        {
            stackIndex = transform.childCount - 1;
        }

        stack[stackIndex].transform.localPosition = new Vector3(0, scoreCount, 0);
    }

    private bool PlaceTile()
    {
        return true;
    }

    private void GameOver()
    {

    }


  


    
}
