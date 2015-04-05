using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*-----------------------------------------------------

    Отвечает за хранение всей информации по текущей битве:
        *количество сторон
        *количество и тип юнитов у каждой из сторон
        *контроль ходов
     
-----------------------------------------------------*/
public class BattleDataController : MonoBehaviour
{
    public List<Player> Players;
    public int currentRound = 0;  

    private static BattleDataController _instance;

    public static BattleDataController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BattleDataController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
