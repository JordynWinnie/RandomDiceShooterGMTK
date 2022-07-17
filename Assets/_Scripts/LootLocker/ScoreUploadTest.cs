using Networking.LookLocker;
using UnityEngine;

namespace Player
{
    public class ScoreUploadTest : MonoBehaviour
    {
        [SerializeField] private int score;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                score += Random.Range(0, 10);
                Debug.Log("Score: " + score);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                LeaderboardManager.instance.SubmitScore(score);
                Debug.Log("Submitted Score");
            }
        }
    }
}