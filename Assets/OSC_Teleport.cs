using UnityEngine;
using SharpOSC;
using TMPro;
public class OSC_Teleport : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string ipAddress;
    public int port = 8000;
    private UDPSender oscSender;
    public TextMeshProUGUI inputIp;
    public GameObject ipPage;
    public GameObject teleportPage;
    [SerializeField] Vector3[] positions, rotations;
    int heightDisaplcement;

    void Start()
    {
        //Debug.Log("ip" + ipAddress);
        //oscSender = new UDPSender(ipAddress, port);
    }

    public void Teleport(int num)
    {
        SendTeleportMessage(positions[num], rotations[num]);
    }

    public void SendTeleportMessage(Vector3 position, Vector3 rotation)
    {
        var message = new OscMessage("/TeleportPlayer", position.x, position.y, position.z, rotation.x, rotation.y, rotation.z);
        oscSender.Send(message);
        Debug.Log("OSC Message Sent: " + position.x + ", " + position.y + ", " + position.z);
    }

    void OnDestroy()
    {
        oscSender.Close();
    }

    public void SubmitIP()
    {
        ipAddress = inputIp.text.Trim().Replace("\u200B", "").Replace("\u00A0", "");
        ipPage.SetActive(false);
        teleportPage.SetActive(true);
        Debug.Log("ip  " + ipAddress);
        oscSender = new UDPSender(ipAddress, port);
    }

}
