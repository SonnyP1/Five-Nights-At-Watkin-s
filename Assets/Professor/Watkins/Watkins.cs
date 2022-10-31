using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watkins : Professor
{
    Room[] _rooms = { Room.WestHall, Room.EastHall };
    List<Door_Button> _doors = new List<Door_Button>();
    IDictionary<Room, Door_Button> _roomDictRoomDoor = new Dictionary<Room, Door_Button>();
    Room currentRoom;
    public override void Start()
    {
        base.Start();
        _doors.Add(_gameStats.GetDoorLeft());
        _doors.Add(_gameStats.GetDoorRight());
        currentRoom = Room.ShowStage;

        for (int i = 0; i < _rooms.Length; i++)
        {
            _roomDictRoomDoor.Add(_rooms[i], _doors[i]);
        }
    }


    public override void Move()
    {
        if(currentRoom == Room.ShowStage)
        {
            if(Random.value < 0.5f)
            {
                _navMeshAgent.SetDestination(_targets[0].position);
                currentRoom = Room.EastHall;
            }
            else
            {
                _navMeshAgent.SetDestination(_targets[1].position);
                currentRoom = Room.WestHall;
            }
        }
        else
        {
            if(!_roomDictRoomDoor[currentRoom].GetIsDoorActive())
            {
                _navMeshAgent.SetDestination(_targets[_targets.Length - 1].position);
                JumpScare();
                return;
            }
            else
            {
                currentRoom = Room.ShowStage;
                FailMove();
            }
        }
    }
}
