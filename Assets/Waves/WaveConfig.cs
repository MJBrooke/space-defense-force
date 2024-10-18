using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
    // We expect a Prefab with multiple underlying waypoint objects
    // TODO - is there a better way to model this than a full Prefab? Just a list seems fine, but it's nice to have a GameObject to place and visualise the points...
    [SerializeField] private Transform pathPrefab;
    
    [SerializeField] private float moveSpeed = 5f;

    public float MoveSpeed => moveSpeed;

    public Transform StartPoint => pathPrefab.GetChild(0);

    public List<Transform> Waypoints() => pathPrefab.Cast<Transform>().ToList();
}