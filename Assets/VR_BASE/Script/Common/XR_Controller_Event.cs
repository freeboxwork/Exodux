using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class XR_Controller_Event : MonoBehaviour
{
    public XRRayInteractor rayInteractor_r;
    public XRRayInteractor rayInteractor_l;

    InputDevice contL;
    InputDevice contR;

    bool isTriggerL;
    bool isTriggerR;

    bool isMediaContCanvasHover = false;
    bool mediaContCanvasEnable = false;

    ControlEvent rightContTriggerBeginEvent;
    ControlEvent rightContPrimaryButtonBeginEvent;
    ControlEvent rightContSecondaryButtonBeginEvent;
    ControlEvent rightContGripButtonEvent;

    ControlEvent leftContTriggerBeginEvent;
    ControlEvent leftContPrimaryButtonBeginEvent;
    ControlEvent leftContSecondaryButtonBeginEvent;
    ControlEvent leftContGripButtonEvdent;

    public XRInteractorLineVisual xrInteractorLineVisual_left;
    public XRInteractorLineVisual xrInteractorLineVisual_right;


    void Start()
    {
        SetControllerEvent();

    }

    void SetControllerEvent()
    {
        rightContTriggerBeginEvent = new ControlEvent(EnumDefinition.PressType.Begin);
        rightContPrimaryButtonBeginEvent = new ControlEvent(EnumDefinition.PressType.Begin);
        rightContSecondaryButtonBeginEvent = new ControlEvent(EnumDefinition.PressType.Begin);

        //pressEvent = new ControlEvent(EnumDefinition.PressType.Continuous); ;

        leftContTriggerBeginEvent = new ControlEvent(EnumDefinition.PressType.Begin);
        leftContPrimaryButtonBeginEvent = new ControlEvent(EnumDefinition.PressType.Begin);
        leftContSecondaryButtonBeginEvent = new ControlEvent(EnumDefinition.PressType.Begin);
    }


    public void EnableControllerLineVisual(bool value)
    {
        var width = value ? 0.02f : 0;
        var length = value ? 10f : 0;
        xrInteractorLineVisual_left.lineLength = length;
        xrInteractorLineVisual_left.lineWidth = width;
        xrInteractorLineVisual_right.lineLength = length;
        xrInteractorLineVisual_right.lineWidth = width;
    }

    /*
    secondaryButton [LeftHand XR Controller] = Y button
    primaryButton [LeftHand XR Controller] = X button
    secondaryButton [RightHand XR Controller] = B button
    primaryButton [RightHand XR Controller] = A button
    */
    void Update()
    {


        if (XR_ControllerBase.instance.IsControllerReadyByType(EnumDefinition.ControllerType.RightController))
        {
            contR = XR_ControllerBase.instance.GetInputDeviceController(EnumDefinition.ControllerType.RightController);


            rightContTriggerBeginEvent.UpdateEvent(EnumDefinition.ControllerEvent.TriggerButton, contR, () =>
            {

            });

            rightContPrimaryButtonBeginEvent.UpdateEvent(EnumDefinition.ControllerEvent.PrimaryButton, contR, () =>
            {


                // Debug.Log("press a");
            });

            rightContSecondaryButtonBeginEvent.UpdateEvent(EnumDefinition.ControllerEvent.SecondaryButton, contR, () =>
            {

                // DebugUI_OnOff();
            });

        }

        if (XR_ControllerBase.instance.IsControllerReadyByType(EnumDefinition.ControllerType.LeftController))
        {
            contL = XR_ControllerBase.instance.GetInputDeviceController(EnumDefinition.ControllerType.LeftController);



            leftContTriggerBeginEvent.UpdateEvent(EnumDefinition.ControllerEvent.TriggerButton, contL, () =>
            {

            });

            leftContPrimaryButtonBeginEvent.UpdateEvent(EnumDefinition.ControllerEvent.PrimaryButton, contL, () =>
            {

                // Debug.Log("press a");
            });

            leftContSecondaryButtonBeginEvent.UpdateEvent(EnumDefinition.ControllerEvent.SecondaryButton, contL, () =>
            {

                // DebugUI_OnOff();
            });
        }

    }







}


[System.Serializable]
public class ControlEvent
{
    public EnumDefinition.PressType pressType;
    bool isPressed;
    bool wasPressed;

    public ControlEvent(EnumDefinition.PressType _pressType)
    {
        pressType = _pressType;
    }

    public void UpdateEvent(EnumDefinition.ControllerEvent controllerEvent, InputDevice controller, UnityEngine.Events.UnityAction _event)
    {
        bool active = false;
        controller.TryGetFeatureValue(new InputFeatureUsage<bool>(controllerEvent.ToString()), out isPressed); // TryGetFeatureValue(CommonUsages.triggerButton, out isPressed);

        SwitchCase(ref active);

        active = isPressed && !wasPressed;
        if (active) _event.Invoke();

        wasPressed = isPressed;
    }


    public void UpdateEvent<F>(InputDevice controller, UnityEngine.Events.UnityAction<F> _event, F f)
    {
        bool active = false;
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out isPressed);

        SwitchCase(ref active);

        active = isPressed && !wasPressed;
        if (active) _event.Invoke(f);

        wasPressed = isPressed;
    }

    public void UpdateEvent<F, S>(InputDevice controller, UnityEngine.Events.UnityAction<F, S> _event, F f, S s)
    {
        bool active = false;
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out isPressed);

        SwitchCase(ref active);

        active = isPressed && !wasPressed;
        if (active) _event.Invoke(f, s);

        wasPressed = isPressed;
    }

    void SwitchCase(ref bool active)
    {
        switch (pressType)
        {
            case EnumDefinition.PressType.Continuous: active = isPressed; break;
            case EnumDefinition.PressType.Begin: active = isPressed && !wasPressed; break;
            case EnumDefinition.PressType.End: active = !isPressed && wasPressed; break;
        }
    }
}


