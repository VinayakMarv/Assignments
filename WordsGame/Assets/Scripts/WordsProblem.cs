using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordsProblem", menuName = "WordProblem")]
public class WordsProblem : ScriptableObject {
    public int level;
    public char[] Letter1,Letter2,Letter3,Letter4;
    public string[] Words;
}
