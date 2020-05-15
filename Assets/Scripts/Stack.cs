using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    private const float bound_size = 12f;
    private const float stack_moving_speed = 3f;
    private const float error_margin = 0.1f;

    public GameObject[] stack;
    Vector2 stackBounds = new Vector2(bound_size, bound_size);

    int stackIndex;
    int scoreCount = 0;
    int combo = 0;

    float tileTransition = 0f;
    float tileSpeed = 1.2f;
    float secondaryPos;

    bool movingOnX = true;

    Vector3 desiredPos;
    Vector3 lastTilePos;


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

    private void Update()
    {
        MoveTile();
        //Move stack
        transform.position = Vector3.Lerp(transform.position, desiredPos, stack_moving_speed * Time.deltaTime);
    }

    public void Click() //Click button on Scene
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
        tileTransition += Time.deltaTime * tileSpeed;
        if (movingOnX)
        {
            //move on X
            stack[stackIndex].transform.localPosition = new Vector3(Mathf.Sin(tileTransition) * bound_size, scoreCount, secondaryPos); 
        }
        else
        {
            //move on Z
            stack[stackIndex].transform.localPosition = new Vector3(secondaryPos, scoreCount, Mathf.Sin(tileTransition) * bound_size);

        }
    }

    private void SpawnTile()
    {
        lastTilePos = stack[stackIndex].transform.localPosition;
        stackIndex--;
        if (stackIndex < 0)
        {
            stackIndex = transform.childCount - 1;
        }

        desiredPos = (Vector3.down) * scoreCount;
        stack[stackIndex].transform.localPosition = new Vector3(0, scoreCount, 0);
    }

    private bool PlaceTile()
    {
        //присваиваем позицию новому tile относительно того где был предыдущий
        Transform transform = stack[stackIndex].transform;

        if (movingOnX)
        {
            float deltaX = lastTilePos.x - transform.position.x;

            if (Mathf.Abs(deltaX) > error_margin)
            {
                //cut the tile
                combo = 0;
                stackBounds.x = Mathf.Abs(deltaX);
                if (stackBounds.x <= 0)
                {
                    return false;
                }
            }
        }

        if (movingOnX)
        {
            secondaryPos = transform.localPosition.x;

        }
        else
        {
            secondaryPos = transform.localPosition.z;
        }

        movingOnX = !movingOnX;

        return true;
    }

    private void GameOver()
    {
        //Game over
    }


  


    
}
