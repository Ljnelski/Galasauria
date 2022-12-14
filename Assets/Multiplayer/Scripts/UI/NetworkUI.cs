using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        serverButton.onClick.AddListener(OnServerButtonClick);
        hostButton.onClick.AddListener(OnHostButtonClick);
        clientButton.onClick.AddListener(OnClientButtonClick);
    }

    private void OnServerButtonClick()
    {
        NetworkManager.Singleton.StartServer();
        CloseCanvas();
    }

    private void OnHostButtonClick()
    {
        NetworkManager.Singleton.StartHost();
        CloseCanvas();
    }

    private void OnClientButtonClick()
    {
        NetworkManager.Singleton.StartClient();
        CloseCanvas();
    }

    private void CloseCanvas()
    {
        gameObject.SetActive(false);
    }
}
