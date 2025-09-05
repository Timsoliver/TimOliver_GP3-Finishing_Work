using UnityEngine;

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
         Destroy(this.gameObject);
      }
      else if (collision.gameObject.CompareTag("Player"))
      {
         Destroy(collision.gameObject);
         Destroy(gameObject);
      }
   }
}
