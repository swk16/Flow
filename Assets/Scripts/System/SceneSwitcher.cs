using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{

    [SerializeField] PlayerInput input;
    [SerializeField] Player player;

    [SerializeField] int gameSceneIndex;
    [SerializeField] CameraFollow cameraFollow;

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    void Awake(){
        player = FindObjectOfType<Player>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            DisableAllInput();
            cameraFollow.StopFollow();
            StartCoroutine(PlayerSwitchMove());
            StartCoroutine(LoadScene(gameSceneIndex));
        }

    }

    void DisableAllInput(){
        input.DisableAllInput();
    }

    IEnumerator LoadScene(int gameSceneIndex){
        yield return new WaitForSeconds(1.5f);
        SceneLoader.Instance.LoadScene(gameSceneIndex);
    }

    IEnumerator PlayerSwitchMove(){
        //lerp move player to player.transform.position.x + 18
        float t = 0f;
        float time = 3f;
        Vector3 startPos = player.transform.position;
        Vector3 endPos = new Vector3(player.transform.position.x + 12, player.transform.position.y, player.transform.position.z);
        while (t < time) {
            t += Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPos, endPos, t / time);
            yield return waitForFixedUpdate;
        }
    }
}
