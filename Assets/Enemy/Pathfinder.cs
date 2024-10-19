using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Given a WaveConfiguration, iteratively move the position of the owning GameObject towards the next waypoint until the end of the path.
/// Once a path is completed, the owning GameObject is destroyed.
/// </summary>
public class Pathfinder : MonoBehaviour
{
    [SerializeField] private WaveConfig waveConfig;
    private List<Transform> _waypoints;
    private int _nextWaypointIdx;

    private Vector2 _direction = Vector2.zero;
    private bool _pathDone;

    private void Start()
    {
        _waypoints = waveConfig.Waypoints();
        // We need at least a starting and ending waypoint to find any path
        if (_waypoints.Count < 2)
        {
            Debug.LogError("Waypoints count is less than 2");
            _pathDone = true;
            return;
        }

        // Spawn at the first waypoint
        transform.position = _waypoints[_nextWaypointIdx].position;
    }

    private void Update()
    {
        MoveTowardsNextWaypoint();
        DeleteIfPathDone();
    }

    private void MoveTowardsNextWaypoint()
    {
        if (_pathDone) return;

        // Move the entity one frame closer to the target location
        transform.position += (Vector3)_direction * (waveConfig.MoveSpeed * Time.deltaTime);

        // If we are not close to the next waypoint, leave our current direction as-is to keep moving there
        if (!(Vector2.Distance(transform.position, _waypoints[_nextWaypointIdx].position) < 0.1f)) return;

        // Increment target waypoint and check if the path is complete
        if (++_nextWaypointIdx == _waypoints.Count)
        {
            _pathDone = true;
            return;
        }

        // Get a Vector pointing towards the next waypoint. Normalise it to remove the distance and just retain the 'direction'.
        _direction = (_waypoints[_nextWaypointIdx].position - transform.position).normalized;
    }

    private void DeleteIfPathDone()
    {
        if (_pathDone) Destroy(gameObject);
    }
}