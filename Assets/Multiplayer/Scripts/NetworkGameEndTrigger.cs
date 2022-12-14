using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NetworkGameEndTrigger : NetworkBehaviour
{
    [SerializeField] private GameEndScreen screen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NetworkPlayerController networkPlayerController = other.GetComponent<NetworkPlayerController>();

            if (networkPlayerController != null && networkPlayerController.IsOwner)
            {
                screen.Open(true);
                switch (GameDifficultyManager.Instance.Difficulty)
                {
                    case GameEnums.GameDifficulty.EASY:
                        WinStatTracker.isLv1Complete = true;
                        break;
                    case GameEnums.GameDifficulty.MEDIUM:
                        WinStatTracker.isLv2Complete = true;
                        break;
                    case GameEnums.GameDifficulty.HARD:
                        WinStatTracker.isLv3Complete = true;
                        break;
                    default:
                        Debug.LogError($"GameEndTrigger ERROR: {GameDifficultyManager.Instance.Difficulty} does not have a respective scene index");
                        break;
                }
            }
        }
    }
}

