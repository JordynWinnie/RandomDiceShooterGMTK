using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Networking.LookLocker;

namespace Player
{
    public class ScoreUploadTest : MonoBehaviour
    {
        [SerializeField] private int score = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                score += UnityEngine.Random.Range(0, 10);
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