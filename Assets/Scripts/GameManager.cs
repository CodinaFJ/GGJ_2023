using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField][Range(0,10)]
    private float gameSpeed;

    [SerializeField][Range(0.1f,1)]
    private float timesRandomizer;

    private void Awake() {
        instance = this;
    }

    /****************************************************************************************
    GETTERS
    ****************************************************************************************/
    public float    GetGameSpeed() => gameSpeed;
    public float    GetTimesRandomizer() => timesRandomizer;
}
