using UnityEngine;
using System.Collections;

public class GlobalControllersPersistent : MonoBehaviour {
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}

}