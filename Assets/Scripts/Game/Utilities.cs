using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    //can use this class to put methods that don't rely on monobehaviour and dont change
    public static bool CheckOutput(bool l, bool d, string name)
    { //take inputLeft and inputDown and produce an output, currently only OR
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

    /*
* GetRandBlock returns a random string which is a gate's name
*/
    //1-3 AND, OR | 1-4 AND, OR, NAND | 1-5 AND, OR, NAND, NOR | 1-6 1-5 AND, OR, NAND, NOR, XOR
    public static string GetRandomGate()
    {
        int randBlock = Random.RandomRange(1, 6);
        string randBlockName = "AND";
        switch (randBlock)
        {
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
    //e.g. {1,3,5} will allow for OR, NAND and XOR gates to generate
    public static string GetRandomGate(int[] blockNumbers)
    {
        int randBlock = Random.Range(1, blockNumbers.Length+1);
        string randBlockName = "AND";
        switch (blockNumbers[randBlock])
        {
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

    public static bool IsNotOnBottomEdge(Vector2 pos)
    { //Checks if a block is on the on the bottom edge
        if ((int)pos.y != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /*
     * IsNotOnLeftEdge checks if a gate is not on the left edge of the grid
     */
    public static bool IsNotOnLeftEdge(Vector2 pos) //Checks if a block is on the left edge
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

}
