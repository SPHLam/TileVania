using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinAudioClip;
    bool wasCollected = false; // prevent double collect 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollected)
        {
            FindObjectOfType<GameSession>().AddPoint();
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinAudioClip, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
