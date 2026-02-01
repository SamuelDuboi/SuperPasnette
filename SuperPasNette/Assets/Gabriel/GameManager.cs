using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public NPCManager npcManager;
    public LayoutManager layoutManager;
    public CharacterController player;
    public float newSpeed;

    private bool isLosing = false;
    private float sanity = 100;
    private float maxSanity = 100;
    private int nPickedUpItem = 0;

    private void Start()
    {
        uiManager.OnPause += UiManager_OnPause;
        uiManager.OnPlay += UiManager_OnPlay;
        uiManager.Restart += UiManager_Restart;
        uiManager.OnQuitLevel += UiManager_OnQuitLevel;
    }

    private void UiManager_OnQuitLevel()
    {
        if (player)
        {
            player.OnPickUp -= Player_OnPickUp;
            player.OnTalk -= Player_OnTalk;
        }

        if (npcManager)
        {
            npcManager.SanityDecrease -= NpcManager_SanityDecrease;
            npcManager.OnDeath -= NpcManager_OnDeath;
        }


        player = null;
        npcManager = null;
        layoutManager = null;

        if (SceneManager.sceneCount > 1)
            StartCoroutine(UnloadAsync("Level"));

        if (!isLosing) uiManager.RestoreCreditBtn();
    }

    private void GetRemainingManagers()
    {
        GameObject[] gameObjects = SceneManager.GetSceneAt(1).GetRootGameObjects();
        foreach (GameObject item in gameObjects)
        {
            if (item.GetComponent<CharacterController>())
            {
                player = item.GetComponent<CharacterController>();
                continue;
            }

            if (item.GetComponent<NPCManager>())
            {
                npcManager = item.GetComponent<NPCManager>();
                continue;
            }

            if (item.GetComponent<LayoutManager>())
            {
                layoutManager = item.GetComponent<LayoutManager>();
                continue;
            }
        }

        nPickedUpItem = 0;

        player.OnPickUp += Player_OnPickUp;
        player.OnTalk += Player_OnTalk;
        npcManager.SanityDecrease += NpcManager_SanityDecrease;
        npcManager.OnDeath += NpcManager_OnDeath;

        player.itemsToPickup.SetManager(uiManager);
    }

    private void Player_OnTalk(Client obj)
    {
        if (obj.gameObject.CompareTag("Cashier") && nPickedUpItem == 0)
        {
            isLosing = false;
            StartCoroutine(TimeBeforeOpenEndScreen(2f));
        }
        else
        {
            uiManager.getHud().ToggleDialogueBox(true);
            uiManager.getHud().dialogueBox.transform.GetChild(1).GetComponent<TMP_Text>().text = obj.GetBark();
        }
    }

    private void Player_OnPickUp()
    {
        nPickedUpItem++;
        switch (nPickedUpItem)
        {
            case 1:
                npcManager?.KillClients();
                //AkSoundEngine.SetState("Paranormal", "Customers_01");
                break;
            case 2:
                npcManager?.InitNPC();
                //AkSoundEngine.SetState("Paranormal", "Roamers_02");
                break;
            case 3:
                Debug.Log("Blinking Lights");
                break;
            case 4:
                //layoutManager?.ChangeLayout();
                //AkSoundEngine.SetState("Paranormal", "Layout_04");
                break;
            case 5:
                npcManager?.InitMrX();
                //AkSoundEngine.SetState("Paranormal", "Layout_05");
                break;
            case 6:
                Debug.Log("Slow Speed");
                npcManager?.SlowEveryone(newSpeed);
                if (player) player.movementSpeed = newSpeed;
                //AkSoundEngine.SetState("Paranormal", "Layout_06");
                break;
        }

        return;
    }

    private void Update()
    {
        npcManager?.UpdateMrX(player.transform.position);

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Update sanity");
            uiManager.UpdateSanity(0.25f);
        }


        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Update sanity");
            uiManager.UpdateSanity(1f);
        }


        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Update sanity");
            uiManager.UpdateSanity(0f);
        }
    }


    private void NpcManager_OnDeath()
    {
        isLosing = true;
        uiManager.setEndScreen(ENDSCREEN_TYPE.MR_X);
        npcManager.StopNPC();
    }

    private void UiManager_Restart()
    {
        player.OnPickUp -= Player_OnPickUp;
        player.OnTalk -= Player_OnTalk;
        npcManager.SanityDecrease -= NpcManager_SanityDecrease;
        npcManager.OnDeath -= NpcManager_OnDeath;

        player = null;
        npcManager = null;
        layoutManager = null;
        StartCoroutine(UnloadAsync("Level"));
    }

    private void UiManager_OnPlay()
    {
        StartCoroutine(LoadAsync("Level"));
    }

    private void UiManager_OnPause(bool obj)
    {
        npcManager.PauseNPC(obj);
        player.setPause(obj);
    }

    private void NpcManager_SanityDecrease(float obj)
    {
        sanity -= obj;
        uiManager.UpdateSanity(sanity / maxSanity);

        if (sanity <= 0)
        {
            isLosing = true;
            uiManager.setEndScreen(ENDSCREEN_TYPE.SANITY);
            npcManager.StopNPC();
        }
    }

    IEnumerator TimeBeforeOpenEndScreen(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        uiManager.setEndScreen(ENDSCREEN_TYPE.WIN);

    }

    IEnumerator LoadAsync(string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        asyncLoad.completed += AsyncLoad_completed;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }

    IEnumerator UnloadAsync(string sceneToUnload)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneToUnload);
        asyncUnload.completed += AsyncUnload_completed;

        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }

    private void AsyncLoad_completed(AsyncOperation obj)
    {
        obj.completed -= AsyncLoad_completed;
        GetRemainingManagers();
        if (!isLosing) uiManager.CloseStoryScreen();
        else uiManager.EndScreenRestartPrepared();
    }

    private void AsyncUnload_completed(AsyncOperation obj)
    {
        obj.completed -= AsyncUnload_completed;
        if (!isLosing) uiManager.RestoreCreditBtn();
        else StartCoroutine(LoadAsync("Level"));
    }
}
