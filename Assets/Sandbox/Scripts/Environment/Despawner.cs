using UnityEngine;

public class Despawner : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SimplePool.Despawn(collision.gameObject);
    }
}
