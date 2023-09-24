using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
public class SaveAndPlace : MonoBehaviour
{
    public GameObject cubePrefab;

    public List<CubeData> cubes = new List<CubeData>();

    private string dataFilePath = "map.gr";
    public string folderPath = @"C:\Users\%USERNAME%\AppData\LocalLow\GoodRooms\GoodRooms";
    public float spawnDistance = 1f; // You can adjust the value of spawnDistance in the Inspector
    public Material[] materials;
    void Start()
    {
        LoadCubes();
    }

    public void SaveCubes()
    {
        // Get all objects with the "Cube" tag
        GameObject[] cubeObjects = GameObject.FindGameObjectsWithTag("Cube");
        List<CubeData> cubeDataList = new List<CubeData>();
        
        // Add data for each cube to the list
        foreach (GameObject cubeObject in cubeObjects)
        {
            string MaterialName = cubeObject.GetComponent<Renderer>().material.name;
            bool deadly = cubeObject.GetComponent<TouchRespawn>() != null;
            cubeDataList.Add(new CubeData(cubeObject.transform.position, cubeObject.transform.rotation, cubeObject.transform.localScale, MaterialName.Replace(" (Instance)", ""), deadly));
        }
        
        // Convert the list to JSON and save to file
        string json = JsonUtility.ToJson(new CubeDataList(cubeDataList));
        File.WriteAllText(Application.persistentDataPath + "/" + dataFilePath, json);
    }

    public void OpenDirectoryOfSave()
    {
        Process.Start("explorer.exe", Environment.ExpandEnvironmentVariables(folderPath));
    }

    void LoadCubes()
    {
        if (File.Exists(Application.persistentDataPath + "/" + dataFilePath))
        {
            // Load JSON data from file
            string json = File.ReadAllText(Application.persistentDataPath + "/" + dataFilePath);
            CubeDataList cubeDataList = JsonUtility.FromJson<CubeDataList>(json);

            // Instantiate a cube for each set of data
            foreach (CubeData cubeData in cubeDataList.cubes)
            {
                GameObject newCube = Instantiate(cubePrefab, cubeData.position, cubeData.rotation);
                newCube.transform.localScale = cubeData.scale;
                Material selectedMaterial = null;
                foreach (Material m in materials)
                {
                if (m.name == cubeData.material)
                {
                    selectedMaterial = m;
                    UnityEngine.Debug.Log("Found");
                    break;
                }
                }
                newCube.GetComponent<Renderer>().material = selectedMaterial;
                if (cubeData.deadly) 
                {
                    newCube.AddComponent<TouchRespawn>();
                }
            }
        }
    }

    public void CreateCube() 
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        string MaterialName = newCube.GetComponent<Renderer>().material.name;
        cubes.Add(new CubeData(newCube.transform.position, newCube.transform.rotation, newCube.transform.localScale, MaterialName, false));
        
        // Add the "Cube" tag to the new cube
        newCube.tag = "Cube";
    }
    public void CreateCubeFromVar(Vector3 position, Quaternion rotation, Vector3 localScale, string materialName, bool deadly) 
    {
    // Instantiate the cube prefab with the desired parameters
    GameObject newCube = Instantiate(cubePrefab, position, rotation);
    newCube.transform.localScale = localScale;

    // Set the cube material to the desired material
    Material selectedMaterial = null;
    foreach (Material m in materials)
    {
        if (m.name == materialName)
        {
            selectedMaterial = m;
            break;
        }
    }
    if (selectedMaterial != null)
    {
        newCube.GetComponent<Renderer>().material = selectedMaterial;
    }
    else
    {
        UnityEngine.Debug.LogError($"Could not find material with name {materialName}");
    }
    /*
    Material[] idk = newCube.GetComponent<Renderer>().materials;
    Array.Resize(ref idk, newCube.GetComponent<Renderer>().materials.Length + 1);
    
    newCube.GetComponent<Renderer>().materials[newCube.GetComponent<Renderer>().materials.Length - 1] = selectmaterial;
    */
    // Add the cube to the list of cubes
    cubes.Add(new CubeData(position, rotation, localScale, materialName.Replace(" (Instance)", ""), deadly));
    }

}

[System.Serializable]
public class CubeData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public String material;
    public bool deadly;
    public CubeData(Vector3 position, Quaternion rotation, Vector3 scale, String material, bool deadly) 
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.material = material;
        this.deadly = deadly;
    }
}

[System.Serializable]
public class CubeDataList
{
    public List<CubeData> cubes;

    public CubeDataList(List<CubeData> cubes)
    {
        this.cubes = cubes;
    }
}

/*
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SavingTest : MonoBehaviour
{
    public GameObject cubePrefab;

    public List<CubeData> cubes = new List<CubeData>();

    private string dataFilePath = "cubes.json";

    public float spawnDistance = 1f; // You can adjust the value of spawnDistance in the Inspector
    void Start()
    {
        LoadCubes();
    }

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            GameObject newCube = Instantiate(cubePrefab, worldPosition, Quaternion.identity);
            cubes.Add(new CubeData(newCube.transform.position, newCube.transform.rotation, newCube.transform.localScale));
        }
        */
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveCubes();
        }
    }

    void SaveCubes()
    {
        string json = JsonUtility.ToJson(new CubeDataList(cubes));
        File.WriteAllText(Application.persistentDataPath + "/" + dataFilePath, json);
    }

    void LoadCubes()
    {
        if (File.Exists(Application.persistentDataPath + "/" + dataFilePath))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/" + dataFilePath);
            CubeDataList cubeDataList = JsonUtility.FromJson<CubeDataList>(json);
            cubes = cubeDataList.cubes;

            foreach (CubeData cubeData in cubes)
            {
                Instantiate(cubePrefab, cubeData.position, cubeData.rotation).transform.localScale = cubeData.scale;
            }
        }
    }
    public void CreateCube() 
    {
        /*
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        GameObject newCube = Instantiate(cubePrefab, worldPosition, Quaternion.identity);
        cubes.Add(new CubeData(newCube.transform.position, newCube.transform.rotation, newCube.transform.localScale));
        */
        /*
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        cubes.Add(new CubeData(newCube.transform.position, newCube.transform.rotation, newCube.transform.localScale));

    }
}

[System.Serializable]
public class CubeData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public CubeData(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }
}

[System.Serializable]
public class CubeDataList
{
    public List<CubeData> cubes;

    public CubeDataList(List<CubeData> cubes)
    {
        this.cubes = cubes;
    }
}
*/