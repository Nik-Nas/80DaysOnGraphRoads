using System;
using System.Collections.Generic;

namespace ITCampFinalProject.Code.GameObjects
{
    public class GameObject
    {
        private HashSet<Component> attachedComponents;

        public bool TryGetComponent(Component type, out Component result)
        {
            return attachedComponents.TryGetValue(type, out result);
        }
    }
}