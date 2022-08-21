using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ITCampFinalProject.Code.Controls.EventSystem
{
    public class ActionMap
    {
        public ReadOnlyCollection<EventSystemAction> Actions => _actions.ToList().AsReadOnly();
        private ISet<EventSystemAction> _actions;

        public ActionMap(ISet<EventSystemAction> actions = null)
        {
            _actions = actions ?? new HashSet<EventSystemAction>();
        }

        public void AddAction(EventSystemAction action) => _actions.Add(action);

        public bool RemoveAction(EventSystemAction action) => _actions.Remove(action);

        public bool RemoveAction(int index) => _actions.Remove(_actions.ElementAtOrDefault(index));

        public List<EventSystemBinding> GetAllBindingsToKey(int keyCode)
        {
            List<EventSystemBinding> result = new List<EventSystemBinding>();
            foreach (EventSystemAction action in _actions)
            {
                action.GetBindingsToKey(keyCode).ForEach(binding => result.Add(binding));
            }

            return result;
        }
    }
}