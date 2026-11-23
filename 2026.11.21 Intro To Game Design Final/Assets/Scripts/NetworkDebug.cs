
using UnityEngine;
using Unity.Netcode;

public class NetworkDebug : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnected;
    }

    void OnConnected(ulong clientId)
    {
        Debug.Log("CONNECTED: " + clientId);
    }

    void OnDisconnected(ulong clientId)
    {
        Debug.LogWarning("DISCONNECTED: " + clientId);
    }

    void OnDestroy()
    {
        if (NetworkManager.Singleton == null) return;

        NetworkManager.Singleton.OnClientConnectedCallback -= OnConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnDisconnected;
    }
}
