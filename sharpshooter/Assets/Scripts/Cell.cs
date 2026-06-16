using UnityEngine;
using System.Collections.Generic;


public class Cell
{
    public bool collapsed = false; // room has been chosen
    public RoomData collapsedRoomData; // data for room that was chosen
    public List<RoomData> possibleRooms; // rooms that you can choose from for a new cell
    public Vector2Int gridPos; // x and y for the cell on the grid
    

    public Cell(Vector2Int gridPos, List <RoomData> totalRooms) // constructs a new grid cell & sets vars
    {
        this.gridPos = gridPos;
        this.possibleRooms = new List<RoomData>(totalRooms);
        collapsedRoomData = null;
        
        
    }

    public void Collapse(RoomData selectedRoom) // choose a room
    {
        collapsed = true;
        collapsedRoomData = selectedRoom;
        possibleRooms =  new List<RoomData>{selectedRoom};
        
    }

    public bool RestrictOptions(List <RoomData> ValidRooms) // changes the number of possible rooms when different cell collapses, based on valid neighbours
    {
        if (collapsed)
        {
            return false;
        }
        int originalCount = possibleRooms.Count;
        possibleRooms.RemoveAll(room => !ValidRooms.Contains(room)); // from possible rms, remove any room not in the valid rooms list
        return possibleRooms.Count != originalCount;
    }
}
