using System.Collections.Generic;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Physics
{
    public class RigidBody : PhysicalBody
    {
        public List<Collider> attachedColliders;
        public RigidBody(Transform attachedTransform) : base(attachedTransform)
        {
                
        }

        public delegate void OnCollisionEnter(Collider other);

        public delegate void OnCollisionStay(Collider other);

        public delegate void OnCollisionExit(Collider other);
    }
}