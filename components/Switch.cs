using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public enum SwitchTypeEnum
{
    Normal,
    Once,
    Toggle
};

public enum SwitchTriggerTypeEnum
{
    Press,
    Touch,
};


public interface ISwitchTrigger : IEventSystemHandler {
    void onSwitchPress();
}

public class Switch : MonoBehaviour {


    public SwitchTypeEnum SwitchType = SwitchTypeEnum.Normal;
    public SwitchTriggerTypeEnum SwitchTriggerType = SwitchTriggerTypeEnum.Press;

    //public CharacterEnum[] CollisionEntitys;
    public GameObject[] SwitchObjects;

    private CharacterDoAction cDoAction;

    private bool onlyPressed = false;
    public bool isPressed = false;

    public float pressTime = 0.0f;

    void Start() {
        if (isPressed) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Update () {
        if ( cDoAction )
        {
            if (SwitchTriggerType == SwitchTriggerTypeEnum.Press && !cDoAction.isActionPress) {
                return;
            }

            switch (SwitchType)
            {
                case SwitchTypeEnum.Normal:
                    if (!isPressed)
                    {
                        Trigger();
                        StartCoroutine(ActionEnd());
                    }
                    break;
                case SwitchTypeEnum.Once:
                    if (!onlyPressed)
                    {
                        Trigger();
                        onlyPressed = true;
                    }
                    break;
                case SwitchTypeEnum.Toggle:
                    if (SwitchTriggerType == SwitchTriggerTypeEnum.Touch)
                    {
                        if (!isPressed)
                        {
                            Trigger();
                        }
                    }
                    else {
                        Trigger();
                    }
                   
                    break;
                default:
                    break;
            }
        }
    }

    void Trigger()
    {
        foreach (var ObjectToTrigger in SwitchObjects)
        {
            ExecuteEvents.Execute<ISwitchTrigger>(ObjectToTrigger, null, (x, y) => x.onSwitchPress());
        }
        isPressed = !isPressed;

        var rotation = isPressed ? 180 : 0;

        transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    protected virtual IEnumerator ActionEnd()
    {
        yield return new WaitForSeconds(pressTime);

        isPressed = false;

        var rotation = isPressed ? 180 : 0;

        transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cDoAction = other.GetComponent<CharacterDoAction>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        cDoAction = null;

        if (SwitchTriggerType == SwitchTriggerTypeEnum.Touch && isPressed 
            && SwitchType == SwitchTypeEnum.Toggle)
        {
            Trigger();
            isPressed = false;
        }
    }

}
