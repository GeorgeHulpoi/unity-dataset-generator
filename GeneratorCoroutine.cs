using System;
using System.Collections;
using UnityEngine;

public class GeneratorCoroutine 
{
    private EmotionsController emotionsController;
    private SnapshotCamera snapshotCamera;
    private EmotionsDistribution currentEmotionsDistribution;
    private Light directionalLight;

    public GeneratorCoroutine() {}

    public GeneratorCoroutine SetEmotionsController(EmotionsController emotionsController)
    {
        this.emotionsController = emotionsController;
        return this;
    }

    public GeneratorCoroutine SetSnapshotCamera(SnapshotCamera snapshotCamera)
    {
        this.snapshotCamera = snapshotCamera;
        return this;
    }

    public GeneratorCoroutine SetCurrentEmotionsDistribution(EmotionsDistribution currentEmotionsDistribution)
    {
        this.currentEmotionsDistribution = currentEmotionsDistribution;
        return this;
    }

    public GeneratorCoroutine SetDirectionalLight(Light directionalLight)
    {
        this.directionalLight = directionalLight;
        return this;
    }
    
    public IEnumerator Start(int numberOfImages) 
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            // Emotions
            if (this.emotionsController != null && this.currentEmotionsDistribution != null)
            {
                this.emotionsController.Reset();
                EmotionsDistribution distribution = this.emotionsController.Randomize(0.8f, 0.95f);
                this.currentEmotionsDistribution.Copy(distribution);
            }

            // Lights
            if (this.directionalLight)
            {
                Vector3 rotation = new Vector3(this.directionalLight.transform.eulerAngles.x,
                                               UnityEngine.Random.Range(-15.0f, 15.0f),
                                               this.directionalLight.transform.eulerAngles.z);
                this.directionalLight.transform.eulerAngles = rotation;
            }

            if (this.snapshotCamera != null)
            {
                this.snapshotCamera.SetActive(true);
            }
            
            yield return null;
        }
    }
}