using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public static RankingManager instance;
    [SerializeField] private Transform _parentGrid;
    [SerializeField] private GameObject _rankingElementPrefab;

    private List<RankingModel> _players = new List<RankingModel>();
    private List<string> _names;
    private int _numRecords = 0;
    private Database _db = StatsManager.instance.db;

    private void Start() {
        // _db = new Database();

        // _names = new List<string> {
        //     "Maximiliano",
        //     "Pablo",
        //     "Roberto",
        //     "Maria",
        //     "Lucas",
        //     "Lucia"
        // };

        // for (int i = 0; i < 5; i++) {
        //     // _players.Add(new RankingModel(i + 1, _names[Random.Range(0, _names.Count)], Random.Range(1, 100000)));
        //     _db.AddRankingRecord(new RankingModel(i + 1, _names[Random.Range(0, _names.Count)], Random.Range(1, 100000)));
        // }

        // _players = _players.OrderByDescending(player => player.Score).ToList();
        _players = _db.GetRankingRecords();
        _numRecords = _players.Count;

        foreach (RankingModel player in _players) {
            RankingUIElement uiElement = Instantiate(_rankingElementPrefab, _parentGrid).GetComponent<RankingUIElement>();
            uiElement.Init(player.ID, player.Name, player.Score);
        }
    }
}
