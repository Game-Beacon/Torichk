using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using  UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MapV2 : MonoBehaviour
{
    private Transform mapHolder;
    private int cols;
    private int rows;
    public GameObject[] ObjArray;//
    public GameObject[] FloorArray;
    public GameObject[] MonsterArray;
    public GameObject[] TrapArray;//陷阱物件 

    List<MapUnit> mapUnits = new List<MapUnit>();
    List<MapUnit> houseList = new List<MapUnit>();
    List<MapUnit> FloorList = new List<MapUnit>();
    List<GameObject> SigelList = new List<GameObject>();
    
    
    //0=null,1 =詩人, 2=王蟲,3=樹,4=水晶,5=玩家,6=怪物,7=陷阱
    //str =m1,m2,m3
    #region MapStr

    string m1 = //(ok)0=null,1 =詩人, 2=王蟲,3=樹,4=水晶,5=玩家,6=怪物,7=陷阱
        "1111111111111111111111111111111111111111" +
        "1000000000001100000000000000000000000001" +
        "1021111110301100333333333110011111111101" +
        "1011111110301110000000000111011111112201" +
        "1000000000301111111111110061000000000601" +

        "1301011000000001111111110111033001111111" +
        "1301611001111100000000000111011001111111" +
        "1001011001111111111111100111011000000001" +
        "1002011001000011111111103111011031111101" +
        "1100011071021011111111203110011001121101" +

        "1100000001011020001100000110111301100001" +
        "1111111101000000001101111110111301111101" +
        "1121111101111110701102111110000001111101" +
        "1007000001111110001100003110101101100001" +
        "1300007000000113000000000110101103000111" +

        "1001111111103110031111100110101100000111" +
        "1001111111103110031111100110101100000111" +
        "1301110111103110000000000000001130111121" +
        "1302110111103110311011011301100000000001" +
        "1000000011100110312021011301111111110011" +//20綠6,7藍

        "1010000011100110000600000001121111110021" +
        "1121110311100300000010010001160000000001" +
        "1101110011110000311110011111111110011111" +
        "1161110011110330011110011111111110011211" +
        "1102210011111111111110011000000000011601" +

        "1000000011111111111100001001111111011001" +
        "1301111111100000000000100011112111011031" +
        "1301112111101111111111110010011110011001" +
        "1000000000001210000000110323011123011001" +
        "1100001100000000001100110000000000021301" +//30

        "1111701100000000001111110011113011000001" +
        "1111001100111111101111110111110011111001" +
        "1211002111111111111111110120000000111031" +
        "1011000112000000010011211170011110111001" +
        "1611011110610100000000011100121210111001" +

        "1000011110011100111100011100100000030011" +
        "1000000121002111111110021130101001000111" +
        "1011000011000111000110601000101012001111" +
        "1011000000070000000000000000000100111111" +
        "1111111111111111111111111111111111111111";

    string m2 = //(ok)0=null,1 =詩人, 2=王蟲,3=樹,4=水晶,5=玩家,6=怪物,7=陷阱
        "11111111111111111111" +
        "10001100600000000001" +
        "10001101111111111101" +
        "10001101111111111101" +
        "10601140000006011001" +
        
        "10001100000000011011" +
        "16001101111111011011" +
        "10000601111111011001" +
        "10001000000000000041" +
        "10001111100011101101" +
        
        "10601111102000001161" +
        "10001100000011101101" +
        "14001101100111101101" +
        "10001101160000001101" +
        "10001101100060011101" +
        
        "10061101111111111101" +
        "10001100011111161101" +
        "10001000000000000001" +
        "15000600000000000601" +
        "11111111111111111111";

    string m3 = //(ok)0=null,1 =詩人, 2=王蟲,3=樹,4=水晶,5=玩家
        "11111111111111111111" +
        "10000000000004000001" +
        "10104000004000001141" +
        "14100004000000001101" +
        "10101111111111101101" +

        "10101111111111101101" +
        "10140000004000004141" +
        "10101111111111100101" +
        "10001111111111100001" +
        "10500040000400001101" +

        "10000000400000001201" +
        "10041111111111100001" +
        "10101111111111100101" +
        "10100004000000040101" +
        "14101111111111101101" +

        "10101111111111101101" +
        "10100000000000001141" +
        "10100400000400001101" +
        "10400000400000000001" +
        "11111111111111111111";

    #endregion
    



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
        Init();
        //CreatImportObj();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var sigel in SigelList)
            {
                sigel.SetActive(false);
            }
        }
    }

    void CreatImportObj() {//0=null,1 =詩人, 2=王蟲,3=樹,4=水晶,5=玩家,6=怪物,7=陷阱
        int k;
        Vector2[] v = new Vector2[2];
        do
        {
            k = Random.Range(0,mapUnits.Count-1);
        } while (mapUnits[k].IsWhat!=0);
        //Debug.Log(mapUnits[k].IsWhat + "(" +mapUnits[k].vector2.x + "," + mapUnits[k].vector2.y + ")");
        GameObject go = Instantiate(ObjArray[5], new Vector3(mapUnits[k].vector2.x, mapUnits[k].vector2.y, -2), Quaternion.identity) as GameObject;
        go.transform.SetParent(mapHolder);
        v[0] = mapUnits[k].vector2;
        //�ЫإX�f
        do
        {
            k = Random.Range(0, houseList.Count - 1);
        } while (houseList[k].IsWhat != 2 || Vector2.Distance(v[0], houseList[k].vector2)<35);
        Debug.Log(houseList[k].IsWhat + "(" +houseList[k].vector2.x + "," + houseList[k].vector2.y + ")");
        go = Instantiate(ObjArray[6], new Vector3(houseList[k].vector2.x, houseList[k].vector2.y, -2), Quaternion.identity) as GameObject;
        go.transform.SetParent(mapHolder);
        v[1] = mapUnits[k].vector2;

        do
        {
            k = Random.Range(0, houseList.Count - 1);
        } while (houseList[k].IsWhat != 2 || Vector2.Distance(v[1], houseList[k].vector2) < 15);
        Debug.Log(houseList[k].IsWhat + "(" + houseList[k].vector2.x + "," + houseList[k].vector2.y + ")");
        go = Instantiate(ObjArray[6], new Vector3(houseList[k].vector2.x, houseList[k].vector2.y, -2), Quaternion.identity) as GameObject;
        go.transform.SetParent(mapHolder);
        
    }

    
    
    bool IsInside(Vector2[] vL,Vector2 v) {
        if ((v.x- vL[0].x)*(v.x-vL[1].x)<0 && (v.y - vL[0].y) * (v.y - vL[1].y) < 0)
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }
    
    void RotateSigel(GameObject gameObject,Vector2 target) {//選轉傳入物件到指定座標

        Vector2 v = new Vector2(target.x - gameObject.transform.position.x, target.y -gameObject.transform.position.y);
        v.Normalize();
        Vector2 v2 = Vector2.up;
        v2.Normalize();
        float theta = Mathf.Acos(Vector2.Dot(v2, v));
        gameObject.transform.rotation = Quaternion.Euler(0, 0, theta * 180 / Mathf.PI * (target.x - gameObject.transform.position.x < 0 ? 1 : -1));
    }
    void Map(string mapStr)
    {
        var l =  GetNextChars(mapStr,1);
        List<int> i = new List<int>();
        foreach (var item in l)
        {
            i.Add(int.Parse(item));
        }
        int count = 0;
        mapHolder = new GameObject("Map").transform;//0=null,1 =詩人, 2=王蟲,3=樹,4=水晶,5=玩家,6=怪物,7=陷阱
        for (int x = cols; x > 0; x--)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject go;
                mapUnits.Add(new MapUnit(i[count], new Vector2(y, x)));



                if (i[count] != 0)
                {
                    if (i[count] == 1)
                    {
                        go = Instantiate(ObjArray[1], new Vector3(y, x, -1), Quaternion.identity) as GameObject;
                        go.transform.SetParent(mapHolder);
                        if (Random.Range(0,10)<3)
                        {
                            CreatSigel(new Vector2(y,x)); 
                        }
                    }
                    
                    if (i[count] == 2)
                    {
                        houseList.Add(new MapUnit(i[count], new Vector2(y, x)));
                        go = Instantiate(ObjArray[2], new Vector3(y, x, -1), Quaternion.identity) as GameObject;
                        go.transform.SetParent(mapHolder);

                    }
                    if (i[count] ==6)
                    {
                        go = Instantiate(MonsterArray[Random.Range(0,MonsterArray.Length-1)], new Vector3(y, x, -1), Quaternion.identity) as GameObject;
                        go.transform.SetParent(mapHolder);
                    }
                    else if (i[count] ==5)
                    {
                        go = Instantiate(ObjArray[i[count]], new Vector3(y, x, -1), Quaternion.identity) as GameObject;
                        go.transform.SetParent(mapHolder);
                    }else if (i[count] == 7)
                    {
                        go = Instantiate(TrapArray[Random.Range(0,TrapArray.Length-1)], new Vector3(y, x, -1), Quaternion.identity) as GameObject;
                        go.transform.SetParent(mapHolder);
                    }else
                    {
                        go = Instantiate(ObjArray[i[count]], new Vector3(y, x, -1), Quaternion.identity) as GameObject;
                        go.transform.SetParent(mapHolder);
                    }

                }
                else
                {
                    FloorList.Add(new MapUnit(i[count], new Vector2(y, x)));
                }
                
                    go = Instantiate(FloorArray[0], new Vector3(y, x, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                    count++;
            }
        }

    }

    void CreatSigel(Vector2 vector2)
    {
        
        GameObject go2 = Instantiate(ObjArray[6], new Vector3(vector2.x, vector2.y-0.3f, -2), Quaternion.identity) as GameObject;
        RotateSigel(go2,PlayerCtrl.PlayerPosition);//這邊需要改成假出口
        go2.transform.SetParent(mapHolder);
        SigelList.Add(go2);
    }
    
    void Init()
    {
        switch (SceneManager.GetActiveScene().name.ToLower())
        {
            case "m1" :
                rows = 40;
                cols = 40;
                Map(m1);
                break;
            case "m2" :
                rows = 20;
                cols = 20;
                Map(m2);
                break;
            case "m3" :
                rows = 20;
                cols = 20;
                Map(m3);
                break;
            
            default:
                
                break;
        }
        
    }
}


struct MapUnit
{
    public int IsWhat;//0=null,1 =詩人, 2=王蟲,3=樹,4=水晶,5=玩家,6=怪物,7=陷阱
    public Vector2 vector2;

    public MapUnit(int isWhat, Vector2 vector2)
    {
        IsWhat = isWhat;
        this.vector2 = vector2;
    }
}
