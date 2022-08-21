using System;

namespace ITCampFinalProject.Code.Controls.EventSystem
{
    public class EventCallbackContext
    {
        public readonly bool Started;
        public readonly bool Performed;
        public readonly bool Cancelled;
        public readonly EventPhase Phase;

        private object value;

        public EventCallbackContext(EventPhase eventPhase, object value)
        {
            Phase = eventPhase;
            this.value = value;
            switch (eventPhase)
            {
                case EventPhase.Started:
                {
                    Started = true;
                    Performed = false;
                    Cancelled = false;
                    break;
                }
                case EventPhase.Performed:
                {
                    Started = false;
                    Performed = true;
                    Cancelled = false;
                    break;
                }
                case EventPhase.Cancelled:
                {
                    Started = false;
                    Performed = false;
                    Cancelled = true;
                    break;
                }
                default: throw new ArgumentException("case " + Phase + " is not processed");
            }
        }

        public T ReadValueAs<T>()
        {
            try
            {
                T result = (T) value;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }

        public enum EventPhase
        {
            Started,
            Performed,
            Cancelled
        }
    }
}