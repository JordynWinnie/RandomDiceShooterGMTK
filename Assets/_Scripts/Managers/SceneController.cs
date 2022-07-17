using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneController : MonoBehaviour
    {
        public const int MAIN_SCENE = 0;
        public const int GAME_SCENE = 1;

        public void ChangeScene(int sceneIndex)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    } 
}