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
    [Header("Water Can")]
    [SerializeField][Range(0, 10)]
    private float waterCanCooldown;
    [SerializeField][Range(1,10)]
    private int waterCanCapacity;

    private int waterCanLevel;
    private bool waterCanAvailable;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        waterCanLevel = waterCanCapacity;
        waterCanAvailable = true;
    }

    /****************************************************************************************
    WATERING CAN CONTROL
    ****************************************************************************************/  

    public void UseWaterCan()
    {
        waterCanLevel--;
        if (waterCanLevel == 0)
        {
            waterCanAvailable = false;
            StartCoroutine(WaterCanCooldown());
        }
    }

    private IEnumerator WaterCanCooldown()
    {
        float elapsedTime = 0;
        while (elapsedTime < waterCanCooldown)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        waterCanAvailable = true;
        waterCanLevel = waterCanCapacity;
    }

    /****************************************************************************************
    GETTERS
    ****************************************************************************************/
    public float    GetGameSpeed() => gameSpeed;
    public float    GetTimesRandomizer() => timesRandomizer;
    public bool     GetWaterCanAvailable() => waterCanAvailable;
}
