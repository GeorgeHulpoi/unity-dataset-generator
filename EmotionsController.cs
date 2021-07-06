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


    public void Reset() 
    {
        foreach (DictionaryEntry controller in this.controllers)
        {
            GameObject obj = (GameObject) controller.Value;
            obj.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public EmotionsDistribution Randomize(IEmotion mainEmotion, float mainEmotionMinIntensity, float mainEmotionMaxIntensity)
    {
        EmotionsDistribution distribution = new EmotionsDistribution();
        List<IEmotion> emotionsPool = this.GetPool();
        IEmotion emotion;

        float mainEmotionIntensity = Random.Range(mainEmotionMinIntensity, mainEmotionMaxIntensity);
        int index = emotionsPool.FindIndex(e => e.GetType() == mainEmotion.GetType());
        emotion = emotionsPool[index];
        emotionsPool.RemoveAt(index);
        emotion.Apply(mainEmotionIntensity);
        distribution.SetDistribution(emotion, mainEmotionIntensity);

        float remainingIntensity = 1 - mainEmotionIntensity;
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