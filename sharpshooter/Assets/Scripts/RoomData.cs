using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "RoomData", menuName = "Scriptable Objects/RoomData")]
public class RoomData : ScriptableObject
{
    public GameObject prefab;
    public List<RoomData> validLeftNeighbours = new List<RoomData>();
    public List<RoomData> validRightNeighbours = new List<RoomData>();
    public List<RoomData> validUpNeighbours = new List<RoomData>();
    public List<RoomData> validDownNeighbours = new List<RoomData>();
}
