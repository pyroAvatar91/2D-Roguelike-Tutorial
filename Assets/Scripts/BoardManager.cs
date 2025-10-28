using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour {
    public class CellData {
        public bool Passable;
        public GameObject ContainedObject;
    }
    
    private CellData[,] boardData;
    private Grid grid;
    private Tilemap tilemap;
    private List<Vector2Int> emptyCellsList = new List<Vector2Int>();
    
    [SerializeField] private PlayerController player;
    [SerializeField] private int levelWidth;
    [SerializeField] private int levelHeight;
    [SerializeField] private Tile[] groundTiles; 
    [SerializeField] private Tile[] wallTiles;
    [SerializeField] private GameObject foodPrefab;

    public void Init() {
        tilemap = GetComponentInChildren<Tilemap>();
        grid = GetComponentInChildren<Grid>();
        emptyCellsList = new List<Vector2Int>();
        boardData = new CellData[levelWidth, levelHeight];

        for (int y = 0; y < levelHeight; ++y) {
            for (int x = 0; x < levelWidth; ++x) {
                Tile tile;
                boardData[x, y] = new CellData();
                
                if (x == 0 || y == 0 || x == levelWidth - 1 || y == levelHeight - 1) {
                    tile = wallTiles[Random.Range(0, wallTiles.Length)];
                    boardData[x, y].Passable = false;
                }
                else {
                    tile = groundTiles[Random.Range(0, groundTiles.Length)];
                    boardData[x, y].Passable = true;
                    emptyCellsList.Add(new Vector2Int(x, y));
                }
                
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
        
        emptyCellsList.Remove(new Vector2Int(1, 1));
        GenerateFood();
    }

    public Vector3 CellToWorld(Vector2Int cellIndex) {
        return grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex) {
        if (cellIndex.x < 0 || cellIndex.x >= levelWidth || cellIndex.y < 0 || cellIndex.y >= levelHeight) {
            return null;
        }
        return boardData[cellIndex.x, cellIndex.y];
    }

    private void GenerateFood() {
        int foodCount = 5;
        for (int i = 0; i < foodCount; ++i) {
            int randomIndex = Random.Range(0, emptyCellsList.Count);
            Vector2Int coordinates = emptyCellsList[randomIndex];
            
            emptyCellsList.RemoveAt(randomIndex);
            CellData cellData = boardData[coordinates.x, coordinates.y];
            GameObject newFood = Instantiate(foodPrefab);
            newFood.transform.position = CellToWorld(coordinates);
            cellData.ContainedObject = newFood;
        }
    }


}
