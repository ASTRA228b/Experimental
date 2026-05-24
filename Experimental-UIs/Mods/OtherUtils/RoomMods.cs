using UnityEngine;
using Photon.Pun;
using GorillaNetworking;

namespace Experimental.Mods.OtherUtils;

public static class RoomMods
{
    private static readonly string[] Zoner = 
    {
        "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit"
    };

    public static void JoinRoom(string roomName)
    {
        if (PhotonNetwork.InRoom)
        {
            Leave();
        }
        PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(roomName, JoinType.Solo);
    }

    public static void Leave()
    {
        PhotonNetwork.Disconnect();
    }

    public static void JoinRandom()
    {
        if (PhotonNetwork.InRoom)
        {
            Leave();
        }
        GameObject Value = GameObject.Find(Zoner[UnityEngine.Random.Range(0, Zoner.Length)]);
        Value?.GetComponent<GorillaTriggerBox>().OnBoxTriggered();
    }
}