using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class Bow : XRGrabInteractable
{
    private PhotonView photonView;
    
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        photonView.RequestOwnership();
        base.OnSelectEntered(interactor);
    }
}
