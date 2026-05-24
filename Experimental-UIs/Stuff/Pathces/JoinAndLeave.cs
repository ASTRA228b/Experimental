using UnityEngine;
using Photon.Pun;
using Experimental.Core.Libraries;

namespace Experimental.Stuff.Pathces;

public class JoinManager : MonoBehaviour
{
    private bool IsInRoom = false;

    private void Update()
    {
        LobbyDetector(); // useful for something idk just putting my notify code to use lol idk
    }

    private void LobbyDetector()
    {
        bool InRoom = PhotonNetwork.InRoom;
        int PlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        string PlayerName = PhotonNetwork.NickName;
        if (!IsInRoom && InRoom)
        {
            string IndexName = PhotonNetwork.CurrentRoom.Name;
            OnScreenNotify.SendIT($"{PlayerName} Joined Room: {IndexName} | Players {PlayerCount}/10");
        }

        if (IsInRoom && !InRoom)
        {
            OnScreenNotify.SendIT("You left The Room");
        }
        IsInRoom = InRoom;
    }
}