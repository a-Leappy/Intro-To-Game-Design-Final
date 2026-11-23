using System;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;

public class networkStartUI : MonoBehaviour
{
    private string joinCode = "";
    private string hostCode = "";
    private string status = "";
    private bool isStarting = false;

    async System.Threading.Tasks.Task InitializeServices()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async void StartHostRelay()
    {
        if (isStarting) return;
        isStarting = true;
        status = "Creating Relay session...";

        try
        {
            await InitializeServices();

            // One additional player, plus the host = 2 players
            Allocation allocation =
                await RelayService.Instance.CreateAllocationAsync(1);

            hostCode =
                await RelayService.Instance.GetJoinCodeAsync(
                    allocation.AllocationId
                );

            UnityTransport transport =
                NetworkManager.Singleton.GetComponent<UnityTransport>();

            transport.SetRelayServerData(
                new RelayServerData(allocation, "dtls")
            );

            if (NetworkManager.Singleton.StartHost())
            {
                status = "Hosting!";
                Debug.Log("Relay Join Code: " + hostCode);
            }
            else
            {
                status = "Failed to start host";
                hostCode = "";
            }
        }
        catch (Exception e)
        {
            status = "Host error: " + e.Message;
            Debug.LogException(e);
        }
        finally
        {
            isStarting = false;
        }
    }

    async void StartClientRelay()
    {
        if (isStarting) return;
        isStarting = true;
        status = "Joining Relay session...";

        try
        {
            await InitializeServices();

            JoinAllocation allocation =
                await RelayService.Instance.JoinAllocationAsync(
                    joinCode.Trim().ToUpperInvariant()
                );

            UnityTransport transport =
                NetworkManager.Singleton.GetComponent<UnityTransport>();

            transport.SetRelayServerData(
                new RelayServerData(allocation, "dtls")
            );

            if (NetworkManager.Singleton.StartClient())
            {
                status = "Connecting to host...";
            }
            else
            {
                status = "Failed to start client";
            }
        }
        catch (Exception e)
        {
            status = "Client error: " + e.Message;
            Debug.LogException(e);
        }
        finally
        {
            isStarting = false;
        }
    }

    void OnGUI()
    {
        if (NetworkManager.Singleton == null) return;

        float w = 250f, h = 40f;
        float x = 10f, y = 10f;

        if (!NetworkManager.Singleton.IsClient &&
            !NetworkManager.Singleton.IsServer)
        {
            GUI.enabled = !isStarting;

            if (GUI.Button(new Rect(x, y, w, h), "Host"))
            {
                StartHostRelay();
            }

            y += h + 10f;

            joinCode = GUI.TextField(
                new Rect(x, y, w, h), joinCode
            );

            y += h + 10f;

            if (GUI.Button(new Rect(x, y, w, h), "Join"))
            {
                StartClientRelay();
            }

            GUI.enabled = true;
        }

        y += h + 10f;

        if (!string.IsNullOrEmpty(hostCode))
        {
            GUI.Label(
                new Rect(x, y, 400f, h),
                "Join Code: " + hostCode
            );
            y += h;
        }

        GUI.Label(
            new Rect(x, y, 600f, 60f),
            status
        );
    }
}
