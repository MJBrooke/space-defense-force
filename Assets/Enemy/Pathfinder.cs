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
        // MoveTowardsNextWaypointFromTutorial();
        DestroyGameObject();
    }

    // Here is my own admittedly convoluted solution.
    // I think I did a few things badly:
    //   - I didn't know Vector2.MoveTowards existed, so I 'reinvented the wheel' by calculating direction and movement myself.
    //   - I tried to 'cache' the _direction variable by putting it on the class-layer to avoid creating new allocations in memory.
    //       This was an early and unnecessary optimisation, and made code less testable/readable since _direction is in the outer scope.
    //   - I overcomplicated the code by trying to keep things less indented. The tutorial version (seen below) is far more readable
    //       despite the 2x level of indentation.
    // The good thing about this function:
    //   - I learnt how to move in a direction at a constant rate without using a built-in function for it. Learning FTW.
    //   - I _think_ my float comparison of the position vs the waypoint is safer, given floating point inaccuracies.
    //       I didn't want to assume I could rely on a whole number of 0. Maybe I could have rather used Epsilon?
    private void MoveTowardsNextWaypoint()
    {
        if (_pathDone) return;

        // Move the entity one frame closer to the target location
        transform.position += (Vector3)_direction * (waveConfig.MoveSpeed * Time.deltaTime);

        // If we are not close to the next waypoint, leave our current direction as-is to keep moving there
        if (!(Vector2.Distance(transform.position, _waypoints[_nextWaypointIdx].position) < 0.01f)) return;

        // Increment target waypoint and check if the path is complete
        if (++_nextWaypointIdx == _waypoints.Count)
        {
            _pathDone = true;
            return;
        }

        // Get a Vector pointing towards the next waypoint. Normalise it to remove the distance and just retain the 'direction'.
        _direction = (_waypoints[_nextWaypointIdx].position - transform.position).normalized;
    }

    // Here is the solution from the tutorial. It's admittedly much neater than mine since I didn't know Vector2.MoveTowards existed. lol.
    private void MoveTowardsNextWaypointFromTutorial()
    {
        if (_nextWaypointIdx < _waypoints.Count)
        {
            transform.position = Vector2.MoveTowards(
                current: transform.position,
                target: _waypoints[_nextWaypointIdx].position,
                maxDistanceDelta: waveConfig.MoveSpeed * Time.deltaTime);
            
            if (transform.position == _waypoints[_nextWaypointIdx].position) _nextWaypointIdx++;
        }
        else _pathDone = true;
    }

    private void DestroyGameObject()
    {
        if (_pathDone) Destroy(gameObject);
    }
}