using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreat : MonoBehaviour
{
    List<Transform> mapTrasform = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


class Item {
    Transform transform;
    int mapObj;//如果是0的話為空，1是角色，2是出口，3是隱藏出口，4是物件，5是怪物，6是陷阱，7是場地

}
