using UnityEngine;

[ExecuteInEditMode]
public class StringRenderer : MonoBehaviour
{
    [Header("Render Positions")]
    [SerializeField] public Transform start;
    [SerializeField] public Transform middle;
    [SerializeField] public Transform end;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // while in editor make sure line renderer follows bow
        if (Application.isEditor/* && !Application.isPlaying*/)
            UpdatePositions();
    }

    private void OnEnable()
    {
        Application.onBeforeRender += UpdatePositions;
    }

    private void OnDisable()
    {
        Application.onBeforeRender -= UpdatePositions;
    }

    private void UpdatePositions()
    {
        // set positions of the line renderer middle position is the notch attach trasform
        lineRenderer.SetPositions(new Vector3[] { start.position, middle.position, end.position });
    }
}
