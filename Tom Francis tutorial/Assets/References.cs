using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{
    public static LevelManager levelManager;
    public static PlayerBehaviour thePlayer;
    public static CanvasBehaviour theCanvas;
    public static List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    public static List<EnemyBehaviour> allEnemies = new List<EnemyBehaviour>();
    public static List<Useable> useables = new List<Useable>();

    public static float maxDistanceInALevel = 1000;

    public static LayerMask wallsLayer = LayerMask.GetMask("Walls");
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");

    public static Screenshake screenshake;

    public static List<NavPoint> navPoints = new List<NavPoint>();
}
