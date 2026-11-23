
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
        Debug.Log("Client Connected: " + clientId);
    }

    void OnDisconnected(ulong clientId)
    {
        Debug.Log("Client Disconnected: " + clientId);
    }

    void OnDestroy()
    {
        if (NetworkManager.Singleton == null) return;

        NetworkManager.Singleton.OnClientConnectedCallback -= OnConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnDisconnected;
    }
}
