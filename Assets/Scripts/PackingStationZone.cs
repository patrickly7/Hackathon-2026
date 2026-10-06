using UnityEngine;

public class PackingStationZone : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the score zone: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED SCORE ZONE!");

            GameManager.Instance.AddScore(points);
        }
    }
}