using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    // Arraste os Sliders desta cena (Cena_Configuracao_Audio) para o Inspetor
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sliderSFX;

    void Start()
    {
        // Verifica se o AudioManager persistente existe
        if (AudioManager.Instance != null)
        {
            // Opcional: Você pode pegar os valores salvos e definir nos Sliders aqui 
            // ou deixar que o AudioManager faça isso, mas você precisa garantir que 
            // o AudioManager tenha as referências aos Sliders.

            // O MODO MAIS SIMPLES: 
            // Você deve garantir que os Sliders na sua Cena_Configuracao_Audio 
            // chamem as funções do seu AudioManager.

            // Exemplo de como conectar (feito no Inspetor):
            // 1. Selecione masterSlider na Hierarchy.
            // 2. No componente Slider, vá em OnValueChanged (Float).
            // 3. Arraste o objeto _AudioManager persistente para o campo de objeto.
            // 4. Selecione a função: AudioManager -> SetMasterVolume.
        }
    }
}