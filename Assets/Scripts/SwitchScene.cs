using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("Touch erkannt");
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("2. Etwas getroffen: " + hit.transform.name);
                if (hit.transform.tag == "Object")
                {
                    SceneManager.LoadScene("Test");
                    Debug.Log("Klappt");
                }else{
                    Debug.Log("Falsches Tag! Das Objekt hat das Tag: " + hit.transform.tag);
                }
            }
            else
            {
                Debug.Log("Raycast ins Leere gegangen (nichts getroffen).");
            }
            
        }
        
    }
        
}
