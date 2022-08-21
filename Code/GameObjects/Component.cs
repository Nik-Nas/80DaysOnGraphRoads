namespace ITCampFinalProject.Code.GameObjects
{
    public abstract class Component
    {
        public string name { get; protected set; }
        public GameObject gameObject { get; protected set; }
    }
}