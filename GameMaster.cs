using UnityEngine;

public class GameMaster : MonoBehaviour {

    public GameObject playerPrefab;
    GameObject player;

    public GameObject normalBarrelPrefab;
    public GameObject chickenPrefab;

	int score = 0;
	int previousScore;
    public int velocity;
    public int numBarrels;
    public int luck;
    enum mode { normal, _99 };

    void Start()
    {
        initiateLevel(mode.normal);
    }

	public int getScore(){
		return score;
	}

    public bool isPaused()
    {
        return false;
    }

    void initiateLevel(mode levelModifier)
    {
        switch(levelModifier)
        {
            case mode.normal:
                //Spawn Player
                player = Instantiate(playerPrefab) as GameObject;
                Player playerScript = player.GetComponent<Player>();
                playerScript.speed = velocity;
                playerScript.chickenPrefab = chickenPrefab;

                //Spawn Barrels
                int i;
                int initialRotation = Random.Range(0, 90);
                for (i = 0; i < numBarrels; i++)
                {
                    instantiateBarrel(Quaternion.Euler(0, 0, initialRotation + (i*(360/numBarrels))));
                }
                break;

            case mode._99:
                break;
        }
    }

    void instantiateBarrel(Quaternion rotation)
    {
        GameObject clone = (GameObject)Instantiate(normalBarrelPrefab, Vector3.zero, rotation);
        clone.AddComponent(typeof(BarrelBehaviour));
    }

	// Update is called once per frame
	void Update () {
		if (score != previousScore) {
			previousScore = score;
			GameObject.FindGameObjectWithTag ("score").GetComponent<GUIText>().text = score.ToString ();
		}
	}

	void dodgedEnemy(){
		score += 10;
	}
}
