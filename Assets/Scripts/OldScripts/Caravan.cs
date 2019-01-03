using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan : MonoBehaviour{

    public Caravan caravan;
    public string factionName;
    public string IPAddress;
    //DataTypes.ProtoPort destinationProtoPort { get; set; }


    public Caravan(string IPAddress, /*DataTypes.ProtoPort destinationProtoPort,*/ string name)
    {
        this.factionName = name;
        this.IPAddress = IPAddress;
        //this.destinationProtoPort = destinationProtoPort;
        
    }

    
}
