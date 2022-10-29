using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Room
{
    Backstage,
    Restrooms,
    ShowStage,
    SupplyCloset,
    WestHall,
    EastHall,
    Kitchen,
    DiningArea,
    Office
};
public class Sidler : Professor
{
    [Header("SidlerLoc")]
    [SerializeField] Transform[] _rooms;
    [SerializeField] Room[] _roomName;
    private IDictionary<Room, Transform> _roomDictRoomTrans = new Dictionary<Room, Transform>();
    private IDictionary<Transform, Room> _roomDictTransRoom = new Dictionary<Transform, Room>();
    private Room currentRoom;
    public override void Start()
    {
        base.Start();
        for (int i = 0; i < _rooms.Length; i++)
        {
            _roomDictRoomTrans.Add(_roomName[i],_rooms[i]);
        }

        for (int i = 0; i < _rooms.Length; i++)
        {
            _roomDictTransRoom.Add(_rooms[i], _roomName[i]);
        }

        currentRoom = Room.SupplyCloset;
    }
    public override void Move()
    {
        if (currentRoom == Room.WestHall)
        {
            if (!_door.GetIsDoorActive())
            {
                currentRoom = Room.Office;
                _navMeshAgent.SetDestination(_roomDictRoomTrans[currentRoom].position);
                JumpScare();
                return;
            }
            else
            {
                FailMove();
                return;
            }
        }


        Transform target = ChooseAdjacentRoom();
        Debug.Log(currentRoom);
        _navMeshAgent.SetDestination(target.position);
    }

    public override void FailMove()
    {
        currentRoom = Room.DiningArea;
        _navMeshAgent.SetDestination(_roomDictRoomTrans[currentRoom].position);
    }

    public Transform ChooseAdjacentRoom()
    {
        if (currentRoom == Room.SupplyCloset)
        {
            Transform[] adjacentRooms = { _roomDictRoomTrans[Room.DiningArea] };
            currentRoom = _roomDictTransRoom[adjacentRooms[0]];
            return adjacentRooms[0];
        }
        else if(currentRoom == Room.DiningArea)
        {
            Transform[] adjacentRooms = { _roomDictRoomTrans[Room.SupplyCloset], _roomDictRoomTrans[Room.WestHall], _roomDictRoomTrans[Room.ShowStage] };
            int randomIndex = Random.Range(0, adjacentRooms.Length);
            Debug.Log(randomIndex);
            currentRoom = _roomDictTransRoom[adjacentRooms[randomIndex]];
            return adjacentRooms[randomIndex];
        }
        else if(currentRoom == Room.ShowStage)
        {
            Transform[] adjacentRooms = { _roomDictRoomTrans[Room.DiningArea] };
            currentRoom = _roomDictTransRoom[adjacentRooms[0]];
            return adjacentRooms[0];
        }


        return null;
    }
}
