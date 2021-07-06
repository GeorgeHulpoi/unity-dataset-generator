using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotCamera
{
    private int textureWidth;
    private int textureHeight;
    private Transform defaultPosition;
    private Transform anchor;
    public Camera camera;
    public bool isActive 
    {
        get { return this.camera.gameObject.activeInHierarchy; }
    }

    public SnapshotCamera(Camera mainCamera, Transform anchor, int textureWidth, int textureHeight)
    {
        this.anchor = anchor;
        this.textureWidth = textureWidth;
        this.textureHeight = textureHeight;

        if (GameObject.Find("Snapshot Camera") == null)
        {
            this.defaultPosition = mainCamera.transform;
            this.camera = Camera.Instantiate(mainCamera);
            this.camera.name = "Snapshot Camera";
            this.camera.targetTexture = new RenderTexture(this.textureWidth, this.textureHeight, 24);
            this.SetActive(false);
        }
    }

    public void RandomizePosition()
    {
        float offsetX = Random.Range(-0.25f, 0.25f);
        float offsetY = Random.Range(-0.25f, 0.25f);

        this.camera.transform.position = this.defaultPosition.position + new Vector3(offsetX, offsetY, 0.0f);
        this.camera.transform.LookAt(this.anchor);
    }

    public void SetActive(bool value)
    {
        this.camera.gameObject.SetActive(value);
    }

    public byte[] Capture()
    {
        Texture2D snapshot = new Texture2D(this.textureWidth, this.textureHeight, TextureFormat.RGB24, false);
        snapshot.hideFlags = HideFlags.HideAndDontSave;
        RenderTexture currentActiveRT = RenderTexture.active;

        RenderTexture.active = this.camera.targetTexture;
        this.camera.Render();
        snapshot.ReadPixels(new Rect(0, 0, this.textureWidth, this.textureHeight), 0, 0);
        byte[] bytes = snapshot.EncodeToPNG();
        RenderTexture.active = currentActiveRT;
        Object.Destroy(snapshot);
        return bytes;
    }
}