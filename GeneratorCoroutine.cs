using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCoroutine 
{
    private EmotionsController emotionsController;
    private SnapshotCamera snapshotCamera;
    private GameObject directionalLight;
    public int frame = 0;
    public EmotionsDistribution currentEmotionsDistribution;

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

    public GeneratorCoroutine SetDirectionalLight(GameObject directionalLight)
    {
        this.directionalLight = directionalLight;
        return this;
    }
    
    public IEnumerator Start(int numberOfImages) 
    {
        if (this.emotionsController == null || this.directionalLight == null 
        || this.snapshotCamera == null)
        {
             throw new Exception("You cannot start without all fields.");
        }

        List<IEmotion> emotionsPool = emotionsController.GetPool();
        int totalEmotions = emotionsPool.Count;

        for (int i = 0; i < numberOfImages; i++)
        {
            // Emotions
            this.emotionsController.Reset();
            IEmotion mainEmotion = emotionsPool[i % totalEmotions];
            this.currentEmotionsDistribution = this.emotionsController.Randomize(mainEmotion, 0.7f, 0.95f);

            // Lights
            Vector3 rotation = new Vector3(UnityEngine.Random.Range(1.0f, 45.0f),
                                            UnityEngine.Random.Range(-15.0f, 15.0f),
                                            this.directionalLight.transform.eulerAngles.z);
            this.directionalLight.transform.eulerAngles = rotation;

            // Camera
            this.snapshotCamera.RandomizePosition();
            this.snapshotCamera.SetActive(true);

            yield return new WaitUntil(() => 
            {
                if (this.frame > 3)
                {
                    this.frame = 0;
                    return true;
                }
                else return false;
            });
        }
    }
}