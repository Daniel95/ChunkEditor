using UnityEngine;
using UnityEngine.UI;
public class SendSliderValue : MonoBehaviour {

    [SerializeField]
    private GridYValue gridYValue;

    [SerializeField]
    private ChunkEditor chunkEditor;

    private Slider slider;

    private int lastSliderValue;

    void Awake() {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        SendValue();
    }

    public void SendValue()
    {
        gridYValue.ChangeNumber((int)slider.value);
        ChunkHolder.CurrentYLength = (int)slider.value;
        chunkEditor.EditChunkHeight();
    }
}
