using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string scenename;
    // Start is called before the first frame update
    public void StartScene() {
        SceneManager.LoadScene(scenename);
    }
}
