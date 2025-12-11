using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMoviment : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller;
    private Transform myCamera;
    private Animator animacao;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask colisaoLayer;

    [Header("Variables")] 
    public float velocidade = 5f;
    private bool isGround; 
    private float yForce;

    bool sentado = false;       // estado atual
    bool pertoDoBanco = false;  // detecta se há um banco por perto
    public Transform player;

    void Start()
    {
        controller = GetComponent<CharacterController>(); 
        myCamera = Camera.main.transform;
        animacao = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Pular();
        SentaLevanta();
        //Dance();
    }

    public void Mover()
    {
        //Debug.Log("Executando o movimento do personagem...");

        float horizontal = 0f; 
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal -= 1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal += 1f;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vertical -= 1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vertical += 1f;

        Vector3 movimentar = new Vector3(horizontal, 0, vertical); 

        movimentar = Vector3.ClampMagnitude(movimentar, 1f); 
        movimentar = myCamera.TransformDirection(movimentar); 
        movimentar.y = 0;

        controller.Move(movimentar * velocidade * Time.deltaTime);
        if (movimentar != Vector3.zero) 
        { 
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(movimentar), 
                Time.deltaTime * 10f
             );
        }

        animacao.SetBool("Mover", movimentar != Vector3.zero);

        isGround = Physics.CheckSphere(foot.position, 0.3f, colisaoLayer);
        animacao.SetBool("IsGround", isGround);
    }

    public void Pular()
    {
        //Debug.Log("Estou no chão?" + isChao);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGround) 
        {
            yForce = 4f; 
            animacao.SetTrigger("Jump"); 
        }

        if (yForce > -9.81f) 
        {
            yForce += -9 * Time.deltaTime; 
        }
        controller.Move(new Vector3(0, yForce, 0) * Time.deltaTime);
    }

    public void Dance()
    {
        if (Keyboard.current.pKey.isPressed && isGround)
        {
            animacao.SetTrigger("Dance");
            //Debug.Log("P pressionado");
        }
    }

    public void SentaLevanta()
    {
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            // Se já está sentado -> levantar
            if (sentado)
            {
                animacao.SetTrigger("Up");
                sentado = false;
                return;
            }

            // Se não está sentado -> só senta se estiver perto do banco
            if (pertoDoBanco)
            {
                animacao.SetTrigger("Sit");
                sentado = true;

                //Faz o player olhar para o eixo Z (0,0,0)
                player.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Banco"))
        {
            pertoDoBanco = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Banco"))
        {
            pertoDoBanco = false;
        }
    }

}
