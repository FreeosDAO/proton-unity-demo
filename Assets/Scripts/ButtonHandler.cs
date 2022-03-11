using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public GameObject ButtonLogin;
    public GameObject ButtonTransfer;
    public GameObject InputAmount;
    public GameObject InputName;

    void Start()
    {
        // Set ButtonLogin Text
        //ButtonLogin = GameObject.Find("ButtonLogin");
        Text loginText = ButtonLogin.transform.Find("Text").GetComponent<Text>();
        loginText.text = "Login";

        // Set ButtonTransfer Text
        //ButtonTransfer = GameObject.Find("ButtonTransfer");
        Text transferText = ButtonTransfer.transform.Find("Text").GetComponent<Text>();
        transferText.text = "Tranfer";

        // Get Input Amount
        Text inputAmount = InputAmount.transform.Find("Text").GetComponent<Text>();

        // Get Input Name
        Text inputName = InputName.transform.Find("Text").GetComponent<Text>();

    }

    // Login() Function
    private Proton protonLogin;
    public void Login()
    {
        protonLogin.ProtonLogin();

    }

    // Transfer() Function
    private Proton protonTransfer;
    public void Transfer()
    {
        protonTransfer.ProtonTransfer();
    }
}
