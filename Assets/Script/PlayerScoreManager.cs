using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI score;
    private const string ScoreKey = "PlayerScore"; // Key to identify the score property.

    // Set the player's score.
    public void SetPlayerScore(int newScore)
    {
        // Create a hashtable to hold the custom property.
        ExitGames.Client.Photon.Hashtable scoreProperty = new ExitGames.Client.Photon.Hashtable
        {
            { ScoreKey, newScore } // Set the score value with the key.
        };

        // Update the custom property for the local player.
        PhotonNetwork.LocalPlayer.SetCustomProperties(scoreProperty);
    }

    // Get the player's score.
    public int GetPlayerScore(Player player)
    {
        if (player.CustomProperties.TryGetValue(ScoreKey, out object scoreValue))
        {
            return (int)scoreValue;
        }

        return 0; // Default score if no value is set.
    }

    // Callback when a player's properties are updated.
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey(ScoreKey))
        {
            Debug.Log($"Player {targetPlayer.NickName}'s score updated to {changedProps[ScoreKey]}");
        }
    }

    // Example usage to update score (e.g., on button press or event).
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        int currentScore = GetPlayerScore(PhotonNetwork.LocalPlayer);
    //        SetPlayerScore(currentScore + 10); // Increase score by 10.
    //    }
    //}
}
