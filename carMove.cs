using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the libraries are default
//this code considers that the object to be moved is in the middle of the screen.
//When he moves, continues at the middle, and at the screen, we see the terrain moving.

public class carMove : MonoBehaviour
{
    //"a" is a parameter for speed. Maybe could be an int
	public float a;
    //below, declaring a component of the game object, type: RigidBody, name rb;
	public Rigidbody rb;
    //when we use the method Input.mousePosition, it returns a position based on the pixels of the screen.
    //that means (0,0,0) will return with the left-down corner, and something like (800,600,0) for the right-top
    //we want a vector based on the position of the object (the car), not the screen. That's why I'm creating
    //a vector that countains the half of the height and width of the screen. Will be used and explained better later.
    Vector3 ajustCordinatesWithScreenParameters = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    //the vector directionAndSpeed will hold the direction and Speed we want the object to move. But for now, (0,0,0);
    Vector3 directionAndSpeed = new Vector3(0, 0, 0);

    void Start()
	{
        //seting rb equals to the component Rigidbody of this game object.
		rb = this.GetComponent<Rigidbody> ();
        //seting velocity as 10. Unitys thought as meters/second
		a = 10f;
	}

	void FixedUpdate()
	{
        //Input.GetMouseButton returns true for the mouse button is pressed, and false otherwise. 0=left button
        //1 would mean right button, ant 2 would be the scroll.
        //Here we are verifiying if the button is pressed, therefore.
		if (Input.GetMouseButton(0)) {
            //Now is when we calculate the direction. Well, at first we catch the mousePosition, ok.
            //directionAndSpeed = Input.mousePosition;

            //Then subtract by the auxiliar vector. This operation will make us to have a vector with the disired direction.
            //witch means it will be (0,0,0) when mouse is in the middle of the screen, something negative if it is
            //in bottom, left, and something positive if it is in top right part of the screen. Ok
            //directionAndSpeed= directionAndSpeed - ajustCordinatesWithScreenParameters;

            //This I discribed above is a Vector3, right? But the moddule changes deppending on where I click.
            //We just want the Direction. That's why I'm using the Vector3.normalized method, that will return the same
            //direction with moddule equals 1. Ok
            //directionAndSpeed = directionAndSpeed.normalized;

            // then we can multiply for a: the desired velocity, and obtain the
            //right direction and moddule. Ok
            //directionAndSpeed = directionAndSpeed * a;

            // but this is happening every frame, so, does this moddule make sense?
            //does it make sense to increment the position every frame using velocity in meters/second?
            //because what we will do after this line is increment the position using this vector we are building!
            //So, think with me. We picked a direction, and multiplied for meters/second, but is happening every frame.
            //Acctually we want meters/frame don't we? How do we obtain that? What do you think of that:
            //(meters/second)*(second/frame). Look's good right? second will cancel and we'll have meters/frame. Ok.
            //The methon Time.deltaTime returns the duration in seconds of the last frame. And I'm using fixedDeltaTime
            //because, well, they might have fixed something on the original Time.deltaTime, so let's use it.
            //So this method will return exactly the (second/frame) we ware needing. End of this line haha.
            //directionAndSpeed = directionAndSpeed * Time.fixedDeltaTime;

            //let's put it all together;
            directionAndSpeed = (Input.mousePosition - ajustCordinatesWithScreenParameters).normalized * a * Time.fixedDeltaTime;

            //Ok, now that we have directionAndSpeed, let's set move. there are two ways of doing this I know.
            //First is using the RigidBody, that's a specific class for physics, that countain the MovePosition
            //method, that will move the component rb, bind to the game object, to the specified point.
            //That means the method will move the object to a point. Verry good.
            //So we need a point, but we acttualy have a Direction. Maybe you'll say WHAT?
            //Why didn't we create a point then, and talked all that stuff off direction?
            //Don't worry. Is very easy to transform the direction in a point, and
            //the only method I've found in class Input I could use was the Input.mousePosition, that's based on
            //the game screen, not the cordinates in game's System. Ok.
            //As I said, is very easy to get the point. Just pick the point the object is with "transform.position"
            //then sum this with the direction. let's say we had the position (0,0,0) and direction is (1,0,0)
            //then the point will be (1,0,0). Next frame would be position (1,0,0) and direction (1,0,0) so the point
            //would be (2,0,0), and so on.
            rb.MovePosition(transform.position + directionAndSpeed);

            //see the line transform.Translte(direction); right below? is the other way of seting the movimentation.
            //you could perfectly comment out the rb.MovePosition(transform.potition + directionAndSpeed)
            //and start using the line below. This method "translates" the object to a given direction
            //This means it takes the object there. It does not teleport the object, it moves. That's it. Easy to use.

            //transform.Translate(directionAndSpeed); 

            //You can print anithyng in the console using the print() method. It was usefull for making some
            //experiments printing like just Input.mousePosition, then 
            //Input.mousePosition-ajustCordinatesWithScreenParameters. I'll sujest some experiments bellow. Comment
            //in an out according to your wish.

            //print(directionAndSpeed);
            //print(Input.mousePosition - ajustCordinatesWithScreenParameters);
            //print((Input.mousePosition - ajustCordinatesWithScreenParameters).normalized);
            //print((Input.mousePosition - ajustCordinatesWithScreenParameters).normalized * a);
            //print(transform.position + directionAndSpeed);
        }
    }
}
