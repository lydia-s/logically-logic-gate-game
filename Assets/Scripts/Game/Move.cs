using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	
	float fall = 0;
	private float fallSpeed;
    public bool inputLeft = false;
    public bool inputDown = false;
    public bool output = false;
    public Sprite leftdown_sprite, down_sprite, left_sprite, noInput_sprite;

    public Animator animator;






    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        fallSpeed = Game.fallSpeed;//GameObject.Find("GridObject").GetComponent<Game>()




    }
	
	// Update is called once per frame
	void Update () {
		CheckUserInput();
        
	}
	
	
	
	void CheckUserInput(){
		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
		}
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
		}
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Time.time - fall >= fallSpeed){
            MoveDown();	
		}
	}
    /*
     * MoveLeft moves a gate left
     */
    void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
        if (CheckIsValidPosition(transform.position))
        {


        }
        else
        {

            transform.position += new Vector3(1, 0, 0);

        }

    }
    /*
    * MoveRight moves a gate right
    */
    void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);

        if (CheckIsValidPosition(transform.position))
        {


        }
        else
        {
            transform.position += new Vector3(-1, 0, 0);
        }

    }
    /*
     * MoveDown moves an object down if the down arrow is pressed or if fall equals the time at the begining of the current frame.
     * Once a gate is not in a valid position it is considered 'placed' and a certain number of steps will be taken.
     * PlayDropSound will be called so the appropriate sound effect will be made.
     * It will check if the block is above the grid in the case of a Game Over
     * 
     */
    void MoveDown()
    {
        transform.position += new Vector3(0, -1, 0);

        if (CheckIsValidPosition(transform.position))
        {


        }
        else
        {

            transform.position += new Vector3(0, 1, 0);
            FindObjectOfType<AudioManager>().PlayDropSound();//play drop a gate sound
            if (FindObjectOfType<Game>().CheckIsAboveGrid(transform.position))
            {
                FindObjectOfType<Game>().GameOver();
            }
            
            FindObjectOfType<Game>().AddToGrid(this);//add this block to the grid array once it has dropped

            UpdateBlockInputOutput(transform.position, this.name);//check what a block's inputs are which correspond to the surrounding blocks

            FindObjectOfType<Game>().GetResults(transform.position);

            FindObjectOfType<Game>().DeleteRow();//Delete row if matches certain circumstances

            enabled = false;
            FindObjectOfType<Game>().SpawnNextBlock();//instantiate a prefab





        }


        fall = Time.time;

    }
    /*
    * UpdateBlockInputOutput calls CheckInput on a gate to set the gate's inputs
    * It then calls CheckOutput to set the outputs
    * Then it calls ChangeState to update the sprite according to the inputs and outputs
    */
    public void UpdateBlockInputOutput(Vector2 pos, string name) { /*this calls the check input, check output and change state methods combining their functionalities*/
        CheckInput(pos);
        output = CheckOutput(inputLeft, inputDown, name);
        ChangeState();


    }

    /*
     * CheckIsValidPosition firstly rounds the position of incoming coordinates by calling Round.
     * Then CheckIsInsideGrid is fed in the rounded position. If it is not inside the grid this method returns false
     * If it is inside the grid, the method calls GetTransformAtGridPosition which returns the transform at a certain position 
     * Then checks if the transform returned by GetTransformAtGridPosition does not equal the transform attached to the current game object
     * 
     */
    bool CheckIsValidPosition(Vector2 pos){
			
			FindObjectOfType<Game>().Round(pos);
			
			if(FindObjectOfType<Game>().CheckIsInsideGrid(pos)== false){
				return false;
			}
			if(FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform){
				return false;
			} 
		
		return true;
		
	}
    /*
    * CheckInput sets the left and down inputs of a gate.
    * It has to check whether it is on the left or bottom side of the grid in case it gets its inputs from the actual grid
    * Otherwise it can get its inputs from the outputs of the gate left to it and/or below it
    */
    public void CheckInput(Vector2 pos)
    {

        if (!FindObjectOfType<Game>().IsNotOnLeftEdge(transform.position) && !FindObjectOfType<Game>().IsNotOnBottomEdge(transform.position))
        { /*if both inputs from the edge
                    of the grid*/

            inputLeft = FindObjectOfType<Game>().AssignFirstLeftInput(transform.position);
            inputDown = FindObjectOfType<Game>().AssignFirstDownInput(transform.position);
        }
        else if (FindObjectOfType<Game>().IsNotOnLeftEdge(transform.position) && !FindObjectOfType<Game>().IsNotOnBottomEdge(transform.position))
        {
            inputDown = FindObjectOfType<Game>().AssignFirstDownInput(transform.position);
            inputLeft = FindObjectOfType<Game>().HasLeftInput(transform.position);

        }
        else if (!FindObjectOfType<Game>().IsNotOnLeftEdge(transform.position) && FindObjectOfType<Game>().IsNotOnBottomEdge(transform.position))
        {
            inputLeft = FindObjectOfType<Game>().AssignFirstLeftInput(transform.position);
            inputDown = FindObjectOfType<Game>().HasDownInput(transform.position);
        }
        else
        {
            inputLeft = FindObjectOfType<Game>().HasLeftInput(transform.position);
            inputDown = FindObjectOfType<Game>().HasDownInput(transform.position);
        }
        
    }
    /*
     * CheckOutput takes in a gate as a parameter
     * It uses a switch statement to check the gate's name
     * It then takes the gate's inputs and, depending on its name, applies certain boolean operators to generate an output.
     * 
     */
    public bool CheckOutput(bool l, bool d, string name) { //take inputLeft and inputDown and produce an output, currently only OR
        switch (name)
        {
            case "OR":
                if (l == true || d == true)
                {
                    return true;

                }
                else
                {
                    return false;
                }

            case "AND":
                if (l == true && d == true)
                {
                    return true;

                }
                else
                {
                    return false;
                }

            case "NAND":

                if (l == true && d == true)
                {
                    return false;

                }
                else
                {
                    return true;
                }
            case "NOR":

                if (l == false && d == false)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            case "XOR":

                if ((l == false && d == false) || (l == true && d == true))
                {
                    return false;

                }
                else
                {
                    return true;
                }
        }
        return false;
        
    }
    /* ChangeState changes the sprite of the gate depending on what its inputs and outputs are*/
    public void ChangeState() {//replaces the sprite if output changes
        

        if (inputLeft == true && inputDown == true)
        {

            this.gameObject.GetComponent<SpriteRenderer>().sprite = leftdown_sprite;
        }
        if (inputLeft == false && inputDown == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = down_sprite;
        }
        if (inputLeft == true && inputDown == false)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = left_sprite;
        }
        if (inputLeft == false && inputDown == false)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = noInput_sprite;
        }

    




    }
    /*
     * DestroyAnimation plays the animation when gates are destroyed when a row is cleared
     */
    public void DestroyAnimation()
    {
        
        animator.Play("destroy_animation");
    }
    






}
