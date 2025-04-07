using UnityEngine;
using SharpOSC;
using TMPro;
public class OSC_Teleport : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string ipAddress = "192.168.0.102";
    public int port = 8000;
    private UDPSender oscSender;
    public TextMeshProUGUI inputIp;
    public GameObject ipPage;
    public GameObject teleportPage;
    void Start()
    {
        oscSender = new UDPSender(ipAddress, port);
    }
    public void Teleport()
    {
        SendTeleportMessage(new Vector3(100, 100, 100), new Vector3(0,0,0));
    }
    public void SendTeleportMessage(Vector3 position, Vector3 rotation)
    {
        var message = new OscMessage("/TeleportPlayer", position.x, position.y, position.z);
        oscSender.Send(message);
        Debug.Log("OSC Message Sent: " + position.x + ", " + position.y + ", " + position.z);
    }

    void OnDestroy()
    {
        oscSender.Close();
    }

    public void SubmitIP()
    {
        ipAddress = inputIp.text;
        ipPage.SetActive(false);
        teleportPage.SetActive(true);
        
    }
}
