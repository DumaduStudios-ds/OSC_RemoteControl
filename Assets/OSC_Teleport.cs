using UnityEngine;
using SharpOSC;
using TMPro;
using System.Collections.Generic;

public class OSC_Teleport : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string ipAddress;
    public int port = 8000;
    private UDPSender oscSender;
    public TextMeshProUGUI inputIp;
    public GameObject ipPage, teleportPage, typeSelectionPage;
    [SerializeField] List<Villa> villas;
    int villaIndex = 0;

    void Start()
    {
        //Debug.Log("ip" + ipAddress);
        //oscSender = new UDPSender(ipAddress, port);
    }

    public void Teleport(int num)
    {
        SendTeleportMessage(villas[villaIndex].positions[num], villas[villaIndex].rotations[num]);
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
        Debug.Log("ip  " + ipAddress);
        try
        {
            oscSender = new UDPSender(ipAddress, port);            
        }
        catch
        {
            Debug.Log("connection error");
            return;
        }
        ipPage.SetActive(false);
        typeSelectionPage.SetActive(true);
    }

    public void SelectVilla(int index)
    {
        Debug.Log("sel  " + index);
        villaIndex = index;
        typeSelectionPage.SetActive(false);
        teleportPage.SetActive(true);
    }

}

[System.Serializable]
public class Villa
{
    public int index;
    public List<Vector3> positions;
    public List<Vector3> rotations;
}
