using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

    public static DropManager control;

    private float initial_spawn_time = 0.9f;
    private float spawn_time_reduction = 0.01f;
    private int spawn_count = 0;
    private float spawn_timer = 1.0f;

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
    }
	
	// Update is called once per frame
	void Update () {
        SpawnDrop();
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

    private void SpawnDrop()
    {
        spawn_timer -= 0.01f;
        if (spawn_timer < 0)
        {
            spawn_count += 1;
            spawn_timer = initial_spawn_time - (spawn_count * spawn_time_reduction);
            if (spawn_timer <= 0)
            {
                spawn_timer = 0.01f;
            }
            Transform drop_created = (Transform)Instantiate(PrefabManagerscript.PlayerDrops[Random.Range(0, PrefabManagerscript.PlayerDrops.Count)], new Vector3(Random.Range(-7,8), Random.Range(-7, 8), 0), Quaternion.identity);
            drop_created.SetParent(this.transform);
        }
    }
}
