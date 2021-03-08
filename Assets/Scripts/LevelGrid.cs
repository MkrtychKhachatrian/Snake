using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class LevelGrid {
    
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int wight;
    private int height;
    private Snake snake;
 
    public LevelGrid(int wight, int height) {
        this.wight = wight;
        this.height = height;
    }

    public void Setup (Snake snake) {
        this.snake = snake;

        SpawnFood();
    }

    private void SpawnFood() {
        do {
            foodGridPosition = new Vector2Int(Random.Range(0, wight), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);
        
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
        if (snakeGridPosition == foodGridPosition) {
            Object.Destroy(foodGameObject);
            SpawnFood();
            GameHandler.AddScore();
            return true;
        } else {
            return false;
        }
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        if (gridPosition.x < 0) {
            gridPosition.x = wight - 1;
        }
        if (gridPosition.x > wight - 1) {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0) {
            gridPosition.y = height - 1;
        }
        if (gridPosition.y > height - 1) {
            gridPosition.y = 0;
        }
        return gridPosition;
    }
}
