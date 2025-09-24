public class StartReactor : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Button[] tiles;          // 9 buttons in a 3x3 grid
    public Text statusText;         // UI text for messages
    public Button startButton;      // Start button

    [Header("Settings")]
    public int roundsToWin = 5;     // How many rounds to complete
    public float flashTime = 0.45f; // How long a tile lights up
    public float pauseTime = 0.15f; // Pause between flashes
    public Color normalColor = Color.gray;
    public Color flashColor = Color.cyan;

    // Internal
    private List<int> sequence = new List<int>();
    private int currentRound = 0;
    private int playerIndex = 0;
    private bool isShowing = false;
    private bool isRunning = false;

    void Start()
    {
        // Setup buttons
        for (int i = 0; i < tiles.Length; i++)
        {
            int idx = i;
            tiles[i].onClick.AddListener(() => OnTilePressed(idx));
            tiles[i].GetComponent<Image>().color = normalColor;
        }

        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        UpdateStatus("Press Start to begin");
    }

    public void StartGame()
    {
        if (isShowing) return;
        sequence.Clear();
        currentRound = 0;
        isRunning = true;
        NextRound();
    }

    void NextRound()
    {
        currentRound++;
        sequence.Add(Random.Range(0, tiles.Length));
        playerIndex = 0;
        UpdateStatus($"Round {currentRound}/{roundsToWin}");
        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        isShowing = true;
        yield return new WaitForSeconds(0.5f);

        foreach (int idx in sequence)
        {
            FlashTile(idx);
            yield return new WaitForSeconds(flashTime + pauseTime);
        }

        isShowing = false;
        UpdateStatus("Your turn");
    }

    void OnTilePressed(int idx)
    {
        if (!isRunning || isShowing) return;

        FlashTile(idx);

        if (idx == sequence[playerIndex])
        {
            playerIndex++;
            if (playerIndex >= sequence.Count)
            {
                if (currentRound >= roundsToWin)
                {
                    UpdateStatus("✅ Reactor Started! Task Complete.");
                    isRunning = false;
                }
                else
                {
                    StartCoroutine(DelayNextRound());
                }
            }
        }
        else
        {
            UpdateStatus("❌ Wrong! Restarting...");
            StartCoroutine(ResetGame());
        }
    }

    void FlashTile(int idx)
    {
        StartCoroutine(FlashCoroutine(tiles[idx].GetComponent<Image>()));
    }

    IEnumerator FlashCoroutine(Image img)
    {
        img.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        img.color = normalColor;
    }

    IEnumerator DelayNextRound()
    {
        yield return new WaitForSeconds(1f);
        NextRound();
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1f);
        StartGame();
    }

    void UpdateStatus(string msg)
    {
        if (statusText != null)
            statusText.text = msg;
        else
            Debug.Log(msg);
    }
}
