using System;
using System.Collections;
using TeamDev.Redis;

public class Redis
{
    private RedisDataAccessProvider dataAccessProvider;

    public Redis()
    {
        this.dataAccessProvider = new RedisDataAccessProvider();
    }

    public string StoreDataset(EmotionsDistribution distribution, byte[] image)
    {
        string key = Guid.NewGuid().ToString();
        this.dataAccessProvider.Hash[key]["distribution"] = distribution.Serialize();
        this.dataAccessProvider.Hash[key]["image"] = Convert.ToBase64String(image);
        return key;
    }

    public void Publish(string key)
    {
        this.dataAccessProvider.SendCommand(RedisCommand.PUBLISH, "dataset-generator", key);
    }

    public void Disconnect() 
    {
        this.dataAccessProvider.Close();
    }
}
