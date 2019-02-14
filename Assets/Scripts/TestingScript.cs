using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class TestingScript : MonoBehaviour
{

    Transform target;

    private void Awake()
    {
        
    }

    private void Start()
    {
        target = Player.instance.transform;
    }

    private void Update()
    {
        Vector2 targetDir = target.position - transform.position;
        Debug.Log(targetDir);
        Vector2.Perpendicular(targetDir);
        Debug.Log(Vector2.Perpendicular(targetDir));
        Debug.DrawLine(target.position, transform.position, Color.blue);
        Debug.DrawRay(target.position, Vector2.Perpendicular(targetDir), Color.red);
        Debug.DrawRay(target.position, -Vector2.Perpendicular(targetDir), Color.red);
        Debug.DrawLine(target.position, Vector2.Perpendicular(targetDir), Color.black);
        Debug.DrawLine(Vector2.Perpendicular(targetDir), -Vector2.Perpendicular(targetDir), Color.green);
        Ray2D ray = new Ray2D(target.position, Vector2.Perpendicular(targetDir));
        Vector2 maxHit = ray.GetPoint(1f);
        Vector2 minHit = ray.GetPoint(-1f);
        Debug.Log(maxHit);
        Debug.Log(minHit);
        UnityEngine.Random.Range(maxHit.x, maxHit.y);
        Ray2D attackRange = new Ray2D(minHit, maxHit);
        Vector2 attackArc = maxHit - minHit;
        Vector2 attackPoint = minHit + UnityEngine.Random.value * attackArc;
        Debug.DrawLine(target.position, minHit, Color.yellow);
        Debug.DrawLine(target.position, maxHit, Color.yellow);
        Debug.DrawLine(target.position, attackPoint, Color.magenta);
    }
}
