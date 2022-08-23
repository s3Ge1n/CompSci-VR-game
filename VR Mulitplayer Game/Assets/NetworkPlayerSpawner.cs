using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
        photonView.RPC("color", RpcTarget.AllBuffered, r, g, b, spawnedPlayerPrefab);
    }

    [PunRPC]
    void color(float r, float g, float b, GameObject spawnedPlayerPrefab)
    {
        Color customColor = new Color(r, g, b, 1f);
        foreach (Renderer rend in spawnedPlayerPrefab.GetComponentsInChildren<Renderer>())
        {
            rend.material.SetColor("_Color", customColor);
        }
        //spawnedPlayerPrefab.GetComponentInChildren<Renderer>().material.color = new Color(r, g, b, 1f);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
