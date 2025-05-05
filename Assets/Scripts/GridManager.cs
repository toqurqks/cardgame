using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 4;   // 가로 칸 수
    public int height = 4;  // 세로 칸 수 (2줄 내 필드 + 2줄 상대 필드)

    public GameObject gridCellPrefab;  // 셀 프리팹 (사각형 또는 UI)
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] gridCells;
    public Vector2 spacing = new Vector2(1.1f, 2f);

    private void Start()
    {
        
        CreateGrid();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];

        // 그리드를 화면 중심 기준으로 배치하기 위해 오프셋 계산
        Vector2 centerOffset = new Vector2(width / 2f - 0.5f, height / 2f - 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                Vector2 spawnPosition = Vector2.Scale(gridPosition - centerOffset, spacing);
                Vector3 worldSpawnPosition = transform.position + new Vector3(spawnPosition.x, spawnPosition.y, 0);

                // 셀 생성
                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
                gridCell.transform.SetParent(transform);

                // 위치 정보 저장 (예: (0,3)은 EnemyField의 첫 번째 칸)
                string owner = y >= 2 ? "EnemyField" : "PlayerField";
                gridCell.name = $"{owner} ({x},{y})";

                // GridCell 스크립트에 위치 설정
                GridCell cell = gridCell.GetComponent<GridCell>();
                if (cell != null)
                {
                    cell.gridIndex = gridPosition;
                }

                // 배열에 저장
                gridCells[x, y] = gridCell;
            }
        }
    }

    // 특정 셀에 오브젝트 배치 시도
    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width &&
            gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();
            if (cell.cellFull) return false;

            GameObject newObj = Instantiate(obj, cell.transform.position, Quaternion.identity);
            newObj.transform.SetParent(transform);
            gridObjects.Add(newObj);

            cell.objectinCell = newObj;
            cell.cellFull = true;

            return true;
        }

        return false;
    }
}