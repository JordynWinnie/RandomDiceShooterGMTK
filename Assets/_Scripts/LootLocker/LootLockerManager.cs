using System.Collections;
using LootLocker.Requests;
using UnityEngine;
using UnityEngine.Events;

namespace Networking.LookLocker
{
    public class LootLockerManager : MonoBehaviour
    {
        [SerializeField] private bool LogMessages;

        public UnityEvent OnConnected;
        public UnityEvent OnFailedToConnect;

        private void Start()
        {
            StartCoroutine(LogInRoutine());
        }

        private IEnumerator LogInRoutine()
        {
            var done = false;
            LootLockerSDKManager.StartGuestSession(res =>
            {
                if (res.success)
                {
                    Log("Successfully connected to LootLocker");
                    PlayerPrefs.SetString("PlayerID", res.player_id.ToString());
                    done = true;
                    PlayerManager.isLoggedIn = true;
                    OnConnected?.Invoke();
                }
                else
                {
                    Log("Unable to connect to LootLocker");
                    done = true;

                    OnFailedToConnect?.Invoke();
                }
            });

            yield return new WaitWhile(() => done == false);
        }

        private void Log(string message)
        {
            if (LogMessages)
                Debug.Log(message);
        }
    }
}