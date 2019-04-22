using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour {

    public GameObject spellProjectile;
    public GameObject spawnLocation;
    float fireRate;
    float nextShot;
    float damage;
    float speed;
    Vector3 spawnPosition;
    Quaternion spawnRotation;

    private float projectileSpeedScale = 0.025f;

    // Use this for initialization
    void Awake ()
    {

    }

    private void Update()
    {
        fireRate = Player.instance.playerFireRate;
        damage = Player.instance.playerDamage;
        speed = Player.instance.playerProjectileSpeed;
    }

    void LateUpdate ()
    {
        
    }

    public void Fire()
    {
        if ((Input.GetButton("AimUp") && Time.time > nextShot) || (Input.GetButton("AimDown") && Time.time > nextShot) || (Input.GetButton("AimLeft") && Time.time > nextShot) || (Input.GetButton("AimRight") && Time.time > nextShot))
        {
            nextShot = Time.time + (1f / fireRate);
            Vector3 spawnPosition = spawnLocation.transform.position;
            Quaternion spawnRotation = spawnLocation.transform.rotation;
            GameObject clone = GameObject.Find("PlayerBulletPool").GetComponent<ObjectPooler>().GetPooledObject();
            clone.transform.position = spawnPosition;
            clone.transform.rotation = spawnRotation;
            Vector2 projectileSize = new Vector2(1 * (damage / 4), 1 * (damage / 4));
            clone.transform.localScale = projectileSize;
            clone.SetActive(true);
            clone.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectileSpeedScale * speed, 0));
        }
    }

}
