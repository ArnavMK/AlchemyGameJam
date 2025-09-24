using UnityEngine;
using UnityEngine.UI;

public class TimebarGame : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject timebarObject; 
    [SerializeField] private Slider timebar;           
    [SerializeField] private RectTransform targetZone; 

    [Header("Settings")]
    [SerializeField] private float speed = 1.0f;   
    [SerializeField] private float targetMin = 0.4f;
    [SerializeField] private float targetMax = 0.6f;

    [Header("Audio")]
    [SerializeField] private AudioSource failMineSound;
    [SerializeField] private AudioSource successMineSound;
    [SerializeField] private AudioSource leftBlipSound;
    [SerializeField] private AudioSource rightBlipSound;

    private bool movingRight = true;
    private bool gameActive = false;

    public static TimebarGame instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timebarObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartMinigame();
        }

        if (!gameActive) return;

        DriveTimebar();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CheckSuccess();
        }
    }

    public void StartMinigame()
    {
        if (!DialogueSystem.instance.isDialogueOngoing 
            && MySceneManager.instance.GetCurrentSceneState() == SceneState.Mine) {
            timebarObject.SetActive(true);
            ResetTimebar();
            gameActive = true;
        }
        
    }

    private void DriveTimebar()
    {
        if (movingRight)
            timebar.value += speed * Time.deltaTime;
        else
            timebar.value -= speed * Time.deltaTime;

        if (timebar.value >= 1f)
        {
            movingRight = false;
            leftBlipSound.Play();
            speed = Random.Range(1f, 2f); // randomize speed when switching
        }
        else if (timebar.value <= 0f)
        {
            movingRight = true;
            rightBlipSound.Play();
            speed = Random.Range(1f, 2f); // randomize speed when switching
        }
    }

    private void CheckSuccess()
    {
        float value = timebar.value;

        if (value >= targetMin && value <= targetMax)
        {
            if (MineManager.instance != null)
            {
                string reward = MineManager.instance.GetRandomMineableResource();
                Debug.Log("You mined: " + reward);
                successMineSound.Play();
                Inventory.instance.AddItem(reward, 1);
            }

            gameActive = false;
            timebarObject.SetActive(false);
        }
        else
        {
            failMineSound.Play();
        }
    }

    private void ResetTimebar()
    {
        timebar.value = 0f;
        movingRight = true;
        speed = Random.Range(1f, 2f); // also randomize on reset
    }
}
