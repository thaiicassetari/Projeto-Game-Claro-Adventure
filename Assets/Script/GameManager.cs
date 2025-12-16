using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject img;

    public static GameManager Instance;
    public int totalCaixas = 6;  
    private int caixasCorretas = 0;
    public UnityEngine.UI.Text boxCountText;  

    public GameObject AtivarTorre1;
    public GameObject AtivarTorre2;
    public GameObject AtivarTorre3;
    public int ToatalPedacos = 12;
    public UnityEngine.UI.Text pedacosCountText;

    [Header("Áudio de Puzzles")] // 1. O AudioSource que está no objeto SFXPlayer
    [SerializeField] private AudioSource sfxPlayerSource;
    [SerializeField] private AudioClip puzzleCompletionClip; // 2. O clip de som específico para a notificação de conclusão


    void Awake()
    { 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (Time.timeScale == 0f)
        {
            Resume();
        }

        UpdateBoxCountUI();
    }
     
    void Update()
    {
        ReturnGame();
        CallPauseResume();
    }

    public void ReturnGame()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void CallPauseResume()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        img.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        img.SetActive(false);
    }

    private void UpdateBoxCountUI()
    {
        // Atualiza a UI para mostrar "2 / 4", "3 / 4", etc.
        if (boxCountText != null)
        {
            boxCountText.text = caixasCorretas.ToString() + " / " + totalCaixas.ToString();
        }
    }
    public void BoxPlacedCorrectly()
    {
        caixasCorretas++;
        UpdateBoxCountUI();

        if (caixasCorretas >= totalCaixas)
        {
            PuzzleComplete();
        }
    }

    public void BoxRemoved()
    {
        caixasCorretas--;
        UpdateBoxCountUI();
    }

    private void PuzzleComplete()
    {
        Debug.Log("PUZZLE RESOLVIDO! VITÓRIA!");
        //PlayPuzzleCompletionSFX();
        // Aqui você pode adicionar mais lógica para quando o puzzle for concluído
    }
    public void AtualizarProgressoTorre(int totalColetado)
    {
        UpdatePedacosCountUI(totalColetado);
    }

    private void UpdatePedacosCountUI(int totalColetado)
    {
        if (pedacosCountText != null)
        {
            pedacosCountText.text = totalColetado.ToString() + " / " + ToatalPedacos.ToString();
        }
    }

    //public void PlayPuzzleCompletionSFX()
    //{
    //    if (sfxPlayerSource != null && puzzleCompletionClip != null)
    //    {
    //        // Usa PlayOneShot para tocar o clip sem interromper outros sons
    //        // que o sfxPlayerSource possa estar tocando (embora improvável neste caso).
    //        sfxPlayerSource.PlayOneShot(puzzleCompletionClip);
    //        Debug.Log("Notificação de puzzle concluído tocada!");
    //    }
    //    else
    //    {
    //        Debug.LogError("SFX Player Source ou Puzzle Completion Clip não estão atribuídos no GameManager!");
    //    }
    //}

}
