using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class WorldGen : MonoBehaviour
{

    // we initiate a new scene each time yah? that might be better but might have reload problems

    public int sizeX; // size of each block
    public int sizeZ;
    public int cornerX; //leftmost corner, each block is exactly the same as x/z
    public int cornerZ;
    public int bigSize;
    int complexity;
/*    int startX;
    int startZ;*/
    int minX;
    int minZ;
    public GameObject floorTile;
    public GameObject whiteTile;
    bool parity; //true = enter from x, false = enter from z 
    int end;
    int en;
    int ex;
    int stX;
    int stZ;

    
    void Generate(int entry, int exit, int startX, int startZ) //entry/exit = 0 postive X, 1 positive Z, 2 negative x, 3 negative z
    {
/*        sizeX = UnityEngine.Random.Range(30, 50);
        sizeZ = UnityEngine.Random.Range(30, 50);*/
        //check if size colides with other rooms. 

        complexity = (UnityEngine.Random.Range(2, 3) *2)+ (entry-exit)%2; // will be even if going across, odd if going L/R
        //Debug.Log("Number of turns: " + complexity);




        if (entry == 0)
        {
            minX = startX;
            minZ = ((int)(Mathf.Floor((float)(startZ - cornerZ) / sizeZ)*sizeZ)) + cornerZ; // if this rounds down, it should give us the minZ
            //Debug.Log(entry + " minZ " + minZ);
            parity = false;
        }
        else if (entry == 1)
        {
            minZ = startZ;
            minX = ((int)(Mathf.Floor((float)(startX - cornerX) / sizeX) * sizeX)) + cornerX;
            //Debug.Log(entry + " minX " + minX);
            parity = true;
        }
        else if (entry == 2)
        {
            minX = startX - sizeX+1;
            minZ = ((int)(Mathf.Floor((float)(startZ - cornerZ) / sizeZ) * sizeZ)) + cornerZ;
            //minZ = ((startZ - cornerZ) / sizeZ) * sizeZ + cornerZ;
            //Debug.Log(entry + " minZ " + minZ);
            parity = false;
        }
        else
        {
            minZ = startZ-sizeZ+1;
            //minX = ((startX - cornerX) / sizeX) * sizeX + cornerX;
            minX = ((int)(Mathf.Floor((float)(startX - cornerX) / sizeX) * sizeX)) + cornerX;
            //Debug.Log(entry + " minX " + minX);
            parity = true;

        } // is there a better way to write this?


        Vector3 Ve = new Vector3(minX + (sizeX) / 2f , 0f, minZ + (sizeZ) / 2f ); 
        GameObject flooring = Instantiate(floorTile, Ve, Quaternion.identity);
        flooring.transform.localScale = new Vector3(sizeX, 1, sizeZ);

        if (exit == 0)
        {
            end = minX-1;
            stX = end;
        }
        else if (exit ==1)
        {
            end = minZ-1;
            stZ = end;
        }
        else if(exit == 2)
        {
            end = minX + sizeX;
            stX = end ;
        }
        else
        {
            end = minZ + sizeZ;
            stZ = end ;
        } // is there a better way to write this?

        /*      if (!parity)
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

              }*/
        //Debug.Log("Corner: " + minX + " x " + minZ);

        //Debug.Log("Size: " + sizeX + " by " + sizeZ);



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

                turn = minZ + UnityEngine.Random.Range(3, sizeZ - 3); // aim for turn
                while (Mathf.Abs(turn - floatyZ) < sizeZ / 5)
                {
                    turn = minZ + UnityEngine.Random.Range(3, sizeZ - 3); // reroll until its not too close
                }

                for (int P = floatyZ; P - turn != 0; P = (Mathf.Abs(P - turn) - 1) * ((P - turn) / Mathf.Abs(P - turn)) + turn)
                {
                    Vector3 V = new Vector3(floatyX+0.5f, 0.55f, P+0.5f);
                    GameObject obj = Instantiate(whiteTile, V, Quaternion.identity);
                    //Debug.Log(floatyX + " x " +  P);
                }
                floatyZ = turn;
                parity = !parity;
            }
            else
            {

                turn = minX + UnityEngine.Random.Range(3, sizeX - 3);

                while (Mathf.Abs(turn - floatyX) < sizeX / 5)
                {
                    turn = minX + UnityEngine.Random.Range(3, sizeX - 3); // reroll until its not too close
                }

                for (int P = floatyX; P - turn != 0; P = (Mathf.Abs(P - turn) - 1) * ((P - turn) / Mathf.Abs(P - turn)) + turn)
                {
                    Vector3 V = new Vector3(P+0.5f, 0.55f, floatyZ+0.5f);
                    GameObject obj = Instantiate(whiteTile, V, Quaternion.identity);
                }
                floatyX = turn;
                parity = !parity;
            }


        if (parity) //ends
        {
            for (int P = floatyZ; P - end != 0; P = (Mathf.Abs(P - end) - 1) * ((P - end) / Mathf.Abs(P - end)) + end)
            {
                Vector3 V = new Vector3(floatyX+0.5f, 0.55f, P+0.5f);
                GameObject obj = Instantiate(whiteTile, V, Quaternion.identity);

            }
            stX = floatyX;
            
        }
        else
        {
            for (int P = floatyX; P - end != 0; P = (Mathf.Abs(P - end) - 1) * ((P - end) / Mathf.Abs(P - end)) + end)
            {
                Vector3 V = new Vector3(P+0.5f, 0.55f, floatyZ+0.5f);
                GameObject obj = Instantiate(whiteTile, V, Quaternion.identity);

            }
            stZ = floatyZ;
        }
    

    }

/*    int aFloorFunctionThatActuallyWorks(float x)
    {
        if (x >= 0.0)
        {
            return (int)Mathf.Floor(x);    
        }
        else
        {
            return (int)Mathf.Ceil(x);
        }
    }*/

    void Awake()
    {
/*        int x = -5;
        int y = 2;
        Debug.Log(Mathf.Floor(x / y));
        Debug.Log("b"+Mathf.Floor(-5 / 2));
        Debug.Log("b" + Mathf.Floor(-5 / 2f));*/
        ex = 2;
        stX = 0 + cornerX;
        stZ = UnityEngine.Random.Range(3, sizeZ - 3) + cornerZ;
        //Debug.Log(stZ);
        //UnityEngine.Random.Range(0, bigSize);

        for (int i = 0; i < bigSize; i++)
        {
            en = (ex + 2) % 4; // its a + coz unity can't handle modulo -1 apparently 
            ex = (en + UnityEngine.Random.Range(1, 4)) % 4; // 1 will turn R, 2 will go straight, 3 will turn L
            //ex = 0;
            //Debug.Log(en + " " + ex + " " + stX + " " + stZ + " ");
            Generate(en, ex, stX, stZ);
            
        }


           
        //Debug.Log(ex +" "+ en);

        // cornerX = cornerX + sizeX / 2f;
        // cornerZ = cornerZ + sizeZ / 2f; // moves the corner into the center of the cornest piece
    }
   
}
