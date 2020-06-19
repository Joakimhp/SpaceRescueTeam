using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class DataPathCommands
{
    [MenuItem ( "Tools/Data/Open persistent data path" )]
    public static void OpenPersistentDataPath() {
        string dataPath = Application.persistentDataPath; //Application.persistentDataPath.Replace ( @"/" , @"\" );
        dataPath = dataPath.TrimEnd ( new [] { '\\' , '/' } ); // Mac doesn't like trailing slash
        Process.Start ( dataPath );
    }

    [MenuItem ( "Tools/Data/Delete save file" )]
    public static void DeleteSaveFile() {
        string path = Application.persistentDataPath + "/player.playerdata";
        //dataPath = dataPath.TrimEnd ( new [] { '\\' , '/' } ); // Mac doesn't like trailing slash
        File.Delete ( path );
    }

    [MenuItem ( "Tools/Data/Create new save file" )]
    public static void CreateNewSaveFile() {
        SaveSystem.CreateAndSaveNewPlayerData ();
    }
}
