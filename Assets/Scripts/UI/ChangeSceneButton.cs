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
        _uiController = UIController.instance;
        _scenesController = ScenesController.instance;
        _button = this.GetComponent<Button>();

        _button.onClick.AddListener(() => { _uiController.SetButtonAction(ActionToSet); });    
    }

}
