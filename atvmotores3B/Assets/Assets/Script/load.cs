using UnityEngine;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour
{
    public string nomeDaCena;

    void Start()
    {
    SceneManager.LoadScene(nomeDaCena);
    }

void Update()
    {
        
    }
}
