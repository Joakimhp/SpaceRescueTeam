using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer( PlayerData player ) {
        BinaryFormatter formatter = new BinaryFormatter ();
        string path = Application.persistentDataPath + "/player.playerdata";
        FileStream stream = new FileStream ( path , FileMode.Create );

        //PlayerDataHandler data = new PlayerDataHandler ( player );

        formatter.Serialize ( stream , player );
        stream.Close ();
    }

    public static PlayerData LoadPlayer() {
        string path = Application.persistentDataPath + "/player.playerdata";
        if ( File.Exists ( path ) ) {
            BinaryFormatter formatter = new BinaryFormatter ();
            FileStream stream = new FileStream ( path , FileMode.Open );
            
            PlayerData data;
            if ( stream.Length != 0 ) {
                data = formatter.Deserialize ( stream ) as PlayerData;
            } else {
                data = new PlayerData ();
            }

            stream.Close ();
            
            return data;
        } else {
            Debug.LogError ( "Save file not found in path: " + path );
            return null;
        }
    }
}
