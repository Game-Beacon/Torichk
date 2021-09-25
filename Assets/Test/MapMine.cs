using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMine : MonoBehaviour
{
    public GameObject[] ImportArray;//0�����a1�����X�f2���u�X�f3.4.5������
    public GameObject[] MonArray;
    public GameObject[] WallArray;
    private Transform mapHolder;
    public GameObject[] OutWallArray;
    public GameObject[] FloorArray;
    public int rowsCount_25;
    public int colsCount_25;
    public int MonCount;
    int rows;  //�w�q�a�Ϫ���C�C
    int cols;
    int[] foxXY = new int[2];

    List<int> XList = new List<int>();
    List<int> YList = new List<int>();


    private void Start()
    {
        Map();
        CreatMon(MonCount);
        CreatTree();
    }

    void CreatTree() {

        for (int i = 0; i < rows-1; i++)
        {
            for (int j = 0; j < cols-1; j++)
            {
                if (IsCanCreat(XList,j)&&IsCanCreat(YList,i))
                {
                    GameObject go = GameObject.Instantiate(WallArray[0], new Vector3(j, i, -1f), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
            }
        }
    }
    void CreatMon(int monCount) {

        for (int i = 0; i <monCount ; i++)
        {
            Vector2[] xy = new Vector2[rowsCount_25*colsCount_25];//3x2
            int xyCount = 0;
            for (int r = 0; r < rowsCount_25; r++)
            {
                for (int l = 0; l < colsCount_25; l++)
                {
                    int k1 = Random.Range(r * 25, (r + 1) * 25-1);
                    if (IsCanCreat(YList, k1)) YList.Add(k1);
                    xy[xyCount].y = k1;

                    int k2 = Random.Range(l * 25, (l + 1) * 25-1);
                    if (IsCanCreat(XList, k2)) XList.Add(k2);
                    xy[xyCount].x = k2;
                    xyCount++;
                }
            }
            foreach (var item in xy)
            {
                int randomMon = Random.Range(0,MonArray.Length);
                GameObject go = GameObject.Instantiate(MonArray[randomMon], new Vector3(item.x,item.y, -1f), Quaternion.identity) as GameObject;
                go.transform.SetParent(mapHolder);
            }
        }
    }
    bool IsCanCreat(List<int> TargetList,int AddNumber) {
        foreach (var item in TargetList)
        {
            if (item == AddNumber) return false;
        }
        return true;
    }
    void Map()
    {
        GetRowsAndCols();
        mapHolder = new GameObject("Map").transform;// �]�m�@�Ӥ����޲z�ͦ����a��
        for (int x = -1; x < cols; x++)
        {
            for (int y = -1; y < rows; y++)
            {
                if (x == -1 || y == -1 || x == cols - 1 || y == rows - 1)//�a�ϳ̥~���@��O����
                {
                    int index = Random.Range(0, OutWallArray.Length);
                    GameObject go = GameObject.Instantiate(OutWallArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
                else// ��l�O�a�O
                {
                    int index = Random.Range(0, FloorArray.Length);
                    GameObject go = GameObject.Instantiate(FloorArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
            }
        }
    }
    void GetRowsAndCols()
    {
        rows = 25 * rowsCount_25 + 2;
        cols = 25 * colsCount_25 + 2;
    }
}
