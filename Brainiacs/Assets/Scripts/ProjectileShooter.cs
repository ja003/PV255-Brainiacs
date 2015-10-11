using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {

    GameObject prefab;
    Vector3 forward = new Vector3(1, 0, 0);

    public float speed = 500;
    public float delayTime = 0.5f;
    public float counter = 0;

	// Use this for initialization
	void Start () {
        prefab = Resources.Load("projectile") as GameObject;
        delayTime = 0.5f;
        speed = 500;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.Q) && counter > delayTime)
        {
            GameObject projectile = Instantiate(prefab) as GameObject;
            Destroy(projectile.gameObject, 1);

            projectile.transform.position = transform.position + forward;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = forward * speed;
            counter = 0;
        }

        counter += 1 * Time.deltaTime;
	}
}
