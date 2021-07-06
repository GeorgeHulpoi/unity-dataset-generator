using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyEmotion : IEmotion
{
    private Hashtable controllers;

    public HappyEmotion(Hashtable controllers)
    {
        this.controllers = controllers;
    }

    public void Apply(float intensity)
    {
        GameObject cntr;
        
        cntr = (GameObject) this.controllers["Mouth_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);

        cntr = (GameObject) this.controllers["Crnr_R_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["Crnr_L_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);

        cntr = (GameObject) this.controllers["Cheek_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["Cheek_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);

        cntr = (GameObject) this.controllers["EyeSqz_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.011f * intensity, 0);
        cntr = (GameObject) this.controllers["EyeSqz_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.011f * intensity, 0);

        cntr = (GameObject) this.controllers["Jaw_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.0082f * intensity, 0);
    }
}