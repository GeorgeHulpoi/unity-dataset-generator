using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseEmotion : IEmotion
{
    private Hashtable controllers;

    public SurpriseEmotion(Hashtable controllers)
    {
        this.controllers = controllers;
    }

    public void Apply(float intensity)
    {
        GameObject cntr;

        cntr = (GameObject) this.controllers["EyeSqz_R_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["EyeSqz_L_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);

        cntr = (GameObject) this.controllers["UprLid_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.0075f * intensity, 0);
        cntr = (GameObject) this.controllers["UprLid_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.0075f * intensity, 0);

        cntr = (GameObject) this.controllers["BrowIn_R_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["BrowIn_L_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);

        cntr = (GameObject) this.controllers["Jaw_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.014f * intensity, 0);
    }
}