using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour {
    Label dousedLabel;
    Button repositionButton;

    bool reposition = false;

    void OnEnable() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        dousedLabel = root.Q<Label>();
        repositionButton = root.Q<Button>();

        repositionButton.clicked += OnRepositionButton;
    }

    void OnRepositionButton() { reposition = true; }

    public bool ShouldReposition() {
        var temp = reposition;
        reposition = false;
        return temp;
    }

    public void SetNumFiresDoused(int numFiresDoused) { dousedLabel.text = $"Number of fires doused: {numFiresDoused}"; }
}
