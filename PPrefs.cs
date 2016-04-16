using UnityEngine;
using System.Collections;

public static class PPrefs
{
    public const string LevelsUnlock = "LevelsUnlock";

    //STAUS
    public const string Score = "Score";
    public const string BestScoreTimeAttack = "BestScoreTimeAttack";
    public const string TimeAttackBonusStage = "TimeAttackBonusStage";

    public const string TotalEggsCatched = "TotalEggs";
    public const string TotalEggsBroken = "TotalEggsBroken";
    public const string TotalRoddentEggsCatched = "TotalEggs";

    public const string TotalLevelsPassed = "TotalLevelsPassed";
    public const string TotalLevelsFail = "TotalLevelsFail";
    public const string TotalLevelsPlayed = "TotalLevelsPlayed";

    public const string TotalEnemiesDefeat = "TotalEnemiesDefeat";
    public const string TotalMiceDefeat = "TotalEnemiesDefeat";

    public const string TotalItems = "TotalItems";

    public static void Plus(string key, int count = 1)
    {
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + count);
        PlayerPrefs.Save();
    }

    public static string Get(string key)
    {
        return PlayerPrefs.GetInt(key).ToString();
    }

}