using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Example Unity Client for connecting to the QuantSlot Math Engine API.
/// This script demonstrates how to send an HTTP POST request to simulate spins.
/// </summary>
public class QuantSlotClient : MonoBehaviour
{
    [Header("API Settings")]
    public string apiBaseUrl = "https://quantslot-api.onrender.com/api";
    
    [Header("Simulation Parameters")]
    public int spinsToSimulate = 100000;
    public int betAmount = 10;

    void Start()
    {
        Debug.Log("🎰 Initializing QuantSlot Client...");
        StartCoroutine(CheckHealthRoutine());
    }

    public void RunSimulation()
    {
        StartCoroutine(SimulateRoutine(spinsToSimulate, betAmount));
    }

    private IEnumerator CheckHealthRoutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"{apiBaseUrl}/health"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Health Check Failed: {webRequest.error}");
            }
            else
            {
                Debug.Log($"API Health Check OK: {webRequest.downloadHandler.text}");
                // Automatically run a simulation after successful health check for demo purposes
                RunSimulation();
            }
        }
    }

    private IEnumerator SimulateRoutine(int spins, int bet)
    {
        string simulateUrl = $"{apiBaseUrl}/simulate";
        
        // Create JSON payload manually or use JsonUtility
        string jsonPayload = $"{{\"spins\": {spins}, \"bet\": {bet}}}";
        
        using (UnityWebRequest webRequest = new UnityWebRequest(simulateUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            Debug.Log($"Sending Simulation Request: {spins} spins...");
            
            float startTime = Time.time;
            yield return webRequest.SendWebRequest();
            float elapsedTime = Time.time - startTime;

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Simulation Request Failed: {webRequest.error}");
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                Debug.Log($"✅ Simulation Complete in {elapsedTime:F2} seconds!");
                Debug.Log($"Results: {jsonResponse}");
                
                // In a real project, you would parse the JSON using JsonUtility or a library like Newtonsoft.Json
                // QuantSlotResponse response = JsonUtility.FromJson<QuantSlotResponse>(jsonResponse);
            }
        }
    }
}
