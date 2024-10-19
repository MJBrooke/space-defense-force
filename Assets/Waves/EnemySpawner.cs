using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Given a WaveConfig, spawn each enemy in the wave and update each one to reference the WaveConfig
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    // TODO - allow wave and enemy TimeBetweenSpawn randomness from Serialized config

    [Tooltip("Wave configurations to be spawned in")]
    [SerializeField] private List<WaveConfig> waveConfigs;

    private void Start() => StartCoroutine(SpawnWaves());

    private IEnumerator SpawnWaves()
    {
        foreach (var waveConfig in waveConfigs)
        {
            // Create all enemies in a wave
            yield return InstantiateEnemies(waveConfig);

            // Wait some time before starting the next wave
            yield return new WaitForSeconds(2);
        }
    }

    private IEnumerator InstantiateEnemies(WaveConfig waveConfig)
    {
        // Instantiate each enemy in the wave's configuration
        foreach (var instantiatedEnemy in waveConfig.EnemiesInWave.Select(Instantiate))
        {
            // Set this spawner as the parent of the newly instantiated enemy to keep the hierarchy clean
            instantiatedEnemy.transform.SetParent(gameObject.transform);

            // Set the enemy to follow the configuration of this WaveConfig (speed, path etc.)
            instantiatedEnemy.GetComponent<Pathfinder>().SetupFromWaveConfig(waveConfig);

            // Wait the given amount of time before spawning the next enemy
            yield return new WaitForSeconds(waveConfig.TimeBetweenEnemySpawns + Random.value);
        }
    }
}