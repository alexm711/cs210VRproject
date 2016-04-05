using UnityEngine;
using System.Collections;

public class SpaceGenerator : MonoBehaviour {

    public int numTargets = 8;
    public int numDebris = 20;
    public int numEnemies = 3;
    public int radius = 20;

    public Rigidbody target;
    public Rigidbody enemy;
    public Rigidbody debris;

    public float forceConstant = 10f;

	// Use this for initialization
	void Start () {
        generateSpace();
	}
	
    void generateSpace()
    {
        generatePrefab(numTargets, target, 0);
        generatePrefab(numEnemies, enemy, 0);
        generatePrefab(numDebris, debris, forceConstant);
    }

    void generatePrefab(int num, Rigidbody rb, float force)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 position = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            Rigidbody thing = (Rigidbody) Instantiate(rb, position, Quaternion.identity);
            Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * forceConstant;
            thing.AddForce(randomForce);
        }
    }


}
