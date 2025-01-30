using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int poolSize = 10;
    private List<GameObject> _projectilePool;

    private void Start()
    {
        _projectilePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            _projectilePool.Add(projectile);
        }
    }

    public GameObject GetProjectile()
    {
        foreach (GameObject projectile in _projectilePool)
        {
            if (!projectile.activeInHierarchy)
            {
                projectile.SetActive(true);
                return projectile;
            }
        }

        GameObject newProjectile = Instantiate(projectilePrefab);
        _projectilePool.Add(newProjectile);
        return newProjectile;
    }
}
