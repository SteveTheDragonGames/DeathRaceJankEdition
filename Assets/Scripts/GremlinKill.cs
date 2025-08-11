using UnityEngine;

public class GremlinKill : MonoBehaviour
{
    private bool dead;
    public GameObject tombStone;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (dead) return;

        // Make sure this is your CarKill child/layer
        if (other.gameObject.layer == LayerMask.NameToLayer("CarKill"))
        {
            dead = true;
            GameController.RaiseKillGremlin(); // <-- static access via type
            Instantiate(tombStone, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
