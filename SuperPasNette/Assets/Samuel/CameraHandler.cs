using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera[] cameras;
    private bool keydown = false;
    [HideInInspector]
    public bool bWasMaxLenght = false;
    public bool canGodUseGodCam = false;
    public  GameObject hidingplane;
    [HideInInspector]
    public GameObject activeCam;
    void Start()
    {
        cameras = GetComponentsInChildren<Camera>();
        activeCam = cameras[0].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        hidingplane.SetActive(!canGodUseGodCam); // c'est degeu mais il est tard
        if(Input.anyKey == false) 
        {
            keydown = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if (!keydown) 
            {
                SwitchCamera(0);
            }

            keydown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!keydown)
            {
                SwitchCamera(1);
            }

            keydown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!keydown)
            {
                SwitchCamera(2);
            }

            keydown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!keydown)
            {
                SwitchCamera(3);
            }

            keydown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (!keydown)
            {
                SwitchCamera(4);
            }

            keydown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (!keydown)
            {
                SwitchCamera(5);
            }

            keydown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (!keydown)
            {
                SwitchCamera(6);
            }

            keydown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (!keydown)
            {
                SwitchCamera(7);
            }

            keydown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (!keydown)
            {
                SwitchCamera(8);
            }

            keydown = true;
        }
    }

    void SwitchCamera(int iIndex) 
    {
        if(iIndex> cameras.Length-1) 
        {
            return;
        }

        bool bIsEven = cameras.Length % 2 == 0;
        int camlenght = cameras.Length + (bIsEven ? 0 : 1);
        bool bIsMaxLenght = iIndex == cameras.Length-1; // minus one for god mode
        bool needReset = bWasMaxLenght;
        for (float i = 0; i < cameras.Length; i++)
        {
            if (bIsMaxLenght && !needReset) 
            {
                bWasMaxLenght = true;
                cameras[(int)i].enabled = true;
               
                float x = 0;
                if (i >= 4) 
                {
                    x = (1f / (camlenght/2f))*2;

                }
                else if (i >= 2) 
                {
                    x = 1f / (camlenght/2f);

                }
                cameras[(int)i].rect = new Rect(x, (i+1)%2 == 0? 0 : 0.5f, 1.0f/ (camlenght/2f), 0.5f);
            }
            else 
            {
                if (needReset) 
                {
                    bWasMaxLenght = false;
                    cameras[(int)i].rect = new Rect(0, 0, 1, 1);
                }
                if( i != cameras.Length || canGodUseGodCam) 
                {
                cameras[(int)i].enabled = i == iIndex;
                    if(i == iIndex) 
                    {
                        activeCam = cameras[(int)i].gameObject;
                    }
                }
            }
        }

        switch (iIndex)
        {
            case 0:
                AkSoundEngine.SetState("Camera_POV", "Cam1");
                break;
            case 1:
                AkSoundEngine.SetState("Camera_POV", "Cam2");
                break;
            case 2:
                AkSoundEngine.SetState("Camera_POV", "Cam3");
                break;
            case 3:
                AkSoundEngine.SetState("Camera_POV", "Cam4");
                break;
            case 4:
                AkSoundEngine.SetState("Camera_POV", "Cam5");
                break;
        }

    }
}
