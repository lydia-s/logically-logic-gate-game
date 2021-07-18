using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    //static variables
    public static int gridWidth = 3;
    public static int gridHeight = 6;
    public static float fallSpeed = 1f;
    public static string gameMode;
    public static Transform[,] grid= new Transform[gridWidth, gridHeight];
    //non-static
    public int score = 0;
    public bool IsGameOver = false;
    public GameObject scorePopup;
    public static string popupString = "Reward";
    private GameObject previewGate;
    private GameObject nextGate;
    private bool gameStarted = false;
    private Vector2 previewGatePos = new Vector2(8f, 5f);
    
    

    // Use this for initialization
    void Start()
    {
        
        

        SpawnNextBlock();

        
    }


    /*
     * ScoreAnimation instantiates a score object which has an animation attached to it, and then destroys the object after the animation is finished
     */
    void ScoreAnimation()
    {
        
        var x = Instantiate(scorePopup, new Vector2(0, 0), Quaternion.identity);
        x.transform.SetParent(GameObject.Find("GameObject").transform, false);
        x.name = popupString;
        Destroy(x, x.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); //destroys it after the length of the animation is over

    }


    /*
     * CheckIsAboveGrid checks if the position of a gate is above the grid
     */
    public bool CheckIsAboveGrid(Vector2 pos)
    {

        if (pos.y > gridHeight - 1)
        {
            
            return true;
        }
        else {
            return false;
        }

        

    }

    /*
    * GameOver is called when the game is over and it saves the time the game ends.
    * It sets the time and score that were accrued so they can be displayed to the player.
    * If the score is higher than the high score for that level it saves the score.
    * Loads the GameOver scene.
    */
    public void GameOver()
    {
        IsGameOver = true;
        string saveTime = FindObjectOfType<Timer>().currentTime;
        PlayerPrefs.SetString("time", saveTime);
        PlayerPrefs.SetInt("yourscore", score);
        if (gameMode == "beginner" &&  score > PlayerPrefs.GetInt("beginnerHighscore"))
        {

            PlayerPrefs.SetInt("beginnerHighscore", score);
        }
        if (gameMode == "intermediate" && score > PlayerPrefs.GetInt("intermediateHighscore"))
        {

            PlayerPrefs.SetInt("intermediateHighscore", score);
        }
        if (gameMode == "challenging" && score > PlayerPrefs.GetInt("challengingHighscore"))
        {

            PlayerPrefs.SetInt("challengingHighscore", score);
        }
        if (gameMode == "custom" && score > PlayerPrefs.GetInt("customHighscore"))
        {

            PlayerPrefs.SetInt("customHighscore", score);
        }



        SceneManager.LoadScene("GameOver");
    }


    /*
     * IsRowComplete checks if gates fill the entirety of a row
     */
    public bool IsRowComplete (int y){
        for(int x =0; x <gridWidth; ++x){//iterating through all x values, each row basically
            if(grid[x, y] == null)
            {

                return false;
            }   
        }
        
        return true;
    }


    /*
     * DeleteBlockAt iterates through all x positions for gates with the same y position and destroys the game objects at these positions
     * This results in destroying a row as gates in the same row will have the same y position but different x positions
     * It also calls the destroy animation from the Gate class on each gate
     */
    public void DeleteBlockAt(int y) {
        for (int x = 0; x < gridWidth; ++x) { //iterating through all x values, each row basically
            grid[x, y].gameObject.GetComponent<Move>().DestroyAnimation();
            Destroy (grid[x,y].gameObject, grid[x, y].gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            grid[x, y] = null;

        }
        
        
    }

    /*
     * MoveRowDown decreases the y position of all the gates in a row by 1
     */
    public void MoveRowDown(int y) {
        for (int x =0; x <gridWidth; ++x) { //iterating through all x values, each row basically
            if (grid[x, y] != null) {

                grid[x, y - 1] = grid[x, y];//block stays x position, shifts down 1 y position
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }

    }
    /*
     * MoveAllRowsDown calls MoveRowDown for every single row in the grid
     */
    public void MoveAllRowsDown(int y) {
        for (int i = y; i < gridHeight; ++i) {
            MoveRowDown(i);
        }

        
    }


    /*
     * DeleteRow checks if IsRowComplete abd DoesOutputMatch return true. 
     * If this is true it calls ClearRowSound to play the corresponding sound effect.
     * Then it calls UpdateTheGrid and DeleteBlockAt to clear the corresponding grid and gate objects.
     * The method increments score and calls to ScoreAnimation to play the corresponding animation.
     * It then calls MoveAllRowsDown so that the gates above the deleted ones are in the right place
     */
    public void DeleteRow() {
        
        for (int y=0; y<gridHeight;++y) {//iterate through all y values 
            if (IsRowComplete(y) && DoesOutputMatch(y)) {
                FindObjectOfType<AudioManager>().PlayClearRowSound();//play sound effect
                GetComponent<GridGenerator>().UpdateTheGrid(y);//update the grid i.e. destroy the corresponding grid objects to the deleted row

                DeleteBlockAt(y);//delete the block at this y value
                //here now also delete blocks in grid array--call UpdateGrid method in GridGenerator here
                
                
                score++;
                ScoreAnimation();
                
                MoveAllRowsDown(y + 1); //for all blocks above the current y value blocks move the row down



                //increment score variable
                --y;
            }
            


        }


    }

    /*
     * DoesOutputMatch checks that the output of the last block matches far right gridblock and return true if  this is correct. 
     */
    public bool DoesOutputMatch(int y) //check from right array
    {
    
        bool rightOutput = GetComponent<GridGenerator>().rightGrid[y].GetComponent<GridComponent>().outputType;
        if (grid[gridWidth - 1, y] != null && (grid[gridWidth - 1, y].gameObject.GetComponent<Move>().output == rightOutput)) /*if the last block exists and equals 
            the corresponding grid component*/
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /*
    * SpawnNextBlock spawns a gate above the grid when the game starts, and also spawns a preview gate to the side of the grid.
    * 
    * After the game has started the preview gate becomes the next gate that appears at the top of the grid and a new gate is spawned to become the preview gate.
    */
    public void SpawnNextBlock(){

        if (!gameStarted)
        {
            gameStarted = true;
            string firstBlock = GetRandBlock();
            nextGate = (GameObject)Instantiate(Resources.Load(firstBlock, typeof(GameObject)), new Vector2(gridWidth - (gridWidth - 1), gridHeight), Quaternion.identity);
            nextGate.name = firstBlock;
            string secondBlock = GetRandBlock();
            previewGate = (GameObject)Instantiate(Resources.Load(secondBlock, typeof(GameObject)), previewGatePos, Quaternion.identity);
            previewGate.name = secondBlock;
            previewGate.GetComponent<Move>().enabled = false;
        }
        else
        {

            previewGate.transform.localPosition = new Vector2(gridWidth - (gridWidth - 1), gridHeight);
            nextGate = previewGate;
            nextGate.GetComponent<Move>().enabled = true;
            //instantiate next preview block
            string secondBlock = GetRandBlock();
            previewGate = (GameObject)Instantiate(Resources.Load(secondBlock, typeof(GameObject)), previewGatePos, Quaternion.identity);
            previewGate.name = secondBlock;
            previewGate.GetComponent<Move>().enabled = false;


        }

        
        

    }

    /*
    * CheckIsInsideGrid checks that a gate's x and y coordinates are more than or equal to 0.
    * It also checks that its x coordinate is less than the grid width
    */
    public bool CheckIsInsideGrid(Vector2 pos){
		Vector2 p = Round(pos);
		return ((int)p.x >= 0 && (int)p.x < gridWidth && (int)p.y >=0);
	}

    /*
     * Round rounds a Vector2 coordinate up to the nearest whole number
     */
    public Vector2 Round(Vector2 pos){
		return new Vector2 (Mathf.Round(pos.x), Mathf.Round(pos.y));
	}

    /*
     *  AddToGrid adds a gate object to the grid
     */
    public void AddToGrid(Move piece){
		Transform p = piece.transform;
	    Vector2 pos = Round(p.position);
		if(pos.y < gridHeight){
            grid[(int)pos.x, (int)pos.y] = p;
		}
		
	}

    /*
    * GetTransformAtGridPosition returns a Transform position in the Transform grid array if it exists
    */
    public Transform GetTransformAtGridPosition(Vector2 pos){
		if(pos.y > gridHeight -1){
			return null;
		}else{
			return grid[(int)pos.x, (int)pos.y];
		}
	}
    /*
    * GetRandBlock returns a random string which is a gate's name
    */
    string GetRandBlock(){
		int randBlock = Random.Range(1,6);
		string randBlockName = "AND";
		switch(randBlock){
			case 1:
			    randBlockName = "OR";
			    break;
			case 2:
			    randBlockName = "AND";
			    break;
            case 3:
                randBlockName = "NAND";
                break;
            case 4:
                randBlockName = "NOR";
                break;
            case 5:
                randBlockName = "XOR";
                break;
        }
			return randBlockName;
		}
    /*
     * IsNotOnBottomEdge checks if a gate is not on the bottom edge of the grid
     */
    public bool IsNotOnBottomEdge(Vector2 pos) { //Checks if a block is on the on the bottom edge
        if ((int)pos.y != 0)
        {
            return true;
        }
        else {
            return false;
        }
    }
    /*
     * IsNotOnLeftEdge checks if a gate is not on the left edge of the grid
     */
    public bool IsNotOnLeftEdge(Vector2 pos) //Checks if a block is on the left edge
    {
        if ((int)pos.x != 0)
        {
            return true;
        }
        
        else
        {
            return false;
        }
    }
    /*
     * HasLeftInput checks if the gate to the left of it to it has an output and then returns that output
     * If it has no left input it defaults to false
     */
    public bool HasLeftInput(Vector2 pos) //this just returns the output of the left input block
    {
        if (grid[(int)pos.x - 1, (int)pos.y] != null)//if an object exists to the left return the output of that object
        {
            return grid[(int)pos.x - 1, (int)pos.y].gameObject.GetComponent<Move>().output;
        }
        else {
            return false; //if no object exists to the left just return false
        }
       
    }
    /*
     * HasDownInput returns the output of the gate below it
     * Doesn't need to check if there is a gate below it as a gate has to be dropped onto something 
     * Also if it reaches this part of the code it means there is no grid component under it so it has to be another gate under it
     */
    public bool HasDownInput(Vector2 pos)//this just returns the output of the bottom input block
    {
        return grid[(int)pos.x, (int)pos.y-1].gameObject.GetComponent<Move>().output;
    }
    /*
     * returns the output of a grid componenet 
     * Its position in the list corresponds to the y coordinate of the gate object that is 'receiving' this output as a left input
     */
    public bool AssignFirstLeftInput(Vector2 pos) {//now we get from GridGenerator array instead

        bool leftOutput = GetComponent<GridGenerator>().leftGrid[(int)pos.y].GetComponent<GridComponent>().outputType;
        return leftOutput;

    }
    /*
     * returns the output of a grid componenet 
     * Its position in the list corresponds to the x coordinate of the gate object that is 'receiving' this output as a bottom input
     */
    public bool AssignFirstDownInput(Vector2 pos)//now we get from GridGenerator array instead
    {
        //find x value in array
        bool downOutput = GetComponent<GridGenerator>().downGrid[(int)pos.x].GetComponent<GridComponent>().outputType;
        return downOutput;
        
    }
    
    /*GetResults is called once a gate is dropped and checks any gates to the right of the recently dropped gates, if there are any gates to the right,
     it then also checks if there are any gates to the right or above any blocks that are to the right of the dropped gate by recursively calling GetResults*/
    public void GetResults(Vector2 pos) {
        if ((int)pos.x + 1 < gridWidth && grid[(int)pos.x + 1, (int)pos.y] != null) { //if there is a block to the right

            GameObject rightOb = grid[(int)pos.x + 1, (int)pos.y].gameObject; //Get the game object to the right of the current gate
            Move rightBlock = (Move) rightOb.GetComponent(typeof(Move)); //Access Move method on this particular gate 
            rightBlock.UpdateBlockInputOutput(rightOb.transform.position, rightOb.name);//Update the gate to the right of the current gate
            GetResults(rightOb.transform.position); //Recursively call GetResults on the right object
            if ((int)pos.x + 1 < gridWidth && (int)pos.y + 1 < gridWidth && grid[(int)pos.x + 1, (int)pos.y + 1] != null)//If there is a gate above the right gate
            {
                GameObject topRightOb = grid[(int)pos.x + 1, (int)pos.y + 1].gameObject; //Get the gate above the gate to the right of the current gate
                Move topRightBlock = (Move)topRightOb.GetComponent(typeof(Move));//Access Move method on this particular gate 
                topRightBlock.UpdateBlockInputOutput(topRightOb.transform.position, topRightOb.name); //Update the gate above the gate to the right of the current gate
                GetResults(topRightOb.transform.position);//Recursively call GetResults on the top right object

            }
            else {
                return;
            }
        }
        /*Following code is to continuously check further gates above following gates to the right of the gate that was initially checked*/
        if ((int)pos.y + 1 < gridWidth && grid[(int)pos.x, (int)pos.y + 1] != null) //Check if there is a block above the current block
        {
            GameObject topOb = grid[(int)pos.x, (int)pos.y + 1].gameObject;//Get the game object above the current gate
            Move topBlock = (Move)topOb.GetComponent(typeof(Move));//Access Move method on this particular gate 
            topBlock.UpdateBlockInputOutput(topOb.transform.position, topOb.name);//Update the game object above the current gate
            GetResults(topOb.transform.position);//Recursively call GetResults on the top object
        }
        else {
            return;
        }
    }
    

    



}

