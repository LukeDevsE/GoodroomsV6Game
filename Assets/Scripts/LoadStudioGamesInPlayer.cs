/*
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine.Networking;

public class LoadStudioGamesInPlayer : MonoBehaviour
{
    public string webServiceUrl = "https://apibluegr.cf/unitygetgamefromid.php";
    public string dataFilePath = "cubeData.json";
    public GameObject cubePrefab;
    public Material[] materials;

    void Start()
    {
        // Get the ID passed in the URL
        string id = "1";

        // Make a HTTP request to the web server to retrieve the JSON data from the database
        StartCoroutine(GetJsonDataFromWebServer(id));
    }

    IEnumerator<object> GetJsonDataFromWebServer(string id)
    {
        // Construct the URL for the web service
        string url = webServiceUrl + "?id=" + id;

        // Send a HTTP GET request to the web service
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            UnityEngine.Debug.LogError("Error: " + www.error);
        }
        else
        {
            // Parse the JSON data
            string json = www.downloadHandler.text;
            /*
            CubeDataListed CubeDataListed = JsonUtility.FromJson<CubeDataListed>(json);
            UnityEngine.Debug.Log(json);
            // Instantiate a cube for each set of data
            foreach (CubeDatasee cubeDatasee in CubeDataListed.cubes)
            {
                GameObject newCube = Instantiate(cubePrefab, cubeDatasee.position, cubeDatasee.rotation);
                newCube.transform.localScale = cubeDatasee.scale;
                Material selectedMaterial = null;
                foreach (Material m in materials)
                {
                    if (m.name == cubeDatasee.material)
                    {
                        selectedMaterial = m;
                        UnityEngine.Debug.Log("Found");
                        break;
                    }
                }
                newCube.GetComponent<Renderer>().material = selectedMaterial;
                if (cubeDatasee.deadly)
                {
                    newCube.AddComponent<TouchRespawn>();
                }
            }
            */
            /*
        }
    }

    // Get the value of a query string parameter in the URL
    string GetQueryStringValue(string key)
    {
        string[] queryStrings = System.Environment.GetCommandLineArgs();
        foreach (string s in queryStrings)
        {
            if (s.StartsWith(key))
            {
                return s.Substring(key.Length + 1);
            }
        }
        return "";
    }
}
/*
public class CubeDatasee
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public string material;
    public bool deadly;

    public CubeDatasee(Vector3 position, Quaternion rotation, Vector3 scale, string material, bool deadly)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.material = material;
        this.deadly = deadly;
    }
}
public class CubeDataListed
{
    public List<CubeDatasee> cubes;

    public CubeDataListed(List<CubeDatasee> cubes)
    {
        this.cubes = cubes;
    }
}
*/
/*
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

public class CubeDataList
{
    public List<CubeData> cubes;

    public CubeDataList(List<CubeData> cubes)
    {
        this.cubes = cubes;
    }
}
*/
/*
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
public class SavingTest : MonoBehaviour
{
    public GameObject cubePrefab;

    private List<CubeData> cubes = new List<CubeData>();
    public Material[] materials;
    void Start()
    {

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