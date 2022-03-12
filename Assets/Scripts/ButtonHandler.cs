using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public GameObject ButtonLogin;
    public GameObject ButtonTransfer;
    public InputField inputName;
    public InputField inputAmount;

    private string userID = null;
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
        userID = inputName.text;
        double amount = float.Parse(System.String.Format("{0:0.0000}", inputAmount.text));
        protonTransfer.ProtonTransfer(userID, amount);
    }
}
