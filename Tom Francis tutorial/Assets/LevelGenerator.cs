using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> possibleChunkPrefabs;
    public List<GameObject> weaponPrefabs;
    public GameObject antiquePrefab;
    public GameObject guardPrefab;

    public float fractionOfPlinthsToHaveAntiques;
    public int numberOfGuardsToCreate;
    public int numberOfSpawnersToCreate;

    public string nextLevelName;
    public int alarmLevels;

    private void Awake()
    {
        References.levelGenerator = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Randomly generate chunks
        for (int i = 0; i < 3; i++)
        {
            GameObject randomChunkType = possibleChunkPrefabs[Random.Range(0, possibleChunkPrefabs.Count)];

            Vector3 spawnPosition = transform.position + new Vector3(i * 15, 0, 0);

            Instantiate(randomChunkType, spawnPosition, Quaternion.identity);

            possibleChunkPrefabs.Remove(randomChunkType);
        }

        int numberOfThingsToPlace = References.plinths.Count;
        int numberOfAntiquesToPlace = (int)(numberOfThingsToPlace * fractionOfPlinthsToHaveAntiques);

        // Assign items to plinth
        foreach (var plinth in References.plinths)
        {
            GameObject thingToCreate;
            float chanceOfAntique = numberOfAntiquesToPlace / numberOfThingsToPlace;

            if (Random.value < chanceOfAntique)
            {
                thingToCreate = antiquePrefab;
                numberOfAntiquesToPlace--;
            }
            else
            {
                thingToCreate = weaponPrefabs[Random.Range(0, weaponPrefabs.Count)];
            }

            numberOfThingsToPlace--;
            GameObject newThing = Instantiate(thingToCreate);

            plinth.AssignItem(newThing);
            //useablesOnPlinths.Remove(randomThing);
        }

        List<NavPoint> possibleSpots = new List<NavPoint>();
        float minDistanceFromPlayer = 12;

        // find points at least minDistanceFromPlayer to spawn guards
        foreach (var nav in References.navPoints)
        {
            if (Vector3.Distance(nav.transform.position, References.thePlayer.transform.position) >= minDistanceFromPlayer)
            {
                possibleSpots.Add(nav);
            }
        }

        // spawn guards
        for (int i = 0; i < numberOfGuardsToCreate; i++)
        {
            if (possibleSpots.Count == 0) break;

            int randomIndex = Random.Range(0, possibleSpots.Count);

            NavPoint spotToSpawnAt = possibleSpots[randomIndex];
            Instantiate(guardPrefab, spotToSpawnAt.transform.position, Quaternion.identity);
            possibleSpots.Remove(spotToSpawnAt);
        }

        while (References.enemySpawners.Count > numberOfSpawnersToCreate)
        {
            int randomIndex = Random.Range(0, References.enemySpawners.Count);
            Destroy(References.enemySpawners[randomIndex].gameObject);
        }

        References.alarmManager.SetUpLevel(alarmLevels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
