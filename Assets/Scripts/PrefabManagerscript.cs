using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManagerscript : MonoBehaviour {

    public static bool Created = false;

    public static List<UnityEngine.Object> PlayerProjectiles = new List<UnityEngine.Object>();
    public static List<UnityEngine.Object> ProjectileExplosions = new List<UnityEngine.Object>();
    public static List<UnityEngine.Object> PlayerDrops = new List<UnityEngine.Object>();

    public Transform PlayerProjectileFire;
    public Transform PlayerProjectileLightning;
    public Transform PlayerProjectileGas;

    public Transform ProjectileFireExplosion;
    public Transform ProjectileLightningExplosion;
    public Transform ProjectileGasExplosion;

    public Transform RedDrop;
    public Transform GreenDrop;
    public Transform BlueDrop;

    public static Transform LightningExplosionConnector;
    public Transform LightningExplosionConnectorSetter;

    // Use this for initialization
    void Start () {
        if (!Created)
        {
            Created = true;

            PlayerProjectiles.Add(PlayerProjectileFire);
            PlayerProjectiles.Add(PlayerProjectileLightning);
            PlayerProjectiles.Add(PlayerProjectileGas);

            ProjectileExplosions.Add(ProjectileFireExplosion);
            ProjectileExplosions.Add(ProjectileLightningExplosion);
            ProjectileExplosions.Add(ProjectileGasExplosion);

            PlayerDrops.Add(RedDrop);
            PlayerDrops.Add(BlueDrop);
            PlayerDrops.Add(GreenDrop);

            LightningExplosionConnector = LightningExplosionConnectorSetter;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
