using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIElement : MonoBehaviour
{
    [SerializeField] private Text _id;
    [SerializeField] private Text _name;
    [SerializeField] private Text _score;

    public void Init(int id, string name, string score) 
    {
        _id.text = id.ToString();
        _name.text = name;
        _score.text = score.ToString();
    }
}
