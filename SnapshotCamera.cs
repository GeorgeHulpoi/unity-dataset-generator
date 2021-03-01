using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotCamera
{
    private int textureWidth = 512;
    private int textureHeight = 512;
    public Camera camera;
    public bool isActive 
    {
        get { return this.camera.gameObject.activeInHierarchy; }
    }

    public SnapshotCamera(Camera mainCamera)
    {
        if (GameObject.Find("Snapshot Camera") == null)
        {
            this.camera = Camera.Instantiate(mainCamera);
            this.camera.name = "Snapshot Camera";
            this.camera.targetTexture = new RenderTexture(this.textureWidth, this.textureHeight, 24);
            this.SetActive(false);
        }
    }

    public SnapshotCamera(Camera mainCamera, int textureWidth, int textureHeight) : this(mainCamera)
    {
        this.textureWidth = textureWidth;
        this.textureHeight = textureHeight;
    }

    public void SetActive(bool value)
    {
        this.camera.gameObject.SetActive(value);
    }

    public byte[] Capture()
    {
        Texture2D snapshot = new Texture2D(this.textureWidth, this.textureHeight, TextureFormat.RGB24, false);
        this.camera.Render();
        RenderTexture.active = this.camera.targetTexture;
        snapshot.ReadPixels(new Rect(0, 0, this.textureWidth, this.textureHeight), 0, 0);
        byte[] bytes = snapshot.EncodeToPNG();
        return bytes;
    }
}