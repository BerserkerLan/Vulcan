using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTypes {

    public enum ProtoPort
    {
        HTTP = 80,
        SMTP = 25,
        DNS = 53,
        FTP = 21,
        SSH = 22
    }
  
    public static readonly string AMAZON_IP_ADDRESS = "72.21.215.90";
    public static readonly string FACEBOOK_IP_ADDRESS = "90.238";
    public static readonly string ITALY_IP_ADDRESS = "90.239";


    public static readonly int INPUT = 1;
    public static readonly int OUTPUT = 2;
    public static readonly int ACCEPT = 3;
    public static readonly int DROP = 4;

    public class FirewallRule
    {
        int inputOrOutput;
        int acceptOrDrop;
        string factionIP;
        ProtoPort protoPort;

        public FirewallRule(int inputOrOutput, int acceptOrDrop, string factionIP, ProtoPort protoPort)
        {
            this.inputOrOutput = inputOrOutput;
            this.acceptOrDrop = acceptOrDrop;
            this.factionIP = factionIP;
            this.protoPort = protoPort;
        }

        public int getInputOrOutput()
        {
            return inputOrOutput;
        }
        public int getAcceptOrDrop()
        {
            return acceptOrDrop;
        }
        public string getFactionIP()
        {
            return factionIP;
        }
        public ProtoPort getProtoPort()
        {
            return protoPort;
        }
        public string getADAsString()
        {
            if (acceptOrDrop == ACCEPT)
            {
                return "ACCEPT";
            }
            else
            {
                return "DROP";
            }
        }

       public override string ToString()
        {
            string tempInOrOut;
            if (inputOrOutput==INPUT)
            {
                tempInOrOut = "INPUT";
            }
            else
            {
                tempInOrOut = "OUTPUT";
            }
            string tempAcceptOrDrop;
            if (acceptOrDrop == ACCEPT)
            {
                tempAcceptOrDrop = "ACCEPT";
            }
            else
            {
                tempAcceptOrDrop = "DROP";
            }
            return tempInOrOut + " | " + tempAcceptOrDrop + " | " + factionIP + " | " + (int) protoPort;
        }


    }
}

