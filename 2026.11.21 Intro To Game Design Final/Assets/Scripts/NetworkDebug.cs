
using UnityEngine;
using Unity.Netcode;

public class networkDebug : MonoBehaviour
{
    void Start()
    {
        Debug.Log("NETWORK DEBUG LOADED");

        NetworkManager.Singleton.OnClientConnectedCallback += OnConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnected;

        InvokeRepeating(nameof(CheckNetwork), 2f, 3f);
    }

    void CheckNetwork()
    {
        NetworkManager nm = NetworkManager.Singleton;

        if (nm == null)
        {
            Debug.LogError("NetworkManager is NULL");
            return;
        }

        Debug.Log("Is Client: " + nm.IsClient +
                  " | Is Server: " + nm.IsServer +
                  " | Is Connected: " + nm.IsConnectedClient +
                  " | Is Listening: " + nm.IsListening);
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
        CancelInvoke();

        if (NetworkManager.Singleton == null) return;

        NetworkManager.Singleton.OnClientConnectedCallback -= OnConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnDisconnected;
    }
}
