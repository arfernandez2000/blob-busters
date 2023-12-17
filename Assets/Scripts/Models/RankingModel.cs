using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingModel
{
    public int ID => _id;
    public string Name => _name;
    public string Score => _score;

    private int _id;
    private string _name;
    private string _score;

    public RankingModel(int id, string name, string score) 
    {
        _id = id;
        _name = name;
        _score = score;
    }

    public string ToString() => $"RankingModel - Id: {ID} | Name: {Name} | Score: {Score}";
}
