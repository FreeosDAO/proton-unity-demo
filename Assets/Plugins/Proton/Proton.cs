using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;
using AOT;

public class Proton : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Login(Action<int> callback);

    [DllImport("__Internal")]
    private static extern void Logout();

    [DllImport("__Internal")]
    private static extern void Reconnect(Action<int> callback);

    [DllImport("__Internal")]
    private static extern void Transfer(string to, double amount, Action<string> callback);

    [DllImport("__Internal")]
    private static extern void GetProtonAvatar(string account);

    [MonoPInvokeCallback(typeof(Action<int>))]
    public static void ReconnectCallback(int status)
    {
        if (status == 1)
        {
            // Transfer("token.burn", 0.0001, TransferCallback);
        }
        else
        {
            Login(LoginCallback);
        }
    }

    [MonoPInvokeCallback(typeof(Action<int>))]
    public static void LoginCallback(int status)
    {
        if (status == 1)
        {
            // Transfer("token.burn", 0.0001, TransferCallback);
        }
        else
        {
            Debug.Log("Error Logging In");
        }
    }

    [MonoPInvokeCallback(typeof(Action<string>))]
    public static void TransferCallback(string txid)
    {
        Debug.Log("TX ID: " + txid);
    }

    public void ProtonLogin()
    {
        Reconnect(ReconnectCallback);
    }


    public void ProtonTransfer(string to, double amount)
    {
        Transfer(to, amount, TransferCallback);
    }

    // Main
    void Start()
    {
        // Reconnect(ReconnectCallback);
    }

    // Update is called once per frame
    void Update()
    {

    }
}