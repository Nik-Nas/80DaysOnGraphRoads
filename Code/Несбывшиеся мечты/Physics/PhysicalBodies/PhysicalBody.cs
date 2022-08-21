using System.Diagnostics;
using ITCampFinalProject.Code.GameObjects;
using ITCampFinalProject.Code.WorldMath;
//ReSharper disable InconsistentNaming
namespace ITCampFinalProject.Code.Physics
{
    public abstract class PhysicalBody : Component
    {
        /// <summary>
        /// standard physical constant of gravity of Earth
        /// </summary>
        public const float g = 9.80665f;
        public readonly Transform attachedTransform;
        /// <summary>
        /// velocity of object along x/y axes in m/s
        /// </summary>
        public Vector2 velocity
        {
            get => _velocity;
            private set => _velocity = value >= Vector2.zero ? value : Vector2.zero;
        }
        /// <summary>
        /// returns speed from velocity
        /// </summary>
        public float speed => velocity.Length;
        /// <summary>
        /// mass of object in kilograms
        /// </summary>
        public float mass = 1f;
        public float frictionCoefficient
        {
            get => _frictionCoefficient;
            set => _frictionCoefficient = AdvancedMath.Clamp(value, 0.1f, 5f);
        }

        private float _frictionCoefficient = 0.1f;
        private Vector2 _velocity;

        #region Methods
        /// <summary>
        /// Applies force to rigidbody, using <see cref="mass"/>
        /// </summary>
        /// <param name="force">force in kilos to apply to rigidbody</param>
        public void AddForce(Vector2 force)
        {
            velocity += force / mass;
        }   

        public void UpdateValues()
        {
            velocity -= Vector2.one * (frictionCoefficient * g);
            attachedTransform.MoveInDirection(velocity.Normalized);
        }
        #endregion
        
        public PhysicalBody(Transform attachedTransform)
        {
            this.attachedTransform = attachedTransform;
        }
    }
}