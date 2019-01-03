using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour {

    public enum Sender
    {
        Alice //"192.168.63.52"
       ,Bob  //"192.424.52.12"
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
    bool startMoving = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.name);
        startMoving = false;
    }

    public void playMovement()
    {
        startMoving = true;
    }

    private void Update()
    {
        if (startMoving)
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
    }
}
