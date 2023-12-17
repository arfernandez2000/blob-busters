using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class Database
{
    private const string DbName = "Database.s3db";
    private const string TABLE_RANKING = "RankingRecords";
    private string _connPath;
    private IDbConnection _dbConn;

    public Database()
    {
        _connPath = $"URI=file:{Application.dataPath}/{DbName}";
        _dbConn = new SqliteConnection(_connPath);

        // DropTable_Ranking();
        CreateTable_Ranking();
    }

    #region COMMON_METHODS
    private void PostQueryToDb(string query) 
    {
        Debug.Log($"*** PostQueryToDb ***\n {query}");
        try
        {
            _dbConn.Open();

            IDbCommand command = _dbConn.CreateCommand();
            command.CommandText = query;
            command.ExecuteReader();

            command.Dispose();
            command = null;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"*** Post query ERROR ***\n {e.Message}");
        }
        finally
        {
            _dbConn.Close();
        }
    }
    #endregion

    #region TABLE_RANKING_ACTIONS
    private void DropTable_Ranking() {
        try
        {
            string query = $"DROP TABLE IF EXISTS {TABLE_RANKING}";
            PostQueryToDb(query);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"*** Drop table Ranking ERROR ***\n {e.Message}");
        }
    }

    private void CreateTable_Ranking() {
        string query = $"CREATE TABLE IF NOT EXISTS {TABLE_RANKING} (" +
        $"Id INTEGER PRIMARY KEY AUTOINCREMENT," + 
        $"Name VARCHAR(200) NOT NULL," +
        $"Score VARCHAR(200) NOT NULL)";

        PostQueryToDb(query);
    }

    public void AddRankingRecord(RankingModel model) 
    {
        Debug.Log($"*** AddRankingRecord ***\n {model.ToString()}");
        string query = $"INSERT INTO {TABLE_RANKING} (Name, Score) VALUES ('{model.Name}', '{model.Score}')";
        PostQueryToDb(query);
    }

    public List<RankingModel> GetRankingRecords() 
    {
        List<RankingModel> records = new List<RankingModel>();
        try
        {
            _dbConn.Open();

            IDbCommand command = _dbConn.CreateCommand();

            string query = $"SELECT Id, Name, Score FROM {TABLE_RANKING} ORDER BY Score DESC";
            command.CommandText = query;

            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                RankingModel model = new RankingModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                records.Add(model);
                Debug.Log(model.ToString());
            }

            command.Dispose();
            command = null;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"*** Post query ERROR ***\n {e.Message}");
        }
        finally
        {
            _dbConn.Close();
        }

        return records;
    }
    #endregion
}
