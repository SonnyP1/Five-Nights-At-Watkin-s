using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arevalo : Sidler
{

    public override void Start()
    {
        base.Start();
        currentRoom = Room.Restrooms;
    }
    public override Transform ChooseAdjacentRoom()
    {
        if (currentRoom == Room.Restrooms)
        {
            Transform[] adjacentRooms = { _roomDictRoomTrans[Room.DiningArea] };
            currentRoom = _roomDictTransRoom[adjacentRooms[0]];
            return adjacentRooms[0];
        }
        else if (currentRoom == Room.DiningArea)
        {
            Transform[] adjacentRooms = { _roomDictRoomTrans[Room.Restrooms], _roomDictRoomTrans[Room.EastHall], _roomDictRoomTrans[Room.ShowStage], _roomDictRoomTrans[Room.Kitchen] };
            int randomIndex = Random.Range(0, adjacentRooms.Length);
            Debug.Log(randomIndex);
            currentRoom = _roomDictTransRoom[adjacentRooms[randomIndex]];
            return adjacentRooms[randomIndex];
        }
        else if (currentRoom == Room.ShowStage)
        {
            Transform[] adjacentRooms = { _roomDictRoomTrans[Room.DiningArea] };
            currentRoom = _roomDictTransRoom[adjacentRooms[0]];
            return adjacentRooms[0];
        }
        else if (currentRoom == Room.Kitchen)
        {
            Transform[] adjacentRooms = { _roomDictRoomTrans[Room.DiningArea] };
            currentRoom = _roomDictTransRoom[adjacentRooms[0]];
            return adjacentRooms[0];
        }

        return null;
    }
}
