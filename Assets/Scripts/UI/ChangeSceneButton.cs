using UnityEngine;
using System.Collections;
using EnumSpace;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{

    private UIController _uiController;
    private ScenesController _scenesController;

    public ScenesEnum ActionToSet;

    private Button _button;

    void Start()
    {
        _uiController = UIController.Instance;
        _scenesController = ScenesController.Instance;
        _button = this.GetComponent<Button>();

        _button.onClick.AddListener(() => { _uiController.SetButtonAction(ActionToSet); });    
    }

}
