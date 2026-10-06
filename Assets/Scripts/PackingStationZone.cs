using UnityEngine;

public class PackingStationZone : MonoBehaviour
{
    public int points = 10;
    public AudioSource packingSFX;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Something collided with the score zone: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED SCORE ZONE!");

            GameManager.Instance.AddScore(points);

            packingSFX.Play();

            float randomX = Random.Range(-8, 8);

            Vector3 newPosition = new Vector3(
                randomX,
                9.5f,
                -18f
            );

            collision.transform.position = newPosition;
        }
    }
}