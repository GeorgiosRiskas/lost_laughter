using TMPro;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    [SerializeField] private GameObject content = default;
    [SerializeField] private TextMeshProUGUI notificationText = default;

    void Start()
    {
        EventsManager.OnShowNotificationEvent += EventsManager_OnShowNotificationEvent;
        EventsManager.OnHideNotificationEvent += EventsManager_OnHideNotificationEvent;
        EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
    }   

    private void OnDestroy()
    {
        EventsManager.OnShowNotificationEvent -= EventsManager_OnShowNotificationEvent;
        EventsManager.OnHideNotificationEvent -= EventsManager_OnHideNotificationEvent;
        EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
    }

    private void EventsManager_OnShowNotificationEvent(string notification)
    {
        content.SetActive(true);
        notificationText.text = notification;
    }

    private void EventsManager_OnHideNotificationEvent()
    {
        content.SetActive(false);
    }
    private void EventsManager_OnDialogueStartedEvent(string dialogue)
    {
        content.SetActive(false);
    }

}
