using UnityEngine;
using System.Collections;

public class SpaceGenerator : MonoBehaviour {

    public int numBlocks = 8;
    public int numDebris = 20;
    public int numEnemies = 3;
    public int radius = 20;

    public Rigidbody block;
    public Rigidbody enemy;
    public Rigidbody debris;


	// Use this for initialization
	void Start () {
        generateSpace();
	}
	
    void generateSpace()
    {
        generatePrefab(numBlocks, block);
        generatePrefab(numEnemies, enemy);
        generatePrefab(numDebris, debris);

    }


    void generatePrefab(int num, Rigidbody rb)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 position = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            Instantiate(rb, position, Quaternion.identity);
        }
    }

}
