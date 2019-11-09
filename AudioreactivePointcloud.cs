using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioreactivePointcloud : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] baseVertices, vertices;
    [SerializeField] Lasp.FilterType _filterType = Lasp.FilterType.Bypass;
    const float kSilence = -40; // -40 dBFS = silence
    float rms, rmsScaled; // instantiate rms variables

    // Start is called before the first frame update
    void Start()
    {   
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        // get rms
        rms = Lasp.MasterInput.CalculateRMSDecibel(_filterType);
        rmsScaled = Mathf.Clamp01(1 - rms / kSilence);

        vertices = new Vector3[baseVertices.Length];
        
        for (var i = 0; i < vertices.Length; i++) {
            vertices[i] = baseVertices[i] + new Vector3(
                rmsScaled * 2 * Mathf.Sin(i),
                rmsScaled * -5 * Mathf.Sin(i),
                rmsScaled * 3 * Mathf.Sin(i)
            );
        }

        mesh.vertices = vertices;

        mesh.RecalculateBounds();
    }
}
