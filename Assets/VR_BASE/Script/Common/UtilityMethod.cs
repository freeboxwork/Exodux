using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public static class UtilityMethod
{
    public static XRController GetController(EnumDefinition.ControllerType controllerType)
    {
        var obj = GameObject.FindGameObjectWithTag(controllerType.ToString());
        if (obj != null)
        {
            if (obj.TryGetComponent<XRController>(out XRController cont))
                return cont;
            else
            {
                Debug.LogError("XRController 컴포넌트가 없음");
                return null;
            }
        }
        else
        {
            Debug.LogError(controllerType.ToString() + " Tag 게임 오브젝트가 없음");
            return null;
        }

    }
}
