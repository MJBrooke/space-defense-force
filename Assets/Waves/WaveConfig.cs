using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Contains the configuration for a given wave of enemies, including the path they will traverse and the speed that they will move.
/// </summary>
[CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
    // We expect a Prefab with multiple underlying waypoint objects
    [Tooltip("The set of waypoints that will be followed by all enemies in this wave.")]
    [SerializeField] private Transform pathPrefab;
    
    [Tooltip("The speed that each enemy will move through the waypoints.")]
    [SerializeField] private float moveSpeed = 5f;

    public float MoveSpeed => moveSpeed;

    // Creates a list of the set of waypoint transforms from the prefab object so that a GameObject can reference them easily
    public List<Transform> Waypoints() => pathPrefab.Cast<Transform>().ToList();
    
    // Prints a friendly message if the config is missing the set of waypoints represented by the Path GameObject
    private void OnValidate() => Assert.IsNotNull(pathPrefab, "The path prefab cannot be null.");
}