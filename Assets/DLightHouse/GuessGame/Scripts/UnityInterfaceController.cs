using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class UnityInterfaceController : MonoBehaviour {

    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void TrainingDone() {
        gameController.TrainingDone();
    }
}
