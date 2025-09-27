using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SequenceManager : MonoBehaviour
{
    [Header("UI References")]
    public List<Button> gridButtons;         // All grid buttons
    public GameObject failPanel;             // Assign Fail Panel
    public GameObject winPanel;              // Assign Win Panel
    public Button startButton;               // Assign Start button
    public Button restartButton;             // Assign Restart button (child of panels)
    public GameObject gameUI;                // Parent for grid + start + text
    public TMP_Text roundsText;                  // "Rounds Left" text

    private List<Button> sequence = new List<Button>();
    private int currentIndex = 0;
    private bool gameActive = false;

    private int round = 0;       // Current round
    private int maxRounds = 5;   // Total rounds

    void Start()
    {
        failPanel.SetActive(false);
        winPanel.SetActive(false);

        startButton.onClick.AddListener(StartSequence);
        restartButton.onClick.AddListener(RestartGame);

        if (roundsText != null)
            roundsText.text = "Rounds Left: " + maxRounds;
    }

    void StartSequence()
    {
        round = 1;
        SetupRound();
    }

    void SetupRound()
    {
        sequence.Clear();
        currentIndex = 0;
        failPanel.SetActive(false);
        winPanel.SetActive(false);
        gameUI.SetActive(true);
        gameActive = false;

        // Pick sequence length for this round
        int length = GetSequenceLength(round);

        // Randomize sequence (no repeats)
        List<Button> temp = new List<Button>(gridButtons);
        for (int i = 0; i < length; i++)
        {
            int randIndex = Random.Range(0, temp.Count);
            sequence.Add(temp[randIndex]);
            temp.RemoveAt(randIndex);
        }

        // Show sequence
        StartCoroutine(ShowSequence());

        // Update text (counting down)
        if (roundsText != null)
            roundsText.text = "Rounds Left: " + (maxRounds - round);
    }

    int GetSequenceLength(int round)
    {
        switch (round)
        {
            case 1: return 1;
            case 2: return 3;
            case 3: return 5;
            case 4: return 6;
            case 5: return 6;
            default: return 6;
        }
    }

    IEnumerator ShowSequence()
    {
        // Disable interaction
        foreach (var btn in gridButtons)
            btn.interactable = false;

        foreach (var btn in sequence)
        {
            btn.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            btn.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        // Enable player input
        foreach (var btn in gridButtons)
        {
            btn.interactable = true;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => ButtonClicked(btn));
        }

        gameActive = true;
    }

    void ButtonClicked(Button clicked)
    {
        if (!gameActive) return;

        if (clicked == sequence[currentIndex])
        {
            currentIndex++;

            if (currentIndex >= sequence.Count)
            {
                gameActive = false;

                if (round < maxRounds)
                {
                    round++;
                    Invoke(nameof(SetupRound), 1f);
                }
                else
                {
                    WinGame();
                }
            }
        }
        else
        {
            FailGame();
        }
    }

    void FailGame()
    {
        Debug.Log("Wrong Button! Game Over!");
        gameActive = false;
        gameUI.SetActive(false);
        failPanel.SetActive(true);
    }

    void WinGame()
    {
        Debug.Log("Player WON the game!");
        gameActive = false;
        gameUI.SetActive(false);
        winPanel.SetActive(true);

        if (roundsText != null)
            roundsText.text = "Rounds Left: 0";
    }

    void RestartGame()
    {
        round = 0; // reset completely
        StartSequence();
    }
}