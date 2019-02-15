using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static int health = 100;
    public static int stamina = 100;
    public Text scoretext;
    public int score = 0;
    public Slider HealthBar;
    public Slider StaminaBar;

    float damagetime = 0;

	// Use this for initialization
	void Start () {
        UpdateText();
  
    }


    void ReduceHealth(float amount)
    {
        // take damage
        health = health - 20;
        HealthBar.value = health;
        // is the player dead?
        if (health <= 0)
            GameManager.Instance.SetGameOver();
    }

    // Player take damage
    public void TakeDamage(float amount)
    {
        //// take damage
        //health = health - amount;
        if (health < 0) health = 0;

        //// is the player dead?
        if (health <= 0)
            GameManager.Instance.SetGameOver();           
    }

    // enter trigger zone of something
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain") return;
        Debug.Log("player trigger " + other.tag+ "name "+other.name);
        if (other.tag == "Fire")
        {
            damagetime = Time.time;
            TakeDamage(10);
        }

        // collectable?
        if (other.tag == "book")
        {
            Destroy(other.gameObject);
            //score = score + 1;
            score ++;
            UpdateText();
        }

    }

    // collided with something?
    void OnTriggerStay(Collider other)
    {
        if (other.tag=="Fire")
        {
            if (Time.time-damagetime>1)
            {
                TakeDamage(10);
                damagetime = Time.time;
            }
        }
    }


    void UpdateText()
    {
        scoretext.text = " Score " + score;
    }
}
