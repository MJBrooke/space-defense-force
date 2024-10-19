using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>
/// Given a WaveConfig, spawn each enemy in the wave and update each one to reference the WaveConfig
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Wave configuration to be spawned in")]
    [SerializeField] private WaveConfig waveConfig;

    private void Start() => StartCoroutine(InstantiateEnemies());

    private IEnumerator InstantiateEnemies()
    {
        // Instantiate each enemy in the wave's configuration
        foreach (var instantiatedEnemy in waveConfig.EnemiesInWave.Select(Instantiate))
        {
            // Set this spawner as the parent of the newly instantiated enemy to keep the hierarchy clean
            instantiatedEnemy.transform.SetParent(gameObject.transform);
            
            // Set the enemy to follow the configuration of this WaveConfig (speed, path etc.)
            instantiatedEnemy.GetComponent<Pathfinder>().SetupFromWaveConfig(waveConfig);
            
            // Wait the given amount of time before spawning the next enemy
            yield return new WaitForSeconds(waveConfig.TimeBetweenEnemySpawns);
        }
    }
}
