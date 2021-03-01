using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionsController
{
    private Hashtable controllers;
    private IEmotion angryEmotion;
    private IEmotion happyEmotion;
    private IEmotion surpriseEmotion;
    
    public EmotionsController(Hashtable controllers)
    {
        if (controllers == null)
        {
            throw new System.Exception("You must provide a controllers");
        }

        this.controllers = controllers;
        this.angryEmotion = new AngryEmotion(controllers);
        this.happyEmotion = new HappyEmotion(controllers);
        this.surpriseEmotion = new SurpriseEmotion(controllers);
    }

    public EmotionsController SetAngryIntensity(float intensity)
    {
        this.angryEmotion.Apply(intensity);
        return this;
    }

    public EmotionsController SetHappyIntensity(float intensity)
    {
        this.happyEmotion.Apply(intensity);
        return this;
    }

    public EmotionsController SetSurpriseIntensity(float intensity)
    {
        this.surpriseEmotion.Apply(intensity);
        return this;
    }

    public void Reset() 
    {
        foreach (DictionaryEntry controller in this.controllers)
        {
            GameObject obj = (GameObject) controller.Value;
            obj.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public EmotionsDistribution Randomize(float mainEmotionMinIntensity, float mainEmotionMaxIntensity)
    {
        EmotionsDistribution distribution = new EmotionsDistribution();
        List<IEmotion> emotionsPool = this.GetPool();

        float mainEmotionIntensity = Random.Range(mainEmotionMinIntensity, mainEmotionMaxIntensity);
        int index = Random.Range(0, emotionsPool.Count);
        IEmotion mainEmotion = emotionsPool[index];
        emotionsPool.RemoveAt(index);
        mainEmotion.Apply(mainEmotionIntensity);
        distribution.SetDistribution(mainEmotion, mainEmotionIntensity);

        float remainingIntensity = 1 - mainEmotionIntensity;
        IEmotion emotion;
        while (emotionsPool.Count > 1)
        {
            index = Random.Range(0, emotionsPool.Count);
            emotion = emotionsPool[index];
            emotionsPool.RemoveAt(index);
            float intensity = Random.Range(0.0f, remainingIntensity);
            emotion.Apply(intensity);
            distribution.SetDistribution(emotion, intensity);
            remainingIntensity = remainingIntensity - intensity;
        }

        emotion = emotionsPool[0];
        emotion.Apply(remainingIntensity);
        distribution.SetDistribution(emotion, remainingIntensity);
        return distribution;
    }

    public List<IEmotion> GetPool()
    {
        List<IEmotion> pool = new List<IEmotion>();
        pool.Add(this.angryEmotion);
        pool.Add(this.happyEmotion);
        pool.Add(this.surpriseEmotion);
        return pool;
    }
}