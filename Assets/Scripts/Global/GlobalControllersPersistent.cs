using UnityEngine;
using System.Collections;

public class GlobalControllersPersistent : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
       DontDestroyOnLoad(this.gameObject);
    }

}