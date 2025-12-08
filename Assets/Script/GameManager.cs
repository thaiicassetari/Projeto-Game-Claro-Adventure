using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro; // para TextMeshPro

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject img;

    public static GameManager Instance;
    public int totalCaixas = 4; // Defina o total de caixas no Inspector
    private int caixasCorretas = 0;
    public UnityEngine.UI.Text boxCountText; // Ou TMPro.TextMeshProUGUI

    void Awake()
    {
        // Garante que só exista uma instância deste Game Manager
        if (Instance == null)
        {
            Instance = this;
            // Opcional: Se este objeto for o GameManager principal, pode mantê-lo entre cenas:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já existe uma instância, destrói este novo objeto para evitar duplicatas.
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

    // Update is called once per frame
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
        // Ações de vitória: liberar porta, próxima cena, etc.
    }
}
