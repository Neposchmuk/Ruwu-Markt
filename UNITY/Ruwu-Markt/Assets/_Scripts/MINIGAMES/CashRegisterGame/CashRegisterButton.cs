using UnityEngine;
using UnityEngine.UI;

public enum UIButtonType
    {
        CASH,
        PAYCARD,
        PAYCASH
    }

public class CashRegisterButton : MonoBehaviour
{
    [SerializeField] UIButtonType buttonType;

    [SerializeField] Button button;

    [SerializeField] int ChangeAmount; [Tooltip("Amount in cents")]

    void OnEnable()
    {
        GameEventsManager.instance.questEvents.onUIButtonInteract += Interact;
        GameEventsManager.instance.questEvents.onToggleButtonInteractable += ToggleButton;
    }

    void OnDisable()
    {
        GameEventsManager.instance.questEvents.onUIButtonInteract -= Interact;
        GameEventsManager.instance.questEvents.onToggleButtonInteractable -= ToggleButton;
    }

    private void ToggleButton(UIButtonType buttonType, bool toggle)
    {
        if(buttonType == this.buttonType)
        {
            button.interactable = toggle;
        }
    }


    private void Interact(GameObject gameObject)
    {
        if(gameObject != this.gameObject) return;

        switch (buttonType)
        {
            case UIButtonType.CASH:
            AddChange();
                break;
            case UIButtonType.PAYCARD:
            PayCard();
                break;
            case UIButtonType.PAYCASH:
            PayCash();
                break;
        }
    }

    private void AddChange()
    {
        GameEventsManager.instance.questEvents.ButtonAddChange(ChangeAmount);
    }

    private void PayCard()
    {
        GameEventsManager.instance.questEvents.PayCard();
    }

    private void PayCash()
    {
        GameEventsManager.instance.questEvents.PayCash();
    }
}
