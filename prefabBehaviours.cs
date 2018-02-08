using UnityEngine;

public class BarrelBehaviour : MonoBehaviour {

	void Start(){
	}

	void Update() {
	}

	void OnCollisionEnter(){
		Destroy (gameObject);
		GameObject.FindGameObjectWithTag ("GM").gameObject.SendMessage ("normalBarrel");
        Debug.Log("Collision!", gameObject);
	}
}

public class projectileBehaviour : MonoBehaviour
{
    public Vector2 destroyPosition = new Vector2(20, 10);

    void Start()
    {
        int force = 10;
        GameObject launcherBarrel = GameObject.FindGameObjectWithTag("LauncherBarrel");
        float rotation = launcherBarrel.transform.rotation.eulerAngles.z + 90
            + (launcherBarrel.GetComponent<Player>().speed / 10);
        rotation = Mathf.Deg2Rad * rotation;
        Debug.Log(rotation);
        gameObject.GetComponent<Rigidbody2D>().velocity = 
            new Vector3(Mathf.Cos(rotation)*force, Mathf.Sin(rotation)*force, 0);
    }

    void Update()
    {
        if (transform.position.y > destroyPosition.y || transform.position.x > destroyPosition.x
            || transform.position.y < -destroyPosition.y || transform.position.x < -destroyPosition.x)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);
        Debug.Log("Collision!", gameObject);
        Destroy(gameObject);
    }
}