using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapV2 : MonoBehaviour
{
    private Transform mapHolder;
    private int cols;
    private int rows;
    public GameObject[] ObjArray;
    public GameObject[] FloorArray;
    public int rowsUnit;
    public int colsUnit;
    public int rowsCount;
    public int colsCount;

    string m1 = //0=null,1 = 詩人,2= 王蟲,3=樹,4=水晶,5=玩家
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "" +
        "";

    string m2 = //0=null,1 = 詩人,2= 王蟲,3=樹,4=水晶,5=玩家
        "11111111111111111111" +
        "10001100000000000001" +
        "10001101111111111101" +
        "10001101111111111101" +
        "10001140000000011001" +
        "10001100000000011011" +
        "10001101111111011011" +
        "10000001111111011001" +
        "10001000000000000041" +
        "10001111100011101101" +
        "10001111102000001101" +
        "10001100000011101101" +
        "14001101100111101101" +
        "10001101100000001101" +
        "10001101100000011101" +
        "10001101111111111101" +
        "10001100011111101101" +
        "10001000000000000001" +
        "15000000000000000001" +
        "11111111111111111111";

    string m3 = //0=null,1 = 詩人,2= 王蟲,3=樹,4=水晶,5=玩家
        "11111111111111111111" +
        "10000000000002000001" +
        "10104000004000001141" +
        "14100004000000001101" +
        "10101111111111101101" +

        "10101111111111101101" +
        "10140000004000004141" +
        "10101111111111100101" +
        "10001111111111100001" +
        "10500040000400001101" +

        "10500000400000001201" +
        "10041111111111100001" +
        "10101111111111100101" +
        "10100000400000040101" +
        "14101111111111101101" +

        "10101111111111101101" +
        "10100000000000001141" +
        "10100400000400001101" +
        "10400000400000000001" +
        "11111111111111111111";



    IEnumerable<string> GetNextChars(string str, int iterateCount)
    {
        var words = new List<string>();

        for (int i = 0; i < str.Length; i += iterateCount)
            if (str.Length - i >= iterateCount) words.Add(str.Substring(i, iterateCount));
            else words.Add(str.Substring(i, str.Length - i));

        return words;
    }
    // Start is called before the first frame update
    void Start()
    {
        Map();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Map()
    {
        var l =  GetNextChars(m2,1);
        List<int> i = new List<int>();
        foreach (var item in l)
        {
            i.Add(int.Parse(item));
        }
        int count = 0;
        GetRowsAndCols();
        mapHolder = new GameObject("Map").transform;// 設置一個父類管理生成的地圖
        for (int x = cols; x > 0; x--)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject go;
                if (i[count] != 0)
                {
                    go = GameObject.Instantiate(ObjArray[i[count]], new Vector3(y, x, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
                    go = Instantiate(FloorArray[0], new Vector3(y, x, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                    count++;
            }
        }

    }

    void GetRowsAndCols()
    {
        rows = rowsUnit * rowsCount;
        cols = colsUnit * colsCount;
    }
}


struct MapUnit
{
    public int IsWhat;//0=null,1 = 詩人,2= 王蟲,3=樹,4=水晶,5=玩家
    public Vector2 vector2;

    public MapUnit(int isWhat, Vector2 vector2)
    {
        IsWhat = isWhat;
        this.vector2 = vector2;
    }
}
