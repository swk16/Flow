using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{

    [SerializeField] PlayerInput input;

    [SerializeField] int gameSceneIndex;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            DisableAllInput();
            SceneLoader.Instance.LoadScene(gameSceneIndex);}
    }

    void DisableAllInput(){
        input.DisableAllInput();
    }
}
