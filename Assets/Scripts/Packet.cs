using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour {

    public enum Sender
    {
        Alice //"192.168.63.52"
       ,Bob  //"192.424.52.12"
       ,Charlie//""192.11.76.5"
    }
    public enum Port
    {
        TCP, UDP, any
    }
    public enum DestinationPort
    {
        HTTP = 80, SSH = 22, any = 0
    }
    public enum SourcePort
    {
        HTTP = 80, SSH = 22, any = 0
    }

    public Sender sender;
    public Port port;
    public DestinationPort destinationPort;
    public SourcePort sourcePort;
    public string sourceIP;
    public string destinationIP;

    bool collided = false;
    bool startMoving1 = false;
    bool startMoving2 = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.name);
        startMoving1 = false;
    }

    public void playMovement()
    {
        startMoving1 = true;
    }

    private void Update()
    {
        if (startMoving1)
        {
            if (sender == Sender.Bob)
            {
                gameObject.transform.position += new Vector3(-0.1f, 0, 0);
            }
            else if (sender == Sender.Alice)
            {
                gameObject.transform.position += new Vector3(0.1f, 0, 0);
            }

        }
        if (startMoving2)
        {
            if (sender == Sender.Bob)
            {

            }
        }
    }
}
