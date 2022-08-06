using UnityEngine;
using Photon.Pun;

public class SetColour : MonoBehaviour
{
    private PhotonView myPV;
    private Color color;

    void Start()
    {
        myPV = GetComponent<PhotonView>();
        if (myPV.IsMine)
        {
            color = Random.ColorHSV();
            myPV.RPC("RPC_SetColor", RpcTarget.AllBuffered, color);
        }
    }


    [PunRPC]
    void RPC_SetColor(Color transferredColor)
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = transferredColor;
    }
}
