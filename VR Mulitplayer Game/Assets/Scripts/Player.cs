using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    [SerializeField] float health;
    [SerializeField] Transform head;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.LogError(string.Format("Player health: {0}", health));
        if (health <= 0)
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel(0);
        }
    }

    public Vector3 GetHeadPosition()
    {
        return head.position;
    }
}
