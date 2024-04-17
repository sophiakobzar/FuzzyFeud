using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public LogicScript logic;
    int sizex = 20;
    int sizez = 20;
    public int[,] myArray;
    //int randomNumber;
    int max_fish = 80;
    //int map_fish = 0;
    private static System.Random random = new System.Random();
    public GameObject Fish;
    public Terrain terrain;
    // Start is called before the first frame update
    void Start()
    {
        
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        Debug.Log("Start");
        //Vector3 terrainSize = transform.localScale;
        Vector3 terrainSize = terrain.terrainData.size;
        sizex = (int) terrainSize.x;
        sizez = (int)terrainSize.z;
        myArray = new int[sizex, sizez];
        Debug.Log($"Terrain size: x={sizex}, z={sizez}" );
        
        createMap();
        
        GenerateFishFromMap();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public static string ArrayToString(int[,] arr)
    {
        StringBuilder builder = new StringBuilder();

        for (int row = 0; row < arr.GetLength(0); row++)
        {
            for (int col = 0; col < arr.GetLength(1); col++)
            {
                builder.Append(arr[row, col].ToString());
                builder.Append(" "); // Add a space as a separator 
            }
            builder.AppendLine(); // Add a newline after each row
        }

        return builder.ToString();
    }

    void createMap()
    {
        
        // 1. Linearize the array for easier shuffling
        int[] linearizedArray = new int[sizex * sizez];
        linearizedArray.AsSpan(0, max_fish).Fill(1);

        // 2. Shuffle the linearized array
        for (int i = linearizedArray.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (linearizedArray[i], linearizedArray[j]) = (linearizedArray[j], linearizedArray[i]);
        }

        // 3. Copy shuffled values back into the 2D array 
        int linearizedIndex = 0;
        for (int row = 0; row < sizex; row++)
        {
            for (int col = 0; col < sizez; col++)
            {
                myArray[row, col] = linearizedArray[linearizedIndex++];
            }
        }

        Debug.Log("Map Made");
        Debug.Log(ArrayToString(myArray));
    }

    // Fisher-Yates Shuffle algorithm for randomizing the list
    private void Shuffle(List<int> list)
    {
        System.Random random = new System.Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            int tmp = list[i];
            list[i] = list[swapIndex];
            list[swapIndex] = tmp;
        }
    }

    void GenerateFishFromMap()
    {
        for (int i = 0; i < sizex; i++)
            for (int j = 0; j < sizez; j++)
                if (myArray[i,j]==1)
                    GenerateFish(i, j);
    }
    bool GenerateFish(int x, int z)
    {
        Vector3 fishPosition = new Vector3(x + 1, 0.1f, z + 1);
        //float radius = 0.5f; // Adjust this radius as needed

        // Use a layer mask for furniture if you've set up layers
        int furnitureLayerMask = LayerMask.GetMask("furniture");

        //Collider[] hitColliders = Physics.OverlapBox(fishPosition, collectable.GetGameObject().GetComponent<MeshRenderer>().bounds.extents, Quaternion.identity, furnitureLayerMask);

        //f (hitColliders.Length > 0)
        //{
        //    Debug.Log($"Furniture found at {fishPosition} - fish spawn prevented");
        //    return false;
        //}

        // Safe to spawn the fish
        Instantiate(Fish, fishPosition, Quaternion.identity);
        logic.updateFFishNum(1);
        Debug.Log($"Fish spawned at {fishPosition}");
        return true;
    }

    public void CollectedTigger(int x, int z) {
        myArray[x, z] = 0;
        int ranX, ranZ;
        bool run = true;
        while (run) {
            ranX = UnityEngine.Random.Range(1, 19);
            ranZ = UnityEngine.Random.Range(1, 19);
            if (myArray[ranX,ranZ]==0&&(ranX!=x&&ranZ!=z))
            {
                run=!GenerateFish(ranX, ranZ);
            }
        }
    }
}
