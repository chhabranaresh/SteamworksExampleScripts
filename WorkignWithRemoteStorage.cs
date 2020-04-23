using UnityEngine;

public class WorkignWithRemoteStorage : MonoBehaviour
{
    public HeathenEngineering.SteamApi.PlayerServices.SteamDataLibrary dataLibrary;

    /// <summary>
    /// Loads the latest file stored on Steam for this particular library
    /// </summary>
    public void LoadTheLateastFile()
    {
        HeathenEngineering.SteamApi.PlayerServices.SteamworksRemoteStorage.Instance.RefreshDataFilesIndex();

        if (dataLibrary.availableFiles.Count > 0)
        {
            //Sort the available files such that the list is in date order ... this puts the newest file at the end of the list
            dataLibrary.availableFiles.Sort((a, b) => a.LocalTimestamp.CompareTo(b.LocalTimestamp));
            //Flip the list such that the newest is at the start
            dataLibrary.availableFiles.Reverse();
            //Load the first file in the list .... e.g. load the most resent one
            dataLibrary.Load(dataLibrary.availableFiles[0]);
        }
    }

    /// <summary>
    /// Saves the current data to Steam with the indicated name
    /// </summary>
    /// <param name="name"></param>
    public void SaveFile(string name)
    {
        dataLibrary.SaveAs(name);
    }

    /// <summary>
    /// Overwrites the most resently read file ... e.g. if you loaded a file, it will belisted as the active File so you can simply call Save to write back to it
    /// </summary>
    public void OverwriteFile()
    {
        if (dataLibrary.activeFile != null)
            dataLibrary.Save();
        else
            Debug.LogError("You cant overwrite a file untill you have loaded it");
    }
}
