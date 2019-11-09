using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioreactiveCube : MonoBehaviour
{
    [SerializeField] Lasp.FilterType _filterType = Lasp.FilterType.Bypass;
    const float kSilence = -40; // -40 dBFS = silence
    float rms, rmsScaled; // instantiate rms variables

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get rms
        rms = Lasp.MasterInput.CalculateRMSDecibel(_filterType);
        rmsScaled = Mathf.Clamp01(1 - rms / kSilence);
        Debug.Log(rms + " | " + rmsScaled); // show rms in console

        // set position
        transform.position = new Vector3(
            0,
            0,
            rmsScaled * 10
        );
        
        // set rotation
        transform.eulerAngles += new Vector3(
            rmsScaled * 10,
            rmsScaled * -5,
            rmsScaled * 2
        );
    }
}
