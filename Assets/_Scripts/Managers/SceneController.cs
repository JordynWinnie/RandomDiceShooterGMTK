using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private const int MAIN_SCENE = 0;
        [SerializeField] private const int GAME_SCENE = 1;

        public int MainSceneID => MAIN_SCENE;
        public int GameSceneID => GAME_SCENE;

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

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