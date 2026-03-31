using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int initialPoolSize = 3;
    [SerializeField] private float launchForce = 5f;
    [SerializeField] private float projectileLifetime = 2f; // ~2 secondes comme demandé
    [SerializeField] private float spawnOffset = 0.25f; // léger offset devant l'ennemi

    private readonly List<GameObject> pool = new List<GameObject>();
    public bool isCharaInside;
    private Transform charaTransform;

    private void Start()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("EggAI: projectilePrefab non défini dans l'inspecteur.");
            return;
        }

        for (int i = 0; i < Mathf.Max(1, initialPoolSize); i++)
            pool.Add(CreatePooledProjectile());
    }

    private GameObject CreatePooledProjectile()
    {
        var go = Instantiate(projectilePrefab);
        if (go.GetComponent<Projectile>() == null) go.AddComponent<Projectile>();
        go.SetActive(false);
        return go;
    }

    private GameObject GetFromPool()
    {
        foreach (var g in pool)
            if (!g.activeInHierarchy)
                return g;

        var newGo = CreatePooledProjectile();
        pool.Add(newGo);
        return newGo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "_Chara") return;
        isCharaInside = true;
        charaTransform = other.transform;
        TrySpawn();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name != "_Chara") return;
        isCharaInside = false;
        charaTransform = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "_Chara") return;
        isCharaInside = true;
        charaTransform = collision.transform;
        TrySpawn();
    }

    // Une fois dans le cercle, on tire vers la position du joueur (direction calculée vers _Chara).
    private void TrySpawn()
    {
        if (charaTransform == null || projectilePrefab == null) return;

        var dir = (charaTransform.position - transform.position);
        var dir2D = new Vector2(dir.x, dir.y).normalized;

        SpawnAndLaunch(dir2D);
    }

    private void SpawnAndLaunch(Vector2 direction)
    {
        var proj = GetFromPool();

        // Positionner légèrement devant l'ennemi pour éviter collision/imbrication initiale
        var spawnPos = (Vector2)transform.position + direction * spawnOffset;
        proj.transform.position = spawnPos;

        // Orienter le projectile selon la direction de tir (rotation Z)
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        proj.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        proj.SetActive(true);

        var rb = proj.GetComponent<Rigidbody2D>() ?? proj.AddComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Appliquer une impulsion vers la direction du joueur
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);

        var pscript = proj.GetComponent<Projectile>();
        pscript.ResetStatus();

        StartCoroutine(ReturnToPoolAfter(proj, projectileLifetime));
    }

    private IEnumerator ReturnToPoolAfter(GameObject proj, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (proj == null) yield break;

        // Si le projectile n'a pas touché (_HasHit == false), on le remet en pool.
        // Si HasHit == true, il a déjà été désactivé par Projectile.Deactivate().
        var p = proj.GetComponent<Projectile>();
        if (p == null || !p.HasHit)
            ReturnToPool(proj);

        if (isCharaInside)
            TrySpawn();
    }

    public void ReturnToPool(GameObject proj)
    {
        if (proj == null) return;
        var rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        proj.SetActive(false);
    }
}

