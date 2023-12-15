using UnityEngine.Events;

namespace _Scripts
{
    public class EventManager
    {
        public static UnityEvent<float> OnPlayerDamaged;
        public static UnityEvent OnPlayerLost;
        
        public static void SendPlayerDamaged(float damage)
        {
            OnPlayerDamaged.Invoke(damage);
        }

        public static void SendPlayerLost()
        {
            OnPlayerLost.Invoke();
        }
        
    }
}
