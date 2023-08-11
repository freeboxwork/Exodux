using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class XR_ControllerBase : MonoBehaviour
{
    public static XR_ControllerBase instance;
    List<XRController> controllers = new List<XRController>();
    List<InputDevice> inputDeviceControllers = new List<InputDevice>();
    public bool isControllerReady = false;
    InputDevice leftCont;
    InputDevice rightCont;
    bool gripValue, gripValueLeft;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
    void Start()
    {
        Init();
    }
    void Init()
    {
        SetInputDeviceController(EnumDefinition.ControllerType.RightController); // device     
    }

    // Update is called once per frame
    private void Update()
    {
        if (!leftCont.isValid)
            leftCont = SetInputDeviceController(EnumDefinition.ControllerType.LeftController);
        if (!rightCont.isValid)
            rightCont = SetInputDeviceController(EnumDefinition.ControllerType.RightController);

        if (leftCont.isValid && rightCont.isValid && inputDeviceControllers.Count <= 0)
        {
            inputDeviceControllers.Add(leftCont);
            inputDeviceControllers.Add(rightCont);
            SetControllers(); // scene controller
            isControllerReady = true;

            //foreach(var controller in inputDeviceControllers)
            //{
            //    Debug.Log(" dev name : " + controller.name);
            //}
        }


        //그립버튼 확인
        if (rightCont.TryGetFeatureValue(CommonUsages.gripButton, out gripValue) && gripValue)
        {

        }
        //그립버튼 확인
        if (leftCont.TryGetFeatureValue(CommonUsages.gripButton, out gripValueLeft) && gripValueLeft)
        {

        }
    }


    void SetControllers()
    {
        controllers.Add(UtilityMethod.GetController(EnumDefinition.ControllerType.LeftController));
        controllers.Add(UtilityMethod.GetController(EnumDefinition.ControllerType.RightController));
    }


    InputDevice SetInputDeviceController(EnumDefinition.ControllerType controllerType)
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDeviceCharacteristics controller;
        controller = controllerType == EnumDefinition.ControllerType.LeftController ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(controller, devices);
        if (devices.Count > 0)
        {
            return devices[0];
        }

        else
            return new InputDevice();

    }

    // IsControllerReadyByType 함수 구현
    public bool IsControllerReadyByType(EnumDefinition.ControllerType controllerType)
    {
        if (controllerType == EnumDefinition.ControllerType.LeftController)
        {
            return leftCont.isValid;
        }
        else if (controllerType == EnumDefinition.ControllerType.RightController)
        {
            return rightCont.isValid;
        }
        else
        {
            return false;
        }
    }

    // GetInputDeviceController 함수 구현
    public InputDevice GetInputDeviceController(EnumDefinition.ControllerType controllerType)
    {
        if (controllerType == EnumDefinition.ControllerType.LeftController)
        {
            return leftCont;
        }
        else if (controllerType == EnumDefinition.ControllerType.RightController)
        {
            return rightCont;
        }
        else
        {
            return new InputDevice();
        }
    }
}
