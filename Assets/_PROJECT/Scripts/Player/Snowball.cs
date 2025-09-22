using UnityEngine;
using UnityEngine.UIElements;

public class Snowball : MonoBehaviour
{
   public float lifeTime = 5f;

   private void Start()
   {
      Destroy(gameObject, lifeTime);
   }

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
      {
         Destroy(gameObject);
         return;
      }

      if (collision.gameObject.CompareTag("Player"))
      {
         PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
         if (health != null && health.IsAlive)
         {
            health.Eliminate();
         }
         else
         {
            Debug.LogWarning("[Snowball] Hit a player object but no PlayerHealth found");
         }
         Destroy(gameObject);
      }
   }
   
}
