using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Controls.EventSystem
{
    public class EventSystemAction
    {
        public readonly Action<EventCallbackContext> Action;
        public readonly Type Type;
        public EventCallbackContext.EventPhase ActionPhase = EventCallbackContext.EventPhase.Started;
        public ReadOnlyCollection<EventSystemBinding> Bindings => _bindings.AsReadOnly();
        private List<EventSystemBinding> _bindings;

        public EventSystemAction(EventValueType valueType, List<EventSystemBinding> bindings = null,
            Action<EventCallbackContext> react = null)
        {
            _bindings = bindings ?? new List<EventSystemBinding>();
            Action = react;
            switch (valueType)
            {
                case EventValueType.Vector2:
                    Type = typeof(Vector2);
                    break;
                case EventValueType.ButtonCode:
                    Type = typeof(int);
                    break;
            }
        }

        public void InvokeAction(EventCallbackContext.EventPhase eventPhase, object value)
        {
            if (value?.GetType() == Type)
            {
                switch (eventPhase)
                {
                    case EventCallbackContext.EventPhase.Started:
                        ActionPhase = EventCallbackContext.EventPhase.Performed;
                        break;
                    case EventCallbackContext.EventPhase.Performed:
                        ActionPhase = EventCallbackContext.EventPhase.Cancelled;
                        break;
                    case EventCallbackContext.EventPhase.Cancelled:
                        ActionPhase = EventCallbackContext.EventPhase.Started;
                        break;
                }

                Action?.Invoke(new EventCallbackContext(eventPhase, value));
                return;
            }

            Console.Error.WriteLine($"arg value type({value.GetType()}) isn't equal to valueType({Type})");
        }

        public void AddBinding(EventSystemBinding binding) => _bindings.Add(binding);

        public bool RemoveBinding(EventSystemBinding binding) => _bindings.Remove(binding);

        public List<EventSystemBinding> GetBindingsToKey(int keyCode) =>
            _bindings?.FindAll(binding => binding.Keycode == keyCode);

        public bool ContainsBindingToKey(int keyCode) => _bindings.Any(binding => binding.Keycode == keyCode);


        public enum EventValueType
        {
            Vector2,
            ButtonCode
        }
    }
}