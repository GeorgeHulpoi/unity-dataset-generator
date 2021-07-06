using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryEmotion : IEmotion
{
    private Hashtable controllers;

    public AngryEmotion(Hashtable controllers)
    {
        this.controllers = controllers;
    }

    public void Apply(float intensity)
    {
        GameObject cntr;

        cntr = (GameObject) this.controllers["BrowIn_L_cntr"];
        cntr.transform.localPosition += new Vector3(0.025f * intensity, -0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["BrowIn_R_cntr"];
        cntr.transform.localPosition += new Vector3(0.025f * intensity, -0.025f * intensity, 0);

        cntr = (GameObject) this.controllers["EyeSqz_R_cntr"];
        cntr.transform.localPosition += new Vector3(intensity * 0.01f, intensity * 0.025f, 0);
        cntr = (GameObject) this.controllers["EyeSqz_L_cntr"];
        cntr.transform.localPosition += new Vector3(intensity * 0.01f, intensity * 0.025f, 0);

        cntr = (GameObject) this.controllers["UprLid_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, intensity * 0.025f, 0);
        cntr = (GameObject) this.controllers["UprLid_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, intensity * 0.025f, 0);

        cntr = (GameObject) this.controllers["Jaw_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0, intensity * 0.025f);

        cntr = (GameObject) this.controllers["UprLip_2_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.025f + intensity * 0.05f, 0);
    }
}