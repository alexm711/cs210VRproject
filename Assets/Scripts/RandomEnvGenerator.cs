using UnityEngine;
using System.Collections;

public class RandomEnvGenerator : MonoBehaviour {


    public int numAsteroids;
    public int radius = 100;
    public Rigidbody[] asteroids;


    // Use this for initialization
    void Start()
    {
        generateSpace();
    }

    void generateSpace()
    {
        for (int i = 0; i < numAsteroids; i++)
        {
            Vector3 position = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            Collider[] hits = Physics.OverlapSphere(position, 10);
            while(hits.Length > 0)
            {
                position = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
                hits = Physics.OverlapSphere(position, 10);
            }

            int j = Random.Range(0, asteroids.Length);
            Rigidbody thing = (Rigidbody)Instantiate(asteroids[j], position, Quaternion.identity);
        }
    }

}
