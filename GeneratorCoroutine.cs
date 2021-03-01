using System;
using System.Collections;

public class GeneratorCoroutine 
{
    private EmotionsController emotionsController;
    private SnapshotCamera snapshotCamera;
    private EmotionsDistribution currentEmotionsDistribution;
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
    
    public IEnumerator Start(int numberOfImages) 
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            this.emotionsController.Reset();
            EmotionsDistribution distribution = this.emotionsController.Randomize(0.8f, 0.95f);
            this.currentEmotionsDistribution.Copy(distribution);
            this.snapshotCamera.SetActive(true);
            yield return null;
        }
    }
}