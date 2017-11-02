using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the libraries are default
//this code considers that the object to be moved is in the middle of the screen.
//When he moves, continues at the middle, and at the screen, we see the terrain moving.

public class carMove : MonoBehaviour
{
    //"v" is a parameter for speed.
	public float v;
    //below, declaring a component of the game object, type: RigidBody, name carBody;
	public Rigidbody carBody;
    //when we use the method Input.mousePosition, it returns a position based on the pixels of the screen.
    //that means (0,0,0) will return with the left-down corner, and something like (800,600,0) for the right-top
    //we want a vector based on the position of the object (the car), not the screen. That's why I'm creating
    //a vector that countains the half of the height and width of the screen. Will be used and explained better later.
    Vector3 auxiliarVector = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    //the vector directionAndSpeed will hold the direction and Speed we want the object to move. But for now, (0,0,0);
    Vector3 directionAndSpeed = new Vector3(0, 0, 0);

    void Start()
	{
        //seting carBody equals to the component Rigidbody of this game object.
		carBody = this.GetComponent<Rigidbody> ();
        //seting velocity as 10. Unitys thought as meters/second
		v = 10f;
	}

	void FixedUpdate()
	{
        //Input.GetMouseButton returns true if the mouse button is pressed. 0=left button
        //1 would mean right button, ant 2 would be the scroll.
        //Here we are verifiying if the button is pressed.
		if (Input.GetMouseButton(0)) {
            //Now is when we calculate the direction. At first we get the mousePosition.
            //directionAndSpeed = Input.mousePosition;

            //Then subtract by the auxiliar vector. This operation will make us to have a vector with the disired direction.
            //witch means it will be (0,0,0) when mouse is in the middle of the screen, something negative if it is
            //in bottom, left, and something positive if it is in top right part of the screen.
            //directionAndSpeed-= auxiliarVector;

            //This I discribed above is a Vector3, right? But the moddule changes deppending on where I click.
            //We just want the Direction. That's why I'm using the Vector3.normalized method, that will return the same
            //direction with moddule equals 1.
            //directionAndSpeed = directionAndSpeed.normalized;

            // then we can multiply for v: the desired velocity.
            //directionAndSpeed*= v;

            //So we picked a direction and multiplied for meters/second, but it is happening every frame.
            //Acctually we want meters/frame don't we? So we need to make
            //(meters/second)*(second/frame). Look's good right? "second" will cancel and we'll have meters/frame. Ok.
            //The methon Time.fixedDeltaTime returns the duration in seconds of the last frame. (second/frame)
            //let's multiply then
            //directionAndSpeed*= Time.fixedDeltaTime;

            //let's put it all together;
            directionAndSpeed = (Input.mousePosition - auxiliarVector).normalized; //direction normalized
            directionAndSpeed*= v * Time.fixedDeltaTime; //velocity fixed

            //Now let's set movimentagion. Two ways of doing this.
            //First is using the RigidBody, that's a specific class for physics, that countain the MovePosition
            //method, that will move the component rb, bind to the game object, to the specified point.
            //look, we want a point, but have a directionAndSpeed. if we sum both we will have the point we must
            //send the object to.
            carBody.MovePosition(transform.position + directionAndSpeed);

            //see the line transform.Translte(direction); right below? is the other way of seting the movimentation.
            //you can comment out the carBody.MovePosition(transform.potition + directionAndSpeed)
            //and start using the line below. This method "translates" the object to a given direction
            //This means it takes the object there. It does not teleport the object, it moves. That's it. Easy to use.

            //transform.Translate(directionAndSpeed);

            //You can print anithyng in the console using the print() method. It was usefull for making some
            //experiments printing like those below:

            //print(directionAndSpeed);
            //print(Input.mousePosition - auxiliarVector);
            //print((Input.mousePosition - auxiliarVector).normalized);
            //print((Input.mousePosition - auxiliarVector).normalized * v);
            //print(transform.position + directionAndSpeed);

        } //end if (Input.GetMouseButton(0))
    } //end fixedUpdate
} //end MonoBehaviour
