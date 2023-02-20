using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class WorldGen : MonoBehaviour
{

    // we initiate a new scene each time yah? that might be better but might have reload problems

    int sizeX;
    int sizeZ;
    int complexity;
    public int startX;
    public int startZ;
    int minX;
    int minZ;
    public GameObject floorTile;
    public GameObject whiteTile;
    public bool parity; //true = enter from x, false = enter from z 
    int end;
    
    void Start()
    {
        sizeX = UnityEngine.Random.Range(30, 50);
        sizeZ = UnityEngine.Random.Range(30, 50);
        //check if size colides with other rooms. 

        complexity = UnityEngine.Random.Range(4, 7);
        //Debug.Log("Number of turns: " + complexity);



        if (!parity)
        {
            minZ = startZ - UnityEngine.Random.Range(3, sizeZ-2);
            minX = startX;
            if (complexity % 2 == 1)
            {
                int random = UnityEngine.Random.Range(0, 2);
                end = random * (minZ-1) + Mathf.Abs(random-1) * (minZ + sizeZ); // random

            }
            else
            {
                end = minX + sizeX;
            }
        }
        else
        {
            minX = startX - UnityEngine.Random.Range(1, sizeX);
            minZ = startZ;
            if (complexity % 2 == 1)
            {
                int random = UnityEngine.Random.Range(0, 2);
                end = random * (minX-1) + Mathf.Abs(random - 1) * (minX + sizeX); // random
            }
            else
            {
                end = minZ + sizeZ;
            }

        }
        //Debug.Log("Corner: " + minX + " x " + minZ);

        //Debug.Log("Size: " + sizeX + " by " + sizeZ);



        Vector3 Ve = new Vector3(minX + (sizeX)/2f -0.5f, 0f, minZ +(sizeZ)/2f -0.5f);
        GameObject flooring = Instantiate(floorTile, Ve, Quaternion.identity);
        flooring.transform.localScale = new Vector3(sizeX, 1, sizeZ);

        /*        for (int X = 0; X < sizeX; X++)
                {
                    for (int Z = 0; Z < sizeZ; Z++)
                    {
                        Vector3 V = new Vector3(minX + X, 0f, minZ + Z);
                        GameObject obj = Instantiate(floorTile, V, Quaternion.identity);
                        //Debug.Log(X + " " + Z);
                    }

                }*/
        //Debug.Log("Done generating map");


        int floatyX = startX;
        int floatyZ = startZ;
        int turn;

        for (int R = 0; R < complexity; R++)

            if (parity)
            {

                turn = minZ + UnityEngine.Random.Range(3, sizeZ-3); // aim for turn
                while (Mathf.Abs(turn - floatyZ) < sizeZ/5)
                {
                    turn = minZ + UnityEngine.Random.Range(3, sizeZ-3); // reroll until its not too close
                }

                for (int P = floatyZ; P - turn != 0 ; P = (Mathf.Abs(P-turn)-1) * ((P-turn)/Mathf.Abs(P-turn)) + turn )
                {
                    Vector3 V = new Vector3(floatyX, 0.55f, P);
                    GameObject obj = Instantiate(whiteTile, V, Quaternion.identity);
                    //Debug.Log(floatyX + " x " +  P);
                }
                floatyZ = turn;
                parity = !parity;
            }
            else
            {
                
                turn = minX + UnityEngine.Random.Range(3, sizeX-3);

                while (Mathf.Abs(turn - floatyX) < sizeX / 5)
                {
                    turn = minX + UnityEngine.Random.Range(3, sizeX-3); // reroll until its not too close
                }

                for (int P = floatyX; P - turn != 0; P = (Mathf.Abs(P - turn) - 1) * ((P - turn) / Mathf.Abs(P - turn)) + turn)
                {
                    Vector3 V = new Vector3(P, 0.55f, floatyZ);
                    GameObject obj = Instantiate(whiteTile, V, Quaternion.identity);
                }
                floatyX = turn;
                parity = !parity;
            }

        
        if (parity)
        {
            for (int P = floatyZ; P - end != 0; P = (Mathf.Abs(P - end) - 1) * ((P - end) / Mathf.Abs(P - end)) + end)
            {
                Vector3 V = new Vector3(floatyX, 0.55f, P);
                GameObject obj = Instantiate(whiteTile, V, Quaternion.identity);

            }
        }
        else
        {
            for (int P = floatyX; P - end != 0; P = (Mathf.Abs(P - end) - 1) * ((P - end) / Mathf.Abs(P - end)) + end)
            {
                Vector3 V = new Vector3(P, 0.55f, floatyZ);
                GameObject obj = Instantiate(whiteTile, V, Quaternion.identity);

            }
        }

    }

   
}
