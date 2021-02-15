using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DatasetGeneratorScript : MonoBehaviour
{
    [SerializeField]
    public GameObject snappersControllers;

    [Range(0.0f, 1.0f)]
    public float angryIntensity;

    [Range(0.0f, 1.0f)]
    public float happyIntensity;

    [Range(0.0f, 1.0f)]
    public float surpriseIntensity;

    [Range(0.0f, 1.0f)]
    public float fearIntensity;

    private Hashtable controllers;
    private IEmotion angryEmotion;
    private IEmotion happyEmotion;
    private IEmotion surpriseEmotion;
    private IEmotion fearEmotion;

    void Start()
    {
        if (this.snappersControllers != null) 
        {
            this.controllers = this.GetFacialControllers(this.snappersControllers);
            this.ResetControllersPosition(this.controllers);

            this.angryEmotion = new AngryEmotion(this.controllers);
            this.happyEmotion = new HappyEmotion(this.controllers);
            this.surpriseEmotion = new SurpriseEmotion(this.controllers);
            this.fearEmotion = new FearEmotion(this.controllers);
        }

        this.ClearScene();
        this.SetupLight();
        this.SetupCamera();
    }

    void Update() 
    {
        if (this.controllers != null)
        {
            this.ResetControllersPosition(this.controllers);

            if (this.angryEmotion != null)
            {
                this.angryEmotion.Apply(this.angryIntensity);
            }

            if (this.happyEmotion != null)
            {
                this.happyEmotion.Apply(this.happyIntensity);
            }

            if (this.surpriseEmotion != null)
            {
                this.surpriseEmotion.Apply(this.surpriseIntensity);
            }

            if (this.fearEmotion != null)
            {
                this.fearEmotion.Apply(this.fearIntensity);
            }
        }
    }

    private void ResetControllersPosition(Hashtable controllers)
    {
        foreach (DictionaryEntry controller in controllers)
        {
            GameObject obj = (GameObject) controller.Value;
            obj.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    private Hashtable GetFacialControllers(GameObject root) 
    {
        Transform controllersParentTransform = root.transform.Find("Controllers_Parent");
        GameObject controllersParent = null;

        if (controllersParentTransform == null) { return null; }

        controllersParent = controllersParentTransform.gameObject;
        Hashtable table = new Hashtable();

        foreach(Transform child in controllersParent.transform) 
        {
            GameObject leaf = GetLeafFromHierarchy(child.gameObject);
            if (leaf != null)
            {
                table.Add(leaf.name, leaf);
            }
        }

        return table;
    }

    private GameObject GetLeafFromHierarchy(GameObject root)
    {
        Transform transform = root.transform.childCount > 0 ? root.transform.GetChild(0) : null; 
        return (transform != null) ? GetLeafFromHierarchy(transform.gameObject) : root;
    }

    private void ClearScene()
    {
        List<string> targets = new List<string>
            {"EventSystem", "Canvas", "Audio", "Ground", "Light Setups Stationary",
            "Camera_FullShot_root", "Camera 4D_Closeup", "Timeline" };

        foreach (string name in targets)
        {
            GameObject target = GameObject.Find(name);
            if (target != null)
            {
                target.SetActive(false);
            }
        }
    }

    private void SetupLight()
    {
        GameObject light = GameObject.Find("Directional Light");

        if (light != null)
        {
            light.transform.rotation = Quaternion.Euler(1, 0, 0);
        }
    }

    private void SetupCamera()
    {
        GameObject focusTarget = GameObject.Find("EyeFocus");

        if (focusTarget != null)
        {
            focusTarget.transform.position = new Vector3(0, 1.7f, 0);
        }

        GameObject camera = GameObject.Find("Main Camera");

        if (camera != null)
        {
            camera.transform.position = new Vector3(0, 1.72f, -1.321f);
        }
    }
}