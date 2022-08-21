using System.Collections.Generic;
using ITCampFinalProject.Code.Physics;

namespace ITCampFinalProject.Code.Несбывшиеся_мечты.Physics
{
    public class RigidbodyManager
    {
        public readonly List<RigidBody> allRigidBodies = new List<RigidBody>();
        
        public void UpdateRigidBodies()
        {
            allRigidBodies.ForEach(rigidBody => rigidBody.UpdateValues());
        }
    }
}