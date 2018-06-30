using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    [SerializeField]
	private Text score;

    [SerializeField]
	private Text currentNumbers;

    [SerializeField]
	private InputField playerGuess;

    [SerializeField]
	private Button guessButton;

    [SerializeField]
	private Text aiPrediction;

    [SerializeField]
	private Text networkState;

    [SerializeField]
	private int maxPredictions;

    private List<int> numbers;
    private int currentPrediction;
    private int currentScore;
    private int currentPredictions;

    private const int NUMBER_RANGE = 5;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if(!IsGameOver() && guessButton.enabled && /*playerPrediction.isFocused && */playerGuess.text != "" && Input.GetKey(KeyCode.Return)) {
            Predict();
        }
    }

    public void Reset() {
        numbers = new List<int>();

        for (int i = 0; i < NUMBER_RANGE; i++)
        {
            int range = i * NUMBER_RANGE;
            numbers.Add(Random.Range(range, range + NUMBER_RANGE) + 1);
        }

        currentScore = 0;
        currentPredictions = 0;

        RetrainNetwork();

        RefreshGUI();

        ResetInputField();

        aiPrediction.text = "AI predicted Value: -------";
    }

    private void RefreshGUI()
    {
        string numersString = "";

        foreach (var item in numbers)
        {
            numersString += item.ToString() + ",";
        }

        if(numbers.Count < NUMBER_RANGE + 2)
        {
            numersString += string.Format("Hint: {0} - {1}", 1 + (numbers.Count * NUMBER_RANGE), (numbers.Count + 1) * NUMBER_RANGE);
        }
        else
        {
            numersString = numersString.Substring(0, numersString.Length - 1);
        }

        currentNumbers.text = numersString;
        score.text = "Score: " + currentScore;

        if (IsGameOver())
        {
            guessButton.gameObject.SetActive(false);
            playerGuess.gameObject.SetActive(false);
        }
        else
        {
            guessButton.gameObject.SetActive(true);
            playerGuess.gameObject.SetActive(true);
        }
    }

    private bool IsGameOver()
    {
        return currentPredictions >= maxPredictions;
    }

    private void ResetInputField()
    {
        playerGuess.text = "";
        playerGuess.Select();
        playerGuess.ActivateInputField();
    }

    public void Predict() {
        Debug.Log("Predict");
        int newPrediction;
        string playerPredictionValue = playerGuess.text;

        if(int.TryParse(playerPredictionValue, out newPrediction))
        {
            aiPrediction.text = "AI predicted Value: " + currentPrediction;

            numbers.Add(currentPrediction);

            //The score will be the distance between the prediction from the AI and the player
            currentScore += NUMBER_RANGE - Mathf.Clamp(Mathf.Abs(newPrediction - currentPrediction), 0, NUMBER_RANGE);

            currentPredictions++;

            RefreshGUI();

            ResetInputField();

            RetrainNetwork();
        }
    }

    private void RetrainNetwork()
    {
        networkState.text = "Network is training!";
        guessButton.enabled = false;

        float[] xValues = new float[numbers.Count];
        float[] yValues = new float[numbers.Count];

        for (int i = 0; i < numbers.Count; i++)
        {
            xValues[i] = i + 1;
            yValues[i] = numbers[i];
        }

        JavascriptInterfaceController.StartTraining(xValues, yValues, numbers.Count);
    }

    public void TrainingDone() {
        networkState.text = "Network ready!";
        guessButton.enabled = true;

        currentPrediction = JavascriptInterfaceController.GetPrediction(numbers.Count + 1);
    }
}
