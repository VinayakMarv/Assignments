using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public WordsProblem wordsprob;
    public TextMeshProUGUI L1,L2,L3,L4,W1,W2,W3;
    int counter=0;
    void Start()
    {
        LetterSetOnButtons(counter);
    }

    void Update()
    {
    }
    void LetterSetOnButtons(int count){
        L1.text=wordsprob.Letter1[count].ToString();
        L2.text=wordsprob.Letter2[count].ToString();
        L3.text=wordsprob.Letter3[count].ToString();
        L4.text=wordsprob.Letter4[count].ToString();
    }
}
