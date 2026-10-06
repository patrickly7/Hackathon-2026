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

            float randomX = Random.Range(-20, 20);
            float randomZ = Random.Range(-20, 20);

            Vector3 newPosition = new Vector3(
                randomX,
                collision.transform.position.y,
                randomZ
            );

            collision.transform.position = newPosition;
        }
    }
}