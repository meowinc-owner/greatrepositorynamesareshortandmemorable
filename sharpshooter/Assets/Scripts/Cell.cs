using UnityEngine;
using System.Collections.Generic;


public class Cell
{
    public bool collapsed = false;
    public RoomData collapsedRoomData;
    public List<RoomData> totalRooms;
    public Vector2Int gridPos;
    

    public Cell(Vector2Int gridPos, List <RoomData> totalRooms)
    {
        this.gridPos = gridPos;
        this.totalRooms = new List<RoomData>(totalRooms);
        collapsedRoomData = null;
        
        
    }

    public void Collapse(RoomData selectedRoom)
    {
        collapsed = true;
        collapsedRoomData = selectedRoom;
        totalRooms =  new List<RoomData>{selectedRoom};
        
    }

    public bool RestrictOptions(List <RoomData> ValidRooms)
    {
        if (collapsed)
        {
            return false;
        }
        int originalCount = totalRooms.Count;
        totalRooms.RemoveAll(room => !ValidRooms.Contains(room));
        return totalRooms.Count != originalCount;
    }
}
