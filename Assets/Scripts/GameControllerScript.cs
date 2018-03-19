using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {

    public static GameControllerScript control;

    private List<float> player_position = new List<float>();

    public Text health_text;
    public Text fire_count_text;
    public Text lightning_count_text;
    public Text gas_count_text;
    public Text score_text;
    public Text final_score_text;

    private float score = 0f;

    public AudioSource audioSource;

    public Transform player;

    public Camera main_camera;

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

        DontDestroyOnLoad(this.gameObject);

        final_score_text.enabled = false;

        player_position.Add(0);
        player_position.Add(0);

        audioSource = GetComponent<AudioSource>();

        Transform player_created = (Transform)Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
        player_created.SetParent(this.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {
        final_score_text.text = "Score: " + score;
        final_score_text.enabled = true;

        StartCoroutine(RestartGameTimer());
        
    }

    IEnumerator RestartGameTimer()
    {
        
        yield return new WaitForSeconds(5);

        EnemyParentScript.control.Reset();
        DropManager.control.Reset();
        score = 0;

        final_score_text.enabled = false;

        SetFireCountText(0);
        SetGasCountText(0);
        SetLightningCountText(0);
        score_text.text = "Score: " + score;
        SetHealthText(1);

        Transform player_created = (Transform)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        player_created.SetParent(this.transform);

    }

    public void SetPlayerPosition(float x, float y)
    {
        player_position[0] = x;
        player_position[1] = y;
    }

    public List<float> GetPlayerPosition()
    {
        return player_position;
    }

    public void SetHealthText(float health)
    {
        health_text.text = "Health: " + health;
    }

    public void SetScoreText(float additional_score)
    {
        score += additional_score;
        score_text.text = "Score: " + score;
    }

    public void SetFireCountText(float fire_count)
    {
        fire_count_text.text = "Fire Count: " + fire_count;
    }

    public void SetLightningCountText(float lightning_count)
    {
        lightning_count_text.text = "Lightning Count: " + lightning_count;
    }

    public void SetGasCountText(float gas_count)
    {
        gas_count_text.text = "Gas Count: " + gas_count;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    public Camera GetMaincamera()
    {
        return main_camera;
    }
}
