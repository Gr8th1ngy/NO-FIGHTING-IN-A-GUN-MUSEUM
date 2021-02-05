using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunBeam : BulletBehaviour
{
    public LineRenderer myBeam;

    // Start is called before the first frame update
    void Start()
    {
        // Fire a laser to see how far until we hit a wall
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, References.maxDistanceInALevel, References.wallsLayer);
        float distanceToWall = hitInfo.distance;

        // Fire laser to check for enemies
        float beamThickness = 0.3f;
        RaycastHit[] hitInfos = Physics.SphereCastAll(transform.position, beamThickness, transform.forward, hitInfo.distance, References.enemiesLayer);

        foreach (var enemyHitInfo in hitInfos)
        {
            enemyHitInfo.collider.GetComponentInParent<HealthSystem>()?.TakeDamage(damage);
        }

        // Show beam
        myBeam.SetPosition(0, transform.position);
        myBeam.SetPosition(1, hitInfo.point);
    }

    protected override void Update()
    {
        // Handle destroying the beam
        base.Update();

        // Beam fades over time
        myBeam.endColor = Color.Lerp(myBeam.endColor, Color.clear, 0.05f);
    }
}
