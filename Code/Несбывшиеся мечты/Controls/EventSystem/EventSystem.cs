using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Controls.EventSystem
{
    public class EventSystem
    {
        private HashSet<ActionMap> _actionMaps;
        private HashSet<int> boundKeys = new HashSet<int>();
        public bool ChangesApplied;

        public EventSystem(HashSet<ActionMap> actionMaps = null)
        {
            _actionMaps = actionMaps ?? new HashSet<ActionMap>();
        }

        public void ApplyChanges()
        {
            if (ChangesApplied) return;
            foreach (EventSystemBinding binding in
                     from actionMap in _actionMaps
                     from action in actionMap.Actions
                     from binding in action.Bindings
                     where !boundKeys.Contains(binding.Keycode)
                     select binding)
            {
                boundKeys.Add(binding.Keycode);
            }

            ChangesApplied = true;
        }

        public void AddActionMap(ActionMap actionMap)
        {
            ChangesApplied = false;

            _actionMaps.Add(actionMap);
        }

        public bool RemoveActionMap(ActionMap actionMap)
        {
            ChangesApplied = false;
            return _actionMaps.Remove(actionMap);
        }

        public bool RemoveActionMap(int index)
        {
            ChangesApplied = false;
            return _actionMaps.Remove(_actionMaps.ElementAtOrDefault(index));
        }

        public void OnKeyEvent(object sender, KeyEventArgs args)
        {
            InvokeBoundActions(true, args.KeyValue);
        }

        public void OnMouseEvent(object sender, MouseEventArgs args)
        {
        }

        public void InvokeBoundActions(bool keyboardBinding, int keyCode)
        {
            if (!boundKeys.Contains(keyCode)) return;

            foreach (EventSystemAction action in
                     from actionMap in _actionMaps
                     from action in actionMap.Actions
                     where action.ContainsBindingToKey(keyCode)
                     select action)
            {
                action.InvokeAction(action.ActionPhase,
                    action.Type == typeof(Vector2) ? new Vector2(keyCode, 0f) : (object) keyCode);
            }
        }
    }
}