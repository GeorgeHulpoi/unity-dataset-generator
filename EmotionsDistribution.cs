using System;

public class EmotionsDistribution
{
    public float angry;
    public float happy;
    public float surprise;

    public EmotionsDistribution() {}

    public void SetDistribution(IEmotion emotion, float intensity)
    {
        if (typeof(AngryEmotion).IsInstanceOfType(emotion))
        {
            this.angry = intensity;
        }
        else if (typeof(HappyEmotion).IsInstanceOfType(emotion))
        {
            this.happy = intensity;
        }
        else if (typeof(SurpriseEmotion).IsInstanceOfType(emotion))
        {
            this.surprise = intensity;
        }  
    }

    public void Copy(EmotionsDistribution distribution)
    {
        this.angry = distribution.angry;
        this.happy = distribution.happy;
        this.surprise = distribution.surprise;
    }

    public String Serialize() 
    {
        return $"{{\"angry\":\"{this.angry}\",\"happy\":\"{this.happy}\",\"surprise\":\"{this.surprise}\"}}";
    }
}