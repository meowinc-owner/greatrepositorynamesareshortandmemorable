using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "RoomData", menuName = "Scriptable Objects/RoomData")]
public class RoomData : ScriptableObject
{
    public GameObject prefab;
    public List<GameObject> validLeftNeighbours = new List<GameObject>();
    public List<GameObject> validRightNeighbours = new List<GameObject>();
    public List<GameObject> validUpNeighbours = new List<GameObject>();
    public List<GameObject> validDownNeighbours = new List<GameObject>();
}
