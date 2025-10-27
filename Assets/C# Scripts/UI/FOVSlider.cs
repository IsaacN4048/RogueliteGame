using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine.UI;
using Player;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;

public class FOVSlider : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpController;
    [SerializeField] private Slider fovSlider;
    [SerializeField] private TextMeshProUGUI FOVText;

    private void Start()
    {
        fovSlider.value = fpController.defaultFOV;
        fovSlider.onValueChanged.AddListener(UpdateFOV);
        FOVText.text = $"{(float)fpController.defaultFOV}";
    }
    void UpdateFOV(float newValue)
    {
        fpController.defaultFOV = newValue;
        FOVText.text = $"{newValue}";
    }



}
