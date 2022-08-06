using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class Quiver : XRBaseInteractable
{
    //[SerializeField] public GameObject arrowPrefab;
    [SerializeField] public string arrowPrefab;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        CreateAndSelectArrow(args);
    }

    private void CreateAndSelectArrow(SelectEnterEventArgs args)
    {
        //Create Arrow, force into interacting hand
        Arrow arrow = CreateArrow(args.interactorObject.transform);
        interactionManager.SelectEnter(args.interactorObject, arrow);
    }

    private Arrow CreateArrow(Transform orientation)
    {
        // create arrow and get arrow component
        GameObject arrowObject = PhotonNetwork.Instantiate(arrowPrefab, orientation.position, orientation.rotation);
        return arrowObject.GetComponent<Arrow>();
    }

}
