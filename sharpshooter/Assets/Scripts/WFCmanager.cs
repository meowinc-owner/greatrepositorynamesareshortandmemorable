using UnityEngine;
using System.Collections.Generic;

public class WFCmanager : MonoBehaviour
{
    
    public float cellSize; // size of cell in worldspace units
    public int gridSize; // how many rooms on 1 axis
    public List<RoomData> totalRooms = new List<RoomData>(); // all possible rooms in the game
    private Cell[,] _grid; // grid for cells

    public void GenerateMap() // for generating map: what rooms go where and spawming
    {
        _grid = new Cell[gridSize, gridSize]; // creates grid and lays out spaces for all cells
        for (int x = 0; x < gridSize; x++) // goes over grid size for x value
        {
            for (int y = 0; y < gridSize; y++) // goes over grid size for y value
            {
                _grid[x,y] = new Cell(new Vector2Int(x,y), totalRooms); // creates new cell @ x and y
                
            }
        }
        
        
        // filling out rooms 

        for (int i = 0; i < gridSize*gridSize; i++) // gridSize*gridSize means number of rooms you have in the map
        {
            int Entropy = int.MaxValue; // tracker to track lowest number of valid rooms in possible rooms you found so far
            int lowestEntropyX = -1; // x value of the room for the entropy you found
            int lowestEntropyY = -1; // same thing from above but for y
            
            // for loop below figures out what cell filling next
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (_grid[x, y].collapsed) // if room is already chosen, skip it
                    {
                        continue; // skip the fishes
                    }

                    int gridEntropy = _grid[x, y].possibleRooms.Count; // possibleRooms.Count = number of possible rooms that could be possible for this cell ----- _grid[x, y] = the specific cell @ x and y
                    if (gridEntropy < Entropy) // if current room has less possibility compared to other rooms
                    {
                        Entropy = gridEntropy; // override entropy with the new lower gridentropy
                        lowestEntropyX = x; // save x and y of the new lowest entropy cell/room
                        lowestEntropyY = y; // save x and y of the new lowest entropy cell/room
                        
                    }
                }
            }

            List<RoomData> Rooms = _grid[lowestEntropyX, lowestEntropyY].possibleRooms; // grabbing a copy of the possible rooms for the room we're filling 👍
            int roomIndex = Random.Range(0, Rooms.Count); // random number between 0 and number of possible rooms
            _grid[lowestEntropyX, lowestEntropyY].Collapse(Rooms[roomIndex]); // pick room at that number
            
            // yahoo done all comments 🥳🥳🥳🥳
            UpdateNeighbours(new Vector2Int(lowestEntropyX, lowestEntropyY));
        }
    }

    private void UpdateNeighbours(Vector2Int pos)
    {
        Vector2Int[] directions =
        {
            new Vector2Int(pos.x, pos.y + 1),
            new Vector2Int(pos.x, pos.y - 1),
            new Vector2Int(pos.x - 1, pos.y),
            new Vector2Int(pos.x + 1, pos.y)
        };

        for (int i = 0; i < directions.Length; i++) // loop over all directions
        {
            if (directions[i].x < 0 || directions[i].y < 0 || directions[i].x >= gridSize || directions[i].y >= gridSize) // checking if passed edge of grid
            {
                continue;
            }
            Cell neighbour = _grid[directions[i].x, directions[i].y];
            if (neighbour.collapsed)
            {
                continue;
            }
            List<RoomData> validRooms = new List<RoomData>();

            foreach(RoomData room in _grid[pos.x, pos.y].possibleRooms) // go over all possible rooms for cell we're on
            {
                switch (i)
                {
                    case 0:
                        validRooms.AddRange(room.validUpNeighbours);
                        break;
                    case 1:
                        validRooms.AddRange(room.validDownNeighbours);
                        break;
                    case 2:
                        validRooms.AddRange(room.validLeftNeighbours);
                        break;
                    case 3:
                        validRooms.AddRange(room.validRightNeighbours);
                        break;
                }
            }
            
            

            if (_grid[directions[i].x, directions[i].y].RestrictOptions(validRooms))
            {
                UpdateNeighbours(directions[i]);
            }
        }
    }
}
