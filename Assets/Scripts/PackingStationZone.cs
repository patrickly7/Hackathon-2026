using UnityEngine;

public class PackingStationZone : MonoBehaviour
{
    public AudioSource packingSFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            packingSFX.Play();

            var player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.HandleCardScoreCalculation();
            }
        }
    }
}