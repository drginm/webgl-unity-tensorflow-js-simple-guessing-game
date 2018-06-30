using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;

public class JavascriptInterfaceController : MonoBehaviour {

#if UNITY_EDITOR
    private static UnityInterfaceController unityInterfaceController;
#endif

    void Start()
    {
#if UNITY_EDITOR
        unityInterfaceController = gameObject.GetComponent<UnityInterfaceController>();
#endif
        InitializeJSInterface();
    }

#if UNITY_EDITOR
    public static void InitializeJSInterface() {
        Debug.Log("Called InitializeJSInterface");
    }
#else
    [DllImport("__Internal")]
    public static extern void InitializeJSInterface();
#endif

#if UNITY_EDITOR
    public static void StartTraining(float[] xValues, float[] yValues, int size) {
        Debug.Log("Called StartTraining");

        unityInterfaceController.TrainingDone();
    }
#else
    [DllImport("__Internal")]
    public static extern void StartTraining(float[] xValues, float[] yValues, int size);
#endif

#if UNITY_EDITOR
    public static int GetPrediction(int valueToPredict) {
        Debug.Log("Called GetPrediction");

        return 1;
    }
#else
    [DllImport("__Internal")]
    public static extern int GetPrediction(int valueToPredict);
#endif
}
