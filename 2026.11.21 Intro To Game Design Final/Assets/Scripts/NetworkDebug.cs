
using UnityEngine;
using Unity.Netcode;

public class networkDebug : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnected;

        InvokeRepeating(nameof(CheckNetwork), 2f, 3f);
    }

    void CheckNetwork()
    {
        NetworkManager nm = NetworkManager.Singleton;

        if (nm == null)
        {
            return;
        }

    }

    void OnConnected(ulong clientId)
    {
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
