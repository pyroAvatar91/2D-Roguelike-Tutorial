using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour {
    public class CellData {
        public bool Passable;
    }
    
    private CellData[,] boardData;
    private Grid grid;
    private Tilemap tilemap;
    
    [SerializeField] private PlayerController player;
    [SerializeField] private int levelWidth;
    [SerializeField] private int levelHeight;
    [SerializeField] private Tile[] groundTiles; 
    [SerializeField] private Tile[] wallTiles;

    private void Start() {
        tilemap = GetComponentInChildren<Tilemap>();
        grid = GetComponentInChildren<Grid>();
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
                }
                
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
        
        player.Spawn(this, new Vector2Int(1,1));
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


}
