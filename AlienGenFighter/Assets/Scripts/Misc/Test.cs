using System;
using Assets.Scripts.GUI;
using UnityEngine;

public class Test : MonoBehaviour {
    const string Description = "bla bla bla bla bla bla bla bla bla bla bla bla bla" +
                               " bla bla bla bla bla bla bla bla bla bla bla bla bla " +
                               " bla bla bla bla bla bla bla bla bla bla bla bla bla " +
                               " bla bla bla bla bla bla bla bla bla bla bla bla bla ";
    public void test() {
        GameEventMessage.AddEventMessage(EIconEventMessage.NotImplemented, "NotImplemented", Description, DateTime.Now);
    }
}
