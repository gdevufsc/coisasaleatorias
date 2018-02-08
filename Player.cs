//Objeto: player
/*
 * limiteInferior = -2
 * limiteSuperior = 5
 * 
*/

using UnityEngine;

public class Player : MonoBehaviour {

	public float speed;
    public Sprite playerSprite;

    public GameObject chickenPrefab;

	public int vidas;
    Quaternion screenBounds = new Quaternion(100, 100, 100, 100);
    
    enum states { aiming, flying, falling, hit };
    states currentState;

    #region Camera
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    #endregion

    //public GameObject goPrefab;

    //public AudioClip audioPlayer;

    // Use this for initialization
    void Start ()
    {
		if(vidas == 1)
        {
			//GameObject.FindGameObjectWithTag ("vidas").GetComponent<GUIText>().text = vidas.ToString()+" life";
		}
		else
        {
			//GameObject.FindGameObjectWithTag ("vidas").GetComponent<GUIText>().text = vidas.ToString()+" lives";
		}
        currentState = states.aiming;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(0, 0, 0.05f * speed);

        //int touchCount = 0;

        switch (currentState)
        {
            case states.aiming:
                if (greatButtonPressed())
                {
                    GameMaster gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

                    if (!gm.isPaused())
                    {
                        Vector3 launchedChickenPos = this.gameObject.transform.position;
                        launchedChickenPos.z -= 1;
                        GameObject clone =
                            (GameObject)Instantiate(chickenPrefab, launchedChickenPos, Quaternion.identity);
                        clone.AddComponent(typeof(projectileBehaviour));
                    }
                    //currentState = states.flying;
                }
                break;

            case states.flying:
                cameraFollow();
                break;

            case states.falling:

                break;

            case states.hit:
                break;
        }
    }

    void cameraFollow()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }
    }

    bool greatButtonPressed()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)//!= TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                if (touch.position.y < 800)
                {
                    return true;
                }
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }

    void outOfBounds()
    {
        //if (this.gameObject.transform.position.y < limiteInferior)
        //{
        //    //float aux = this.gameObject.transform.position.x;
        //    //this.gameObject.transform.position = new Vector3(aux,limiteInferior + 0.2f,0);
        //    this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, playerSpeed / 5, 0);
        //}

        //if (this.gameObject.transform.position.y > limiteSuperior)
        //{
        //    float aux = this.gameObject.transform.position.x;
        //    this.gameObject.transform.position = new Vector3(aux, limiteSuperior - 0.001f, 0);
        //    this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -playerSpeed / 5, 0);
        //}
    }

	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "barrel")
        {
            currentState = states.hit;
        }
	}

	void getPowerGun()
    {
	}

	void getPowerTime()
    {
	}
}