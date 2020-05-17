using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    [SerializeField] GameObject[] stack;
    [SerializeField] Text scoreText;
    [SerializeField] RestartPanel restartPanel;
    [SerializeField] Color[] colorsTile;
    [SerializeField] AudioSource audioSource;


    private const float bound_size = 10f;
    private const float stack_moving_speed = 3f;
    private const float error_margin = 0.1f;


    Vector2 stackBounds = new Vector2(bound_size, bound_size);

    int stackIndex;
    int scoreCount = 0;
    int colorIndex = 0;

    float tileTransition = 0f;
    float tileSpeed = 2f;
    float secondaryPos;

    bool movingOnX = true;
    bool gameOver = false;

    Vector3 desiredPos;
    Vector3 lastTilePos;

    


    
    void Start()
    {
        stack = new GameObject[transform.childCount];
        scoreText.text = "Score: " + scoreCount;

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
            audioSource.Play();
            SetColorTile();
            scoreCount++;
            scoreText.text = "Score: " + scoreCount;

        }
        else
        {
            GameOver();
        }

        MoveTile();
    }

    private void MoveTile()
    {
        if (gameOver)
        {
            return;
        }

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
        stack[stackIndex].transform.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
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
                stackBounds.x -= Mathf.Abs(deltaX);
                if (stackBounds.x <= 0)
                {
                    return false;
                }

                float middle = lastTilePos.x + transform.position.x / 2;
                transform.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                transform.localPosition = new Vector3(middle - (lastTilePos.x / 2), scoreCount, lastTilePos.z);
            }
            else
            {
                transform.localPosition = lastTilePos + Vector3.up;
            }
        }
        else
        {
            float deltaZ = lastTilePos.z - transform.position.z;

            if (Mathf.Abs(deltaZ) > error_margin)
            {
                //cut the tile
                stackBounds.y -= Mathf.Abs(deltaZ);
                if (stackBounds.y <= 0)
                {
                    return false;
                }

                float middle = lastTilePos.z + transform.position.z / 2;
                transform.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                transform.localPosition = new Vector3(lastTilePos.x, scoreCount, middle - (lastTilePos.z / 2));
            }
            else
            {
                transform.localPosition = lastTilePos + Vector3.up;
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
    
    private void SetColorTile()
    {
        Renderer tileRenderer = stack[stackIndex].GetComponent<Renderer>();
        tileRenderer.material.color = colorsTile[colorIndex];

        if (colorIndex == colorsTile.Length - 1)
        {
            colorIndex = 0;
        }
        else
        {
            colorIndex++;
        }
    }

    private void GameOver()
    {
        restartPanel.gameObject.SetActive(true);
        restartPanel.ShowScore(scoreCount);
        scoreText.enabled = false;
        
        gameOver = true;
        stack[stackIndex].AddComponent<Rigidbody>();
    }



  


    
}
