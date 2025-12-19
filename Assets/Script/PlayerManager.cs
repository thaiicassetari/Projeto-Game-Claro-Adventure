using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float vidaJogador = 100f;
    public float vidaMaxima = 100f;
    public Image barraVida; 

    private bool podeSalvar = false;  
    private Vector3 ultimaPosicaoSalva;
    private float ultimaVidaSalva;
    private bool temDadosSalvos = false; 

    public GameObject AtivarTorre1;
    public GameObject AtivarTorre2;
    public GameObject AtivarTorre3;
    public int pedacosTotaisColetados = 0;

    void Start()
    {
        AtualizarBarraVida();
        CarregarDadosJogador();

        AtivarTorre1.SetActive(false);
        AtivarTorre2.SetActive(false);
        AtivarTorre3.SetActive(false);

        VerificarAtivacaoTorres(pedacosTotaisColetados);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AtualizarProgressoTorre(pedacosTotaisColetados);
        }
    }

    void Update()
    {
        AtualizarBarraVida();
        if (podeSalvar && Keyboard.current.bKey.wasPressedThisFrame)
        {
            SalvarDadosJogador();
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            LimparDadosJogador();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            Debug.Log("Colidiu com o ponto de salvamento! Pressione B para salvar ou C para limpar dados!");
            podeSalvar = true;
        }

        if (other.CompareTag("PedacoTorre"))
        {
            //Debug.Log("Pedaço da torre coletado!");
            pedacosTotaisColetados++;
            Destroy(other.gameObject);
            VerificarAtivacaoTorres(pedacosTotaisColetados);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AtualizarProgressoTorre(pedacosTotaisColetados);
            }
        }
    }
     
    private void VerificarAtivacaoTorres(int total)
    {
        if (total >= 4)
        {
            if (!AtivarTorre1.activeSelf)
            {
                AtivarTorre1.SetActive(true);
                //Debug.Log("Torre 1 ativada! (4/12)");
            }
        }
        if (total >= 8)
        {
            if (!AtivarTorre2.activeSelf)
            {
                AtivarTorre2.SetActive(true);
                //Debug.Log("Torre 2 ativada! (8/12)");
            }
        }
        if (total >= 11)
        {
            if (!AtivarTorre3.activeSelf)
            {
                AtivarTorre3.SetActive(true);
                //Debug.Log("Torre 3 ativada! (12/12) - Todas as torres ativadas!");
                
                //if (GameManager.Instance != null)
                //{
                //    GameManager.Instance.PlayPuzzleCompletionSFX();// Tocar o som de conclusão!
                //}
            }
        }
        if (total < 12)
        {
            //Debug.Log($"Progresso da Antena: {total} de 12 pedaços coletados.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            Debug.Log("Saiu do ponto de salvamento!");
            podeSalvar = false;
        }
    }

    void SalvarDadosJogador()
    {
        ultimaPosicaoSalva = transform.position;  
        ultimaVidaSalva = vidaJogador;

        PlayerPrefs.SetFloat("PosX", ultimaPosicaoSalva.x);  
        PlayerPrefs.SetFloat("PosY", ultimaPosicaoSalva.y);
        PlayerPrefs.SetFloat("PosZ", ultimaPosicaoSalva.z);
        PlayerPrefs.SetFloat("Vida", ultimaVidaSalva);

        PlayerPrefs.SetInt("PedacosTotais", pedacosTotaisColetados); 

        PlayerPrefs.SetInt("temDadosSalvos", 1);
        PlayerPrefs.Save();
        //Debug.Log("Dados salvos com sucesso!");
        podeSalvar = false;  

    }

    void LimparDadosJogador()
    {
        PlayerPrefs.DeleteKey("PosX"); 
        PlayerPrefs.DeleteKey("PosY");
        PlayerPrefs.DeleteKey("PosZ");
        PlayerPrefs.DeleteKey("Vida");
        PlayerPrefs.DeleteKey("PedacosTotais");  

        PlayerPrefs.DeleteKey("temDadosSalvos");

        ultimaPosicaoSalva = Vector3.zero;  
        ultimaVidaSalva = 0f;
        temDadosSalvos = false;
        pedacosTotaisColetados = 0;

        vidaJogador = vidaMaxima; 
        AtualizarBarraVida();

        //Debug.Log("Dados apagados com sucesso!");
    }

    void CarregarDadosJogador()
    {
        if (PlayerPrefs.GetInt("temDadosSalvos") == 1)  
        {
            float posX = PlayerPrefs.GetFloat("PosX");
            float posY = PlayerPrefs.GetFloat("PosY");
            float posZ = PlayerPrefs.GetFloat("PosZ");
            ultimaPosicaoSalva = new Vector3(posX, posY, posZ);
            ultimaVidaSalva = PlayerPrefs.GetFloat("Vida");
            transform.position = ultimaPosicaoSalva;  
            vidaJogador = ultimaVidaSalva;
            pedacosTotaisColetados = PlayerPrefs.GetInt("PedacosTotais", 0);

            temDadosSalvos = true;
            //Debug.Log("Dados carregados com sucesso!");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AtualizarProgressoTorre(pedacosTotaisColetados);
            }
            VerificarAtivacaoTorres(pedacosTotaisColetados);
            //Debug.Log("Dados carregados com sucesso! Pedaços: " + pedacosTotaisColetados);
        }
        else
        {
            //Debug.Log("Nenhum dado salvo encontrado.");
        }
    }

    void AtualizarBarraVida()
    {
        if (barraVida == null) return;

        float vidaNormalizada = Mathf.Clamp01(vidaJogador / vidaMaxima);
        barraVida.fillAmount = vidaNormalizada;

        if (vidaJogador <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void CarregarDano()
    {
        //vidaJogador -= npcDano.perderVida;
        AtualizarBarraVida();
    }
}
