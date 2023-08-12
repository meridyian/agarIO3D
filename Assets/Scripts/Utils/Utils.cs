using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    // Start is called before the first frame update
    public static void DebugLog(string message)
    {
        Debug.Log($"{Time.time} {message}");
    }
    
    // Return a random position within the playfield
    public static Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-GetPlayFieldSize()/2f, GetPlayFieldSize()/2f), Random.Range(-GetPlayFieldSize()/2f, GetPlayFieldSize()/2f),0)*0.9f;
        
    }

    public static float GetPlayFieldSize()
    {
        return 100f;
    }

    public static string GetRandomName()
    {
        string[] names = { "Eddy", "Freddy", "Teddy", "Paddy", "Buddy", "Neddy", "Herkel" };

        return names[Random.Range(0, names.Length)] + Random.Range(1, 100);
    }
}
