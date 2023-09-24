 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
using Photon.Pun;
public class ChooseGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void TestRoom() 
    {
        SceneManager.LoadScene("1");
    }
    public void Obby() 
    {
        SceneManager.LoadScene("2");
    }
    public void MainMenu() 
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
    public void Studio() 
    {
        SceneManager.LoadScene("Studio");
    }
}
