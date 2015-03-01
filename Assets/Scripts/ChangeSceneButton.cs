using UnityEngine;
using System.Collections;
using EnumSpace;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{

    private GUIController _GUIController;
    private ScenesController _scenesController;

    public ScenesEnum ActionToSet;

    private Button _button;

    void Start()
    {
        _GUIController = GUIController.instance;
        _scenesController = ScenesController.instance;
        _button = this.GetComponent<Button>();

        _button.onClick.AddListener(() => { _GUIController.SetButtonAction(ActionToSet); });    
    }

}
