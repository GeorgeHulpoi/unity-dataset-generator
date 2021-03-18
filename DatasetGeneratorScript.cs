using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatasetGeneratorScript : MonoBehaviour
{
    [SerializeField]
    public GameObject snappersControllers;

    [SerializeField]
    public Camera camera;

    [SerializeField]
    public GameObject directionalLight;

    [SerializeField]
    public int textureWidth = 512;
    
    [SerializeField]
    public int textureHeight = 512;

    [SerializeField]
    public int numberOfImages;

    private Hashtable controllers;
    private EmotionsController emotionsController;
    private SnapshotCamera snapshotCamera;
    private GeneratorCoroutine generatorCoroutine = new GeneratorCoroutine();
    private EmotionsDistribution currentEmotionsDistribution = new EmotionsDistribution();
    private Redis redis = new Redis();
    private bool skipFirstLateUpdate;

    void Start()
    {
        this.generatorCoroutine = this.generatorCoroutine.SetCurrentEmotionsDistribution(this.currentEmotionsDistribution);

        if (this.snappersControllers != null) 
        {
            this.controllers = this.GetFacialControllers(this.snappersControllers);
            this.emotionsController = new EmotionsController(this.controllers);
            this.generatorCoroutine = this.generatorCoroutine.SetEmotionsController(this.emotionsController);
        }

        this.ClearScene();
        this.SetupLight();
        this.SetupCamera();

        if (this.camera != null)
        {
            this.snapshotCamera = new SnapshotCamera(this.camera, this.textureWidth, this.textureHeight);
            this.generatorCoroutine = this.generatorCoroutine.SetSnapshotCamera(this.snapshotCamera);
            
        }
    
        if (this.directionalLight != null)
        {
            this.generatorCoroutine = this.generatorCoroutine.SetDirectionalLight(this.directionalLight);
        }
    }

    void Update() 
    {
        if (Input.GetButtonDown("Fire1") && this.generatorCoroutine != null)
        {
            this.skipFirstLateUpdate = true;
            StartCoroutine(this.generatorCoroutine.Start(this.numberOfImages));
        }
    }

    void LateUpdate() 
    {
        if (this.snapshotCamera != null && this.snapshotCamera.isActive)
        {
            if (this.skipFirstLateUpdate == true)
            {
                this.skipFirstLateUpdate = false;
                return;
            }
            
            byte[] image = this.snapshotCamera.Capture();
            string key = this.redis.StoreDataset(this.currentEmotionsDistribution, image);
            this.redis.Publish(key);
            this.snapshotCamera.SetActive(false);
        }
    }

    void OnApplicationQuit() 
    {
        if (this.snapshotCamera != null)
        {
            DestroyImmediate(this.snapshotCamera.camera.gameObject);
        }

        if (this.redis != null)
        {
            this.redis.Disconnect();
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
        if (this.directionalLight != null)
        {
            this.directionalLight.transform.rotation = Quaternion.Euler(1, 0, 0);
            this.directionalLight.SetActive(true);
        }
        else 
        {
            Debug.LogWarning("Directional Light not found!");
        }
    }

    private void SetupCamera()
    {
        GameObject eyeFocus = GameObject.Find("EyeFocus");

        if (eyeFocus != null)
        {
            eyeFocus.transform.position = new Vector3(0, 1.7078f, 0);

            FocusClosestTarget focusClosestTarget = (FocusClosestTarget) eyeFocus.GetComponent(typeof(FocusClosestTarget));
            if (focusClosestTarget != null)
            {
                focusClosestTarget.focusNearDistance = 2.5f;
            }
        }
        else 
        {
            Debug.LogWarning("Eye Focus not found!");
        }

        GameObject cameraFocusTarget = GameObject.Find("Camera Focus Target");
        if (cameraFocusTarget != null)
        {
            cameraFocusTarget.transform.position = new Vector3(0, 1.5792f, 0);
        }
        else 
        {
            Debug.LogWarning("Camera Focus Target not found!");
        }

        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null)
        {
            AudioListener audioListener = camera.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                DestroyImmediate(audioListener);
            }
            camera.transform.position = new Vector3(0, 1.72f, -1.321f);
        }
        else 
        {
            Debug.LogWarning("Main Camera not found!");
        }
    }
}