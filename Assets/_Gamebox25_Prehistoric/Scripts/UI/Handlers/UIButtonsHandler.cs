using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class UIButtonsHandler : MonoBehaviour
{
    private EventBus _eventBus;

    [Inject]
    public void Constructor(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void OpenUiWindow(string windowType)
    {
        if (Enum.TryParse(windowType, true, out UiWindowType type))
        {
            _eventBus.RaiseOnOpenUiWindow(type);
        }
    }

    public void OpenScene(string sceneName)
    {
        GameObject eventTarget = EventSystem.current.currentSelectedGameObject;
        eventTarget.TryGetComponent(out Button targetButton);
        
        if (targetButton != null)
        {
            targetButton.interactable = false;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        if (asyncLoad == null)
        {
            if (targetButton != null)
            {
                targetButton.interactable = true;
            }
        }
    }

    public void PlayClickSound()
    {
        _eventBus.RaiseUiClick();
    }

    public void ExitButton()
    {
        Debug.Log("Exit.");
        Application.Quit();
    }
}