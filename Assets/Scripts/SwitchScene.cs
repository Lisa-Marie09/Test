using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class SwitchScene : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            Touch touch = Touch.activeTouches[0];

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Debug.Log("Touch erkannt");

                Ray ray = Camera.main.ScreenPointToRay(
                    touch.screenPosition
                );

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Etwas getroffen: " + hit.transform.name);

                    if (hit.transform.CompareTag("Object"))
                    {
                        SceneManager.LoadScene("Test");
                        Debug.Log("Klappt");
                    }
                    else
                    {
                        Debug.Log("Falsches Tag! Das Objekt hat das Tag: "
                                  + hit.transform.tag);
                    }
                }
                else
                {
                    Debug.Log("Raycast ins Leere gegangen (nichts getroffen).");
                }
            }
        }
        
    }
        
}
