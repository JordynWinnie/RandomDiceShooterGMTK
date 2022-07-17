using System.Collections;
using LootLocker.Requests;
using TMPro;
using UnityEngine;

namespace Networking.LookLocker
{
    public class LeaderboardManager : MonoBehaviour
    {
        public static LeaderboardManager instance;
        [SerializeField] private TextMeshProUGUI playerNamesTMP;
        [SerializeField] private TextMeshProUGUI scoresTMP;
        [SerializeField] private TextMeshProUGUI loadingTMP;
        [SerializeField] private TMP_InputField usernameTMP;
        public bool loadUIless = true;

        private readonly int leaderboardID = 4774;

        private void Awake()
        {
            instance = this;

            if (!loadUIless)
            {
                loadingTMP.text = "Exposing your IP...";

                loadingTMP.enabled = true;
                playerNamesTMP.enabled = false;
                scoresTMP.enabled = false;

                usernameTMP.onEndEdit.AddListener(SetUsername);
            }
        }

        public void SubmitScore(int score)
        {
            StartCoroutine(SubmitScoreRoutine(score));
        }

        public void FetchTopScores()
        {
            StartCoroutine(FetchTopScoresRoutine());
        }

        private IEnumerator FetchTopScoresRoutine()
        {
            if (!PlayerManager.isLoggedIn) yield return null;

            var done = false;
            LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, res =>
            {
                if (res.success)
                {
                    var tmpNames = "Names\n";
                    var tmpScores = "Scores\n";

                    var members = res.items;

                    for (var i = 0; i < members.Length; i++)
                    {
                        tmpNames += i + 1 + ". ";
                        tmpNames += members[i].player.name == "" ? members[i].player.id : members[i].player.name;
                        tmpScores += members[i].score + "\n";
                        tmpNames += "\n";
                    }

                    playerNamesTMP.text = tmpNames;
                    scoresTMP.text = tmpScores;

                    loadingTMP.enabled = false;
                    playerNamesTMP.enabled = true;
                    scoresTMP.enabled = true;
                    done = true;
                }
            });

            yield return new WaitWhile(() => done == false);
        }

        private IEnumerator SubmitScoreRoutine(int score)
        {
            if (!PlayerManager.isLoggedIn) yield return null;


            var done = false;
            var playerID = PlayerPrefs.GetString("PlayerID");
            LootLockerSDKManager.SubmitScore(playerID, score, leaderboardID, res =>
            {
                if (res.success)
                    Debug.Log("Score Submitted");
                else
                    Debug.Log("Unable to submit score");
            });

            yield return new WaitWhile(() => done == false);
        }

        public void SetUsername(string username)
        {
            if (!PlayerManager.isLoggedIn) return;


            LootLockerSDKManager.SetPlayerName(username, res =>
            {
                if (res.success) Debug.Log("Updated username");
            });
        }
    }
}