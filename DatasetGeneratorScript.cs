using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DatasetGeneratorScript : MonoBehaviour
{
    [SerializeField]
    public GameObject snappersControllers;

    [SerializeField]
    public Camera camera;

    [SerializeField]
    public Transform anchor;

    [SerializeField]
    public GameObject directionalLight;

    private int numberOfImages = 0;
    private int textureWidth = 512;
    private int textureHeight = 512;
    private Hashtable controllers;
    private EmotionsController emotionsController;
    private SnapshotCamera snapshotCamera;
    private GeneratorCoroutine generatorCoroutine = new GeneratorCoroutine();
    private Redis redis = new Redis();

    void Start()
    {
        string[] args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            if (i + 1 < args.Length)
            {
                if (args[i] == "--size")
                {
                    this.numberOfImages = System.Int32.Parse(args[i + 1]);
                }
                else if (args[i] == "--w" || args[i] == "--width")
                {
                    this.textureWidth = System.Int32.Parse(args[i + 1]);
                }
                else if (args[i] == "--h" || args[i] == "--height")
                {
                    this.textureHeight = System.Int32.Parse(args[i + 1]);
                }
            }
        }
        
        if (this.snappersControllers != null) 
        {
            this.controllers = this.GetFacialControllers(this.snappersControllers);
            this.emotionsController = new EmotionsController(this.controllers);
            this.generatorCoroutine = this.generatorCoroutine.SetEmotionsController(this.emotionsController);
        }

        this.ClearScene();
        this.SetupLight();
        this.SetupCamera();

        if (this.camera != null && this.anchor != null)
        {
            this.snapshotCamera = new SnapshotCamera(this.camera, this.anchor, this.textureWidth, this.textureHeight);
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
            StartCoroutine(this.generatorCoroutine.Start(this.numberOfImages));
        }
    }

    void LateUpdate() 
    {
        if (this.snapshotCamera != null && this.snapshotCamera.isActive)
        {
            if (this.generatorCoroutine.frame < 3)
            {
                this.generatorCoroutine.frame++;
            }
            else 
            {
                byte[] image = this.snapshotCamera.Capture();
                string key = this.redis.StoreDataset(this.generatorCoroutine.currentEmotionsDistribution, image);
                this.redis.Publish(key);
                this.snapshotCamera.SetActive(false);
                this.generatorCoroutine.frame = 100;
            }
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
            eyeFocus.transform.position = new Vector3(0, 1.75f, 0);

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
            cameraFocusTarget.transform.position = new Vector3(0, 1.75f, 0);
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
            camera.transform.position = new Vector3(0, 1.75f, -1.0f);
        }
        else 
        {
            Debug.LogWarning("Main Camera not found!");
        }
    }
}