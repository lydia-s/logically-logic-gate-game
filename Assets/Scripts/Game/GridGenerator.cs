using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GridComponent gridComponent;
    public List<GameObject> leftGrid = new List<GameObject>();
    public List<GameObject> rightGrid = new List<GameObject>();
    public List<GameObject> downGrid = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateFirstInputsOutputs();

    }

    public void GenerateFirstInputsOutputs()

    {
        for (int x = 0; x < Game.gridWidth; ++x)//iterate through all indexes in grid array
        {

            for (int y = 0; y < Game.gridHeight; ++y)
            {

                if (y == 0)//if y = 0 or x= 0 for any coordinate
                {

                    string name0 = GenRandGridBlock();
                    var block0 = Instantiate(Resources.Load(name0, typeof(GameObject)), new Vector2(x, y - 1), Quaternion.identity);
                    GridComponentOutput(name0, (GameObject)block0);
                    block0.name = name0;
                    downGrid.Add((GameObject)block0);

                }
                if (x == 0)
                {
                    string name1 = GenRandGridBlock();
                    var block1 = Instantiate(Resources.Load(name1, typeof(GameObject)), new Vector2(x - 1, y), Quaternion.identity);
                    GridComponentOutput(name1, (GameObject)block1);//assign output
                    block1.name = name1;//rename clone   
                    leftGrid.Add((GameObject)block1);//add to grid


                }
                if (x == Game.gridWidth - 1)
                {
                    string name2 = GenRandGridBlock();
                    var block2 = Instantiate(Resources.Load(name2, typeof(GameObject)), new Vector2(x + 1, y), Quaternion.identity);
                    GridComponentOutput(name2, (GameObject)block2);//assign output
                    block2.name = name2;//rename clone
                    rightGrid.Add((GameObject)block2);//add to grid


                }
            }
        }

    }
    public void UpdateTheGrid(int indexRemoved)
    {
        
        if (indexRemoved == 0)//if the bottom row is deleted make the outputs of the deleted row the bottom grid
        {
            Debug.Log("went in here");
            MergeRowsDown();
        }
        DeleteGridBlocksAt(indexRemoved);
        SpawnNewBlocks();
    }
    string GenRandGridBlock()
    {
        int randBlock = Random.Range(1, 3);
        string randBlockName;
        if (randBlock == 1)
        {
            randBlockName = "1";
            return randBlockName;
        }
        else {
            randBlockName = "0";
            return randBlockName;
        }
        
    }
    public void GridComponentOutput(string name, GameObject block) {

        if (name == "1")
        {
            block.GetComponent<GridComponent>().outputType = true;
        }
        else {
            block.GetComponent<GridComponent>().outputType = false;
        }

    }

    public void AddBlockToRightArray()//add a block to the right array
    {
        string name = GenRandGridBlock();
        var block = Instantiate(Resources.Load(name, typeof(GameObject)), new Vector2(Game.gridWidth, Game.gridHeight-1), Quaternion.identity);
        GridComponentOutput(name, (GameObject)block);//assign output
        block.name = name;//rename clone
        rightGrid.Add((GameObject)block);//add to grid
    }
    public void AddBlockToLeftArray()//add a block to the left array
    {
        string name = GenRandGridBlock();
        var block = Instantiate(Resources.Load(name, typeof(GameObject)), new Vector2(Game.gridWidth - (Game.gridWidth + 1) , Game.gridHeight-1), Quaternion.identity);
        GridComponentOutput(name, (GameObject)block);//assign output
        block.name = name;//rename clone
        leftGrid.Add((GameObject)block);//add to grid
    }

    public void DeleteGridBlocksAt(int indexRemoved)//this will delete the grid blocks on either side of the deleted row
    {
        Destroy(rightGrid[indexRemoved].gameObject);
        rightGrid.RemoveAt(indexRemoved);
        Destroy(leftGrid[indexRemoved].gameObject);
        leftGrid.RemoveAt(indexRemoved);


        MoveGridBlocksDown(indexRemoved);
        
            


    }

    public void MoveGridBlocksDown(int indexRemoved)//decrement the array position of all grid blocks higher than the deleted one
    {
        for (int i = indexRemoved; i < Game.gridHeight - 1; ++i) //for all values higher than shift value
        {

            rightGrid[i].transform.Translate(0, -1, 0);

            leftGrid[i].transform.Translate(0, -1, 0);


        }

    }
    public void SpawnNewBlocks()//spawn random block in last array position for left and right arrays
    {
        AddBlockToLeftArray();
        AddBlockToRightArray();
    }
    public void ChangeGridBlockAt(int x, bool output)
    {
        downGrid[x].gameObject.GetComponent<GridComponent>().ChangeOutputType(output);//for the particular index in down grid list change the object's output
    }

    public void MergeRowsDown()//this will take the outputs of the blocks of the bottom deleted row and makes those outputs the new bottom outputs(I'm debating this)
    {
        //for the component objects [x,0] loop through
        //for each component replace the output variable with the corresponding grid block
        for (int x = 0; x < Game.gridWidth; ++x)
        {
            

            bool newGridBlockOutput = Game.grid[x, 0].GetComponent<Move>().output;
            ChangeGridBlockAt(x, newGridBlockOutput);

        }
    }
}
