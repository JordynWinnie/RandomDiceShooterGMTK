using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LootLocker.Requests;
using TMPro;

namespace Networking.LookLocker
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerNamesTMP;
        [SerializeField] private TextMeshProUGUI scoresTMP;
        [SerializeField] private TextMeshProUGUI loadingTMP;
        [SerializeField] private TMP_InputField usernameTMP;
        
        private int leaderboardID = 4774;
        public static LeaderboardManager instance;
        public bool loadUIless = true;

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

        public void SubmitScore(int score) => StartCoroutine(SubmitScoreRoutine(score));
        public void FetchTopScores() => StartCoroutine(FetchTopScoresRoutine());

        private IEnumerator FetchTopScoresRoutine()
        {
            if (!PlayerManager.isLoggedIn)
            {
                yield return null;
            }

            bool done = false;
            LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, (res) => 
            {
                if (res.success)
                {
                    string tmpNames = "Names\n";
                    string tmpScores = "Scores\n";

                    LootLockerLeaderboardMember[] members = res.items;

                    for (int i = 0; i < members.Length; i++)
                    {
                        tmpNames += (i + 1).ToString() + ". ";
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
            if (!PlayerManager.isLoggedIn)
            {
                yield return null;
            }


            bool done = false;
            string playerID = PlayerPrefs.GetString("PlayerID");
            LootLockerSDKManager.SubmitScore(playerID, score, leaderboardID, (res) =>
            {
                if (res.success)
                {
                    Debug.Log("Score Submitted");
                } else
                {
                    Debug.Log("Unable to submit score");
                }
            });

            yield return new WaitWhile(() => done == false);
        }

        public void SetUsername(string username)
        {
            if (!PlayerManager.isLoggedIn)
            {
                return;
            }


            LootLockerSDKManager.SetPlayerName(username, (res) =>
            {
                if (res.success)
                {
                    Debug.Log("Updated username");
                }
            });
        }
    }
}