using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public sealed class SaveManager : MonoBehaviour
{
    [SerializeField] private string SaveDir;
    public static SaveManager _instance;
    private static List<Car> _defaultCarList;
    private static List<Map> _defaultMapList;

    public float SavedMusicVolume { get; private set; }
    public float SavedSFXVolume { get; private set; }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            Debug.Log("Instance set!");
        }
        DontDestroyOnLoad(gameObject);
        InitializeDefaultCarList();
        InitializeDefaultMapList();
    }

    private void InitializeDefaultCarList()
    {
        _defaultCarList = new List<Car>();
        _defaultCarList.Add(new Kopeyka());
        _defaultCarList.Add(new Skyline());
        _defaultCarList.Add(new Dodge());
        _defaultCarList.Add(new Golf());
        _defaultCarList.Add(new Lambo());
        _defaultCarList.Add(new Lancer());
        _defaultCarList.Add(new Pickup());
        _defaultCarList.Add(new Supra());
    }
    private void InitializeDefaultMapList()
    {
        _defaultMapList = new List<Map>();
        _defaultMapList.Add(new Holes());
        _defaultMapList.Add(new Forest());
    }

    public void MusicVolumeChange(float currentVal)
    {
        SavedMusicVolume = currentVal;
    }
    public void SFXVolumeChange(float currentVal)
    {
        SavedSFXVolume = currentVal;
    }


    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {

            Debug.Log(PlayerPrefs.GetFloat("MusicVolume"));
            SavedMusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            SavedMusicVolume = 100;
            PlayerPrefs.SetFloat("MusicVolume", 100);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SavedSFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            Debug.Log("New Key Created");
            SavedSFXVolume = 100;
            PlayerPrefs.SetFloat("SFXVolume", 100);
        }
    }

    void OnApplicationQuit()
    {
        SaveInstance();
    }

    private void SaveInstance()
    {
        PlayerPrefs.SetFloat("MusicVolume", SavedMusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", SavedSFXVolume);
    }

    [Serializable]
    public class SaveData
    {
        public int SelectedCarID { get; set; }
        public int SelectedMapID { get; set; }
        public float TopSpeed { get; set; }
        public int Money { get; set; }
        public float TopDistance { get; set; }
        public List<Car> Cars = new List<Car>();
        public List<Map> Maps = new List<Map>();

        public SaveData(float speed, int money, float distance, int selCarID, List<Car> cars, int selMapID, List<Map> maps)
        {
            TopDistance = distance;
            TopSpeed = speed;
            Money = money;
            SelectedCarID = selCarID;
            Cars = cars;
            SelectedMapID = selMapID;
            Maps = maps;
        }
    }


    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + SaveDir);
        SaveData data = new SaveData(GameStateManager._instance.TopSpeed, GameStateManager._instance.MoneyAmount, GameStateManager._instance.Distance,
        GameStateManager._instance.SelectedCarID, GameStateManager._instance.AvailableCars, 
        GameStateManager._instance.SelectedMapID, GameStateManager._instance.AvailableMaps); 
        
        
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Game Was Saved!");
    }

    public void LoadGame()
    {
        Debug.Log(Application.persistentDataPath + SaveDir);
        if (File.Exists(Application.persistentDataPath + SaveDir))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + SaveDir, FileMode.Open);
                SaveData data = (SaveData)bf.Deserialize(fs);
                fs.Close();
                GameStateManager._instance.MoneyAmount = data.Money;
                GameStateManager._instance.TopSpeed = data.TopSpeed;
                
                GameStateManager._instance.AvailableCars = data.Cars;
                GameStateManager._instance.SelectedCarID = data.SelectedCarID;
                GameStateManager._instance.SelectedCar = data.Cars[data.SelectedCarID];
                
                GameStateManager._instance.AvailableMaps = data.Maps;
                GameStateManager._instance.SelectedMap = data.Maps[data.SelectedMapID];
                GameStateManager._instance.SelectedMapID = data.SelectedMapID;

                if(data.Cars.Count < _defaultCarList.Count)
                    for(int i = data.Cars.Count; i< _defaultCarList.Count; i++)
                        GameStateManager._instance.AvailableCars.Add(_defaultCarList[i]);

                if(data.Maps.Count < _defaultMapList.Count)
                    for(int i = data.Maps.Count; i< _defaultMapList.Count; i++)
                        GameStateManager._instance.AvailableMaps.Add(_defaultMapList[i]);
            }
            catch (Exception)
            {
                CreateNewSaveFile();
            }
        }
        else
        {
            CreateNewSaveFile();
        }
    }

    private void CreateNewSaveFile()
    {
        Debug.Log("Save File Corrupted!");
        GameStateManager._instance.MoneyAmount = 0;
        GameStateManager._instance.TopSpeed = 0;
        GameStateManager._instance.Distance = 0;
        
        GameStateManager._instance.SelectedCarID = 0;
        GameStateManager._instance.AvailableCars = _defaultCarList;
        GameStateManager._instance.SelectedCar = _defaultCarList[0];

        GameStateManager._instance.SelectedMapID = 0;
        GameStateManager._instance.AvailableMaps = _defaultMapList;
        GameStateManager._instance.SelectedMap = _defaultMapList[0];
    }
}