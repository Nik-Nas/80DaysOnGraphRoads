namespace ITCampFinalProject.Code.Controls.EventSystem
{
    public class EventSystemBinding
    {
        public int Keycode { get; private set; }
        public readonly BindingType bindingType;

        public EventSystemBinding(int keycode, BindingType bindingType)
        {
            Keycode = keycode;
            this.bindingType = bindingType;
        }

        public enum BindingType
        {
            PressAndHold, Press
        }
    }
}