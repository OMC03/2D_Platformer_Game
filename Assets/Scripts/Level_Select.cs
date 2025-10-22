using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Select : MonoBehaviour
{
    // Start is called before the first frame update
    public void Reset()
    {
        SceneManager.LoadScene("Level Select");
    }
}
