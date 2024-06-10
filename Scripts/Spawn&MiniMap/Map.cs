using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Map : MonoBehaviour
{
    [Header("Map")]
    public GameObject MiniMapPanel;
    public GameObject WordMapPanel;

    private bool mapOpen;

    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && !mapOpen)
        {
            mapOpen = true;
            MapOpen();
        }
        else if (context.phase == InputActionPhase.Started && mapOpen)
        {
            mapOpen = false;
            MapClose();
        }
    }

    private void MapOpen()
    {
        WordMapPanel.SetActive(true);
        MiniMapPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    private void MapClose()
    {
        WordMapPanel.SetActive(false);
        MiniMapPanel.SetActive(true);
        Time.timeScale = 1f;
    }
}
