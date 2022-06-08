using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colider : MonoBehaviour
{
    //[SerializeField] float damage;
    [SerializeField] AudioClip takeDamageAudio;
    [SerializeField] float takeDamageSFXVolume = 1f;


    
    private void OnCollidertEnter2D(Collider2D other)
    {
        Debug.Log("Colid not player");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log ("collision");

            AudioManager.Instance.PlaySFX(takeDamageAudio, takeDamageSFXVolume);
        }
    }

   
}
