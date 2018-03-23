using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParentScript : MonoBehaviour {

    public static EnemyParentScript control;

    public Transform enemy;

    private float initial_spawn_time = 1.0f;
    private float spawn_time_reduction = 0.01f;
    private int spawn_count = 0;
    private float spawn_timer = 1.0f;

    private List<Vector3> spawn_points = new List<Vector3>();

	// Use this for initialization
	void Start () {

        if (control == null)
        {
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        spawn_points.Add(new Vector3(7, 7,0));
        spawn_points.Add(new Vector3(-7, 7,0));
        spawn_points.Add(new Vector3(7, -7,0));
        spawn_points.Add(new Vector3(-7, -7,0));
    }
	
	// Update is called once per frame
	void Update () {
        //SpawnEnemy();
	}

    void FixedUpdate()
    {
        SpawnEnemy();
    }

    public void Reset()
    {
        spawn_count = 0;
        spawn_timer = 1.0f;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnEnemy()
    {
        spawn_timer -= 0.01f;
        if (spawn_timer < 0)
        {
            spawn_count += 1;
            spawn_timer = initial_spawn_time - (spawn_count * spawn_time_reduction);
            //spawn_timer = initial_spawn_time;
            if (spawn_timer <= 0)
            {
                spawn_timer = 0.01f;
            }
            Transform enemy_created = (Transform)Instantiate(enemy, spawn_points[Random.Range(0,spawn_points.Count)], Quaternion.identity);
            enemy_created.SetParent(this.transform);
        }
    }

    public List<Transform> GetAllEnemies()
    {
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < transform.childCount; ++i)
            children.Add(transform.GetChild(i));
        return children;
    }
}
