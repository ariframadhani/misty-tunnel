using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour {

    Material one;
    Material two;
    Material three;
    Material four;
    Material five;

    GameManager gm;

    Vector4 movement;
    Vector4 starting;

    float transitionTime;
    float maxoffset;

    bool turn;

    void Start() {
        gm = FindObjectOfType<GameManager>();

        turn = (Random.Range(-1 ,2) > 0f); // random turn, when game start
        transitionTime = 0.5f; // transition offset movement on materials / best fit: 0.5f / test: 10;
        maxoffset = 10f; // max offset of materials / best fit: 10f / test: 4f
        
        LoadMaterials();
        
        starting = new Vector4(one.GetVector("_QOffset").x, one.GetVector("_QOffset").y, one.GetVector("_QOffset").z, one.GetVector("_QOffset").w);
        
    }

    void Update() {
        if (!gm.gameOver)
        {
            Movement();
        }
        
    }

    private void OnApplicationQuit()
    {
        starting = new Vector4(0, 0, 0, 0);
    }

    void LoadMaterials()
    {
        one = (Material)Resources.Load("Materials/[c]Character_Mat", typeof(Material));
        two = (Material)Resources.Load("Materials/[c]GroundTiling_Tex", typeof(Material));
        three = (Material)Resources.Load("Materials/[c]Props_Mat", typeof(Material));
        four = (Material)Resources.Load("Materials/[c]TilingMetal_Mat", typeof(Material));
        five = (Material)Resources.Load("Materials/[c]TillingGround_Mat", typeof(Material));
    }

    void Movement()
    {

        if (turn)
        {
            movement = new Vector4(starting.x + transitionTime * Time.deltaTime, starting.y, starting.z, starting.w);
        }
        else
        {
            movement = new Vector4(starting.x - transitionTime * Time.deltaTime, starting.y, starting.z, starting.w);
        }

        starting = movement;

        if (movement.x >= maxoffset)
        {
            turn = false;
        }
        else if (movement.x <= -maxoffset)
        {
            turn = true;
        }

        ChangeOffset(movement);
    }
    
    void ChangeOffset(Vector4 movement)
    {
        one.SetVector("_QOffset", movement);
        two.SetVector("_QOffset", movement);
        three.SetVector("_QOffset", movement);
        four.SetVector("_QOffset", movement);
        five.SetVector("_QOffset", movement);
    }
}
