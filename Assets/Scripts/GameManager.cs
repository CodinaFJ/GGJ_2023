using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField][Range(0,10)]
    private float gameSpeed;
    [SerializeField][Range(0,10)]
    private float topSpeed;
    [SerializeField]
    private float accelerationSpeed;
    [SerializeField][Range(0.1f,1)]
    private float timesRandomizer;
    [Header("Water Can")]
    [SerializeField][Range(0, 10)]
    private float waterCanCooldown;
    [SerializeField][Range(1,10)]
    private int waterCanCapacity;
    [SerializeField]
    Canvas GameOverCanvas;
    [SerializeField]
    Canvas StartCanvas;
    [SerializeField]
    GameObject plants;
    [SerializeField]
    GameObject waterCan;
    [SerializeField]
    GameObject fertilizer;

    private int waterCanLevel;
    private bool waterCanAvailable;

    private void Awake() {
        instance = this;
        Time.timeScale = 0;
    }

    private void Start() {
        StartCanvas.gameObject.SetActive(true);
        waterCanLevel = waterCanCapacity;
        waterCanAvailable = true;
        GameOverCanvas.gameObject.SetActive(false);
        plants.SetActive(true);
        waterCan.SetActive(true);
        fertilizer.SetActive(true);
    }

    private void Update() {
        if (gameSpeed < topSpeed)
            UpdateGameSpeed();
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
        WateringStation.instance.StartWateringCooldown();
        while (elapsedTime < waterCanCooldown)
        {
            WateringStation.instance.UpdateWateringCooldown(elapsedTime / waterCanCooldown);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        waterCanAvailable = true;
        waterCanLevel = waterCanCapacity;
        WateringStation.instance.EndWateringCooldown();
    }

    public void Die()
    {
        Time.timeScale = 0;
        GameOverCanvas.gameObject.SetActive(true);
    }

    public void Restart()
    {
        plants.SetActive(false);
        waterCan.SetActive(false);
        fertilizer.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        StartCanvas.gameObject.SetActive(false);
    }

    private void UpdateGameSpeed()
    {
        gameSpeed += (accelerationSpeed * Time.deltaTime);
    }

    /****************************************************************************************
    GETTERS
    ****************************************************************************************/
    public float    GetGameSpeed() => gameSpeed;
    public float    GetTimesRandomizer() => timesRandomizer;
    public bool     GetWaterCanAvailable() => waterCanAvailable;
}
