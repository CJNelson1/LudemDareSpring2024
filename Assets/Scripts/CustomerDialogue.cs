using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDialogue : MonoBehaviour
{

    public int whichCustomer;

    public string activeDialogue;

    public int activeLine;

    public enum string customer1DialogueLines 
    {
        1 = "customer1line1";
        2 = "customer1line2";
        3 = "customer1line3";
        4 = "customer1line4";
        5 = "customer1line5";
        6 = "customer1line6";
    }

    public enum string customer2DialogueLines 
    {
        1 = "customer2line1";
        2 = "customer2line2";
        3 = "customer2line3";
        4 = "customer2line4";
        5 = "customer2line5";
        6 = "customer2line6";
    }

    public enum string customer3DialogueLines 
    {
        1 = "customer3line1";
        2 = "customer3line1";
        3 = "customer3line1";
        4 = "customer3line1";
        5 = "customer3line1";
        6 = "customer3line1";
    }

     public enum string customer4DialogueLines 
    {
        1 = "customer4line1";
        2 = "customer4line2";
        3 = "customer4line3";
        4 = "customer4line4";
        5 = "customer4line5";
        6 = "customer4line6";
    }

    public enum string customer5DialogueLines 
    {
        1 = "customer5line1";
        2 = "customer5line2";
        3 = "customer5line3";
        4 = "customer5line4";
        5 = "customer5line5";
        6 = "customer5line6";
    }

    public void CustomerDialogue
    {
        activeDialogue = string.Empty;
        activeLine = 1;
    }

    public string advanceDialogue
    {
        activeDialogue = string.Empty;
        activeLine++;

        switch (whichCustomer)
        {
            1:
                activeDialogue = Customer1DialogueLines.activeLine;
            2:
                activeDialogue = Customer2DialogueLines.activeLine;
            3:
                activeDialogue = Customer3DialogueLines.activeLine;
            4:
                activeDialogue = Customer4DialogueLines.activeLine;
            5:
                activeDialogue = Customer5DialogueLines.activeLine;
            default:
                activeDialogue = "oops";
        }
    }
}