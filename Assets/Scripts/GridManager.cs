using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 4;   // ���� ĭ ��
    public int height = 4;  // ���� ĭ �� (2�� �� �ʵ� + 2�� ��� �ʵ�)

    public GameObject gridCellPrefab;  // �� ������ (�簢�� �Ǵ� UI)
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

        // �׸��带 ȭ�� �߽� �������� ��ġ�ϱ� ���� ������ ���
        Vector2 centerOffset = new Vector2(width / 2f - 0.5f, height / 2f - 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                Vector2 spawnPosition = Vector2.Scale(gridPosition - centerOffset, spacing);
                Vector3 worldSpawnPosition = transform.position + new Vector3(spawnPosition.x, spawnPosition.y, 0);

                // �� ����
                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
                gridCell.transform.SetParent(transform);

                // ��ġ ���� ���� (��: (0,3)�� EnemyField�� ù ��° ĭ)
                string owner = y >= 2 ? "EnemyField" : "PlayerField";
                gridCell.name = $"{owner} ({x},{y})";

                // GridCell ��ũ��Ʈ�� ��ġ ����
                GridCell cell = gridCell.GetComponent<GridCell>();
                if (cell != null)
                {
                    cell.gridIndex = gridPosition;
                }

                // �迭�� ����
                gridCells[x, y] = gridCell;
            }
        }
    }

    // Ư�� ���� ������Ʈ ��ġ �õ�
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