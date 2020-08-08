using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] public static int turnedGood = 0;
    [SerializeField] Text scoreCount;
    [SerializeField] public int hit;
    // Start is called before the first frame update
    void Start()
    {
        turnedGood = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Count()
    {
        turnedGood += hit;
        scoreCount.text = " Enemies: " + turnedGood.ToString() + "/10";
        //Debug.Log("jumped" + turnedGood);
    }
}
