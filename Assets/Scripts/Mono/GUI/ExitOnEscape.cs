using UnityEngine;

public class ExitOnEscape : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKey("escape") )
            Application.Quit();
    }
}