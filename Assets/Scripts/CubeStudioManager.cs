using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class CubeStudioManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public TMP_InputField sizeXInput, sizeYInput, sizeZInput, posXInput, posYInput, posZInput, materialInput;
    public Toggle toggle;
    //public TouchRespawn TouchRespawn;
    private GameObject selectedCube;
    private GameObject lastselectedCube;
    public Material[] materials;
    public SaveAndPlace cubeManager;
    public AudioSource duplicatesound;
    public Text textofenablepropertiesbutton;
    public bool propertiesenabled = false;
    private GameObject[] selectedcubesfind;
    void Update()
    {
        //if (!IsMouseOverInputField() && Input.GetMouseButtonDown(0)) {
            /*
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.CompareTag("Cube")) {
                    // Set the selected cube and show the input fields
                    selectedCube = hit.collider.gameObject;
                    SetInputFields(selectedCube.transform.localScale, selectedCube.transform.position, selectedCube);
                }
            }
            */
            
            selectedcubesfind = GameObject.FindGameObjectsWithTag("Cube");
            // Loop through each cube and check its material name
            bool found;
            found = false;
            foreach (GameObject cube in selectedcubesfind)
            {
                    Material[] materialsofcube = cube.GetComponent<Renderer>().materials;
                    if (materialsofcube.Length > 1 && propertiesenabled)
                    {
                        if(cube != lastselectedCube) {
                            if (selectedCube == null) 
                            {
                                selectedCube = cube;
                            }
                            else 
                            {
                                lastselectedCube = selectedCube;
                                selectedCube = cube;
                            }

                        }
                        //selectedCube = cube;
                        if (selectedCube != lastselectedCube) 
                        {
                        SetInputFields(selectedCube.transform.localScale, selectedCube.transform.position, selectedCube);
                        }
                        found = true;
                    }
            }
            if (found == false) 
            {
                selectedCube = null;
            }
            // && materialsofcube[1].name.Contains("Custom/Outline")
            if (selectedCube == null) 
            {
                sizeXInput.gameObject.SetActive(false);
                sizeYInput.gameObject.SetActive(false);
                sizeZInput.gameObject.SetActive(false);
                posXInput.gameObject.SetActive(false);
                posYInput.gameObject.SetActive(false);
                posZInput.gameObject.SetActive(false);
                materialInput.gameObject.SetActive(false);
                toggle.gameObject.SetActive(false);
            }
            
            //Debug.Log("found cube");
        //}
        /*
        else {
                // Hide the input fields if a cube was not clicked
                selectedCube = null;
                sizeXInput.gameObject.SetActive(false);
                sizeYInput.gameObject.SetActive(false);
                sizeZInput.gameObject.SetActive(false);
                posXInput.gameObject.SetActive(false);
                posYInput.gameObject.SetActive(false);
                posZInput.gameObject.SetActive(false);
                materialInput.gameObject.SetActive(false);
                toggle.gameObject.SetActive(false);
                Debug.Log("no cube");
            }
            */
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                //RaycastHit hit;
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (Physics.Raycast(ray, out hit)) {
                //    if (hit.collider.CompareTag("Cube")) {
                    GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
                    foreach (GameObject cube in cubes)
                    {
                        Material[] materialsofcube = cube.GetComponent<Renderer>().materials;
                        if (materialsofcube.Length > 1)
                        {
                            Debug.Log("Doing");
                        bool isdeadly = false;
                        if (cube.GetComponent<TouchRespawn>() != null) 
                        {
                            isdeadly = true;
                        }
                        cubeManager.CreateCubeFromVar(cube.transform.position, cube.transform.rotation, cube.transform.localScale, cube.GetComponent<Renderer>().material.name.Replace(" (Instance)", ""), isdeadly);
                        duplicatesound.Play(); 
                        }
                    }
                    //}
                //}
            }
         }
            if (Input.GetKeyDown(KeyCode.Delete)) 
            {
                //RaycastHit hit;
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (Physics.Raycast(ray, out hit)) {
                    //if (hit.collider.CompareTag("Cube")) {
                    // Find all game objects with the "Cube" tag
                    GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");

                    // Loop through each cube and check its material name
                    foreach (GameObject cube in cubes)
                    {
                        Material[] materialsofcube = cube.GetComponent<Renderer>().materials;
                        if (materialsofcube.Length > 1)
                        {
                Destroy(cube);
                sizeXInput.gameObject.SetActive(false);
                sizeYInput.gameObject.SetActive(false);
                sizeZInput.gameObject.SetActive(false);
                posXInput.gameObject.SetActive(false);
                posYInput.gameObject.SetActive(false);
                posZInput.gameObject.SetActive(false);
                materialInput.gameObject.SetActive(false);
                toggle.gameObject.SetActive(false);
                        }
                    }
                //return;
                   // }
                //}
            }
        if (string.IsNullOrEmpty(sizeXInput.text) ||
        string.IsNullOrEmpty(sizeYInput.text) ||
        string.IsNullOrEmpty(sizeZInput.text) ||
        string.IsNullOrEmpty(posXInput.text) ||
        string.IsNullOrEmpty(posYInput.text) ||
        string.IsNullOrEmpty(posZInput.text))
        {
        return;
        }
        if (selectedCube != null && propertiesenabled) {
            // Update the selected cube's scale and position based on the input fields
            Vector3 newScale = new Vector3(
                float.Parse(sizeXInput.text),
                float.Parse(sizeYInput.text),
                float.Parse(sizeZInput.text)
            );
            Vector3 newPosition = new Vector3(
                float.Parse(posXInput.text),
                float.Parse(posYInput.text),
                float.Parse(posZInput.text)
            );
            string materialName = materialInput.text;
            //Debug.Log(materials[1].name);

            Material selectedMaterial = null;
            foreach (Material m in materials)
            {
                if (m.name == materialName)
                {
                    selectedMaterial = m;
                    //Debug.Log("Found");
                    break;
                }
            }
            if (toggle.isOn)
            {
                TouchRespawn myScript = selectedCube.GetComponent<TouchRespawn>();
                if (myScript == null)
                {
                    selectedCube.AddComponent<TouchRespawn>();
                }
            }
            else
            {
                TouchRespawn myScript = selectedCube.GetComponent<TouchRespawn>();
                if (myScript != null)
                {
                    Destroy(myScript);
                }
            }

            if (selectedMaterial == null)
            {
                //Debug.LogError($"Material '{materialName}' not found in Materials folder.");
                return;
            }
            selectedCube.GetComponent<Renderer>().material = selectedMaterial;
            if (selectedCube.transform.localScale == newScale && selectedCube.transform.position == newPosition) {
                return;
            }
            
            /*
             && float.TryParse(sizeXInput.text, out float sizeX) &&
    float.TryParse(sizeYInput.text, out float sizeY) &&
    float.TryParse(sizeZInput.text, out float sizeZ) &&
    float.TryParse(posXInput.text, out float posX) &&
    float.TryParse(posYInput.text, out float posY) &&
    float.TryParse(posZInput.text, out float posZ) &&
    sizeX != selectedCube.transform.localScale.x &&
    sizeY != selectedCube.transform.localScale.y &&
    sizeZ != selectedCube.transform.localScale.z &&
    posX != selectedCube.transform.position.x &&
    posY != selectedCube.transform.position.y &&
    posZ != selectedCube.transform.position.z
    */
            /*Material[] materialsofcube = selectedCube.GetComponent<Renderer>().materials;
            if (materialsofcube.Length > 1)
            {
                return;
            }
            */
            selectedCube.transform.localScale = newScale;
            selectedCube.transform.position = newPosition;
        }
    }

    void SetInputFields(Vector3 scale, Vector3 position, GameObject cube) {
        // Set the input field values and show the input fields
        sizeXInput.text = scale.x.ToString();
        sizeYInput.text = scale.y.ToString();
        sizeZInput.text = scale.z.ToString();
        posXInput.text = position.x.ToString();
        posYInput.text = position.y.ToString();
        posZInput.text = position.z.ToString();
        TouchRespawn myScript = cube.GetComponent<TouchRespawn>();
        if (myScript != null)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        materialInput.text = cube.GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
        sizeXInput.gameObject.SetActive(true);
        sizeYInput.gameObject.SetActive(true);
        sizeZInput.gameObject.SetActive(true);
        posXInput.gameObject.SetActive(true);
        posYInput.gameObject.SetActive(true);
        posZInput.gameObject.SetActive(true);
        materialInput.gameObject.SetActive(true);
        toggle.gameObject.SetActive(true);
    }
    bool IsMouseOverInputField() {
    // Check if the mouse is over any of the input fields
    if (EventSystem.current == null) return false;
    PointerEventData eventData = new PointerEventData(EventSystem.current);
    eventData.position = Input.mousePosition;
    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventData, results);
    foreach (RaycastResult result in results) {
        if (result.gameObject.GetComponent<TMP_InputField>() != null  || result.gameObject.transform.parent.gameObject.name == "Toggle" || result.gameObject.transform.parent.gameObject.name == "Background") {
            return true;
        }
        //Debug.Log(result.gameObject.name);
    }
    return false;
    }   
    public void ToggleEnabled() 
    {
        propertiesenabled = !propertiesenabled;
        if (propertiesenabled) {
            textofenablepropertiesbutton.text = "Disable Properties";
        }
        else
        {
            textofenablepropertiesbutton.text = "Enable Properties";
            sizeXInput.gameObject.SetActive(false);
            sizeYInput.gameObject.SetActive(false);
            sizeZInput.gameObject.SetActive(false);
            posXInput.gameObject.SetActive(false);
            posYInput.gameObject.SetActive(false);
            posZInput.gameObject.SetActive(false);
            materialInput.gameObject.SetActive(false);
            toggle.gameObject.SetActive(false);
        }
    }
    
}