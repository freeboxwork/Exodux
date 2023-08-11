

public class EnumDefinition
{
    public enum ControllerType
    {
        LeftController,
        RightController
    }
    public enum PressType
    {
        Continuous,
        Begin,
        End
    }

    /*
    secondaryButton [LeftHand XR Controller] = Y button
    primaryButton [LeftHand XR Controller] = X button
    secondaryButton [RightHand XR Controller] = B button
    primaryButton [RightHand XR Controller] = A button
    */
    public enum ControllerEvent
    {
        TriggerButton,
        PrimaryButton,
        SecondaryButton

    }
}
