using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // garantir que só haja um AudioManager
    public static AudioManager Instance;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sliderSFX;

    private const string MasterKey = "MasterVolume";
    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";

    

    private void Awake()
    {
        // 1. Lógica para garantir que só haja UMA instância deste objeto
        if (Instance == null)
        {
            Instance = this;
            // 2. Comanda a Unity para manter este objeto vivo entre as cenas
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já existe uma instância, destrói o novo objeto que tentou ser criado
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Carrega as configurações ao iniciar o jogo
        if (Instance == this)
        {
            LoadVolumeSettings();

            // É importante adicionar os Listeners APÓS carregar as configs
            // Caso contrário, ele tentará alterar o mixer antes de carregar
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sliderSFX.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    // --- Funções de Alteração de Volume (Chamadas pelos Sliders) ---

    public void SetMasterVolume(float volume)
    {
        // Converte o valor linear (0 a 1) do Slider para o valor logarítmico (dB)
        // O valor mínimo é -80dB (silêncio) e o máximo é 0dB (volume máximo)
        float db = Mathf.Log10(volume) * 20;
        mixer.SetFloat("MasterVolume", db);
        PlayerPrefs.SetFloat(MasterKey, volume); // Salva o valor
    }

    public void SetMusicVolume(float volume)
    {
        float db = Mathf.Log10(volume) * 20;
        mixer.SetFloat("MusicVolume", db);
        PlayerPrefs.SetFloat(MusicKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        float db = Mathf.Log10(volume) * 20;
        mixer.SetFloat("SFXVolume", db);
        PlayerPrefs.SetFloat(SFXKey, volume);
    }

    // --- Função para Carregar Configurações Salvas ---

    private void LoadVolumeSettings()
    {
        // Pega o valor salvo ou usa 1 (volume máximo) se não houver um salvo
        float masterVol = PlayerPrefs.GetFloat(MasterKey, 1f);
        float musicVol = PlayerPrefs.GetFloat(MusicKey, 1f);
        float sfxVol = PlayerPrefs.GetFloat(SFXKey, 1f);

        // Define o valor dos Sliders
        masterSlider.value = masterVol;
        musicSlider.value = musicVol;
        sliderSFX.value = sfxVol;

        // Atualiza o volume no Mixer com os valores carregados
        // (As chamadas abaixo também atualizam o PlayerPrefs, mas é importante para o Mixer)
        SetMasterVolume(masterVol);
        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);

        // Garante que os dados salvos serão gravados no disco
        PlayerPrefs.Save();
    }
}