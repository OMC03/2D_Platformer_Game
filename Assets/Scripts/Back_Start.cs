using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back_Start : MonoBehaviour
{
    // Start is called before the first frame update
    public void Reset()
    {
        SceneManager.LoadScene("Start Screen");
    }
}
