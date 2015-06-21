using EnumSpace;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{

    private UIController _uiController;

    public ScenesEnum ActionToSet;

    private Button _button;

    void Start()
    {
        _uiController = UIController.Instance;
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => { _uiController.SetButtonAction(ActionToSet); });    
    }

}
