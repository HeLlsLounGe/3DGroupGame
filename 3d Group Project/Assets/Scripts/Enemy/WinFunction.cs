using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFunction : MonoBehaviour
{
    public int Spawners;
    public int Enemies;
    GameObject door;
    WinDoor winDoor;
    void Awake()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        winDoor = door.GetComponent<WinDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinCheck()
    {
        if(Spawners == 0 && Enemies == 0)
        {
            Debug.Log("You're are winner part 1");
            winDoor.DoorOpen();
        }
    }
}
