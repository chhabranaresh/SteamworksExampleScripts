using UnityEngine;
/// <summary>
/// Note that in this class we use the fully qualified name of each object.
/// We do this to make it very clear where everything comes from but you can save yoru self a lot of typing by using namespaces.
/// e.g. add using HeathenEngineering.SteamApi.PlayerServices; to the top line and you can remove all occurances of that string below.
/// </summary>
public class WorkingWithLeaderboards : MonoBehaviour
{
    /// <summary>
    /// The <see cref="HeathenEngineering.SteamApi.PlayerServices.SteamworksLeaderboardData"/> object to work against.
    /// </summary>
    public HeathenEngineering.SteamApi.PlayerServices.SteamworksLeaderboardData feetTraveled;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: if you dont need a Start event you really should remove this
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: if you dont need an update you really should remove this
    }

    /// <summary>
    /// Register for the Unity events of this board.
    /// </summary>
    void RegisterForBoardEvents()
    {
        //The On Query Results event occures every time a query returns results.
        feetTraveled.OnQueryResults.AddListener(handleOnQueryResults);

        //The Board Found event occures when the board's ID is located on the Steam API
        feetTraveled.BoardFound.AddListener(handleBoardFound);

        //The User New High Rank event occures when the local user's rank changes for the better in the board
        feetTraveled.UserNewHighRank.AddListener(handleUserNewHighRank);

        //The User Rank Changed event occures when the local user's rank changes at all (for the better or worse)
        feetTraveled.UserRankChanged.AddListener(handleUserRankChanged);

        //The User Rank Loaded event occures when the local user's rank is loaded from the Steam API backend
        feetTraveled.UserRankLoaded.AddListener(handleUserRankLoaded);
    }

    /// <summary>
    /// This is set to the <see cref="HeathenEngineering.SteamApi.PlayerServices.SteamworksLeaderboardData.UserRankLoaded"/> event in <see cref="RegisterForBoardEvents"/> method
    /// </summary>
    /// <param name="eventData">The user data resulting of the load operation</param>
    void handleUserRankLoaded(HeathenEngineering.SteamApi.PlayerServices.LeaderboardUserData eventData)
    {
        Debug.Log("Local user's rank data has been loaded, Rank: " + eventData.entry.m_nGlobalRank + ", Score: " + eventData.entry.m_nScore + ", Details: " + eventData.details);
    }

    /// <summary>
    /// This is set to the <see cref="HeathenEngineering.SteamApi.PlayerServices.SteamworksLeaderboardData.UserRankChanged"/> event in <see cref="RegisterForBoardEvents"/> method
    /// </summary>
    /// <param name="eventData">The change rank data loaded as a result of the operation</param>
    void handleUserRankChanged(HeathenEngineering.SteamApi.PlayerServices.LeaderboardRankChangeData eventData)
    {
        Debug.Log("Local user's rank has changed");
        // You can find the old entry data in eventData.oldEntry, this is a Nullable<T> so check if eventData.oldEntry.HasValue and if so read from eventData.oldEntry.Value
        // You can find the new entry data in eventData.newEntry, this is not nullable so always have a value.
    }

    /// <summary>
    /// This is set to the <see cref="HeathenEngineering.SteamApi.PlayerServices.SteamworksLeaderboardData.UserNewHighRank"/> event in <see cref="RegisterForBoardEvents"/> method
    /// </summary>
    /// <param name="eventData">The change rank data loaded as a result of the operation</param>
    void handleUserNewHighRank(HeathenEngineering.SteamApi.PlayerServices.LeaderboardRankChangeData eventData)
    {
        Debug.Log("Local user's rank has changed for the better");
        // You can find the old entry data in eventData.oldEntry, this is a Nullable<T> so check if eventData.oldEntry.HasValue and if so read from eventData.oldEntry.Value
        // You can find the new entry data in eventData.newEntry, this is not nullable so always have a value.
    }

    /// <summary>
    /// This is set to the <see cref="HeathenEngineering.SteamApi.PlayerServices.SteamworksLeaderboardData.BoardFound"/> event in <see cref="RegisterForBoardEvents"/> method
    /// </summary>
    void handleBoardFound()
    {
        Debug.Log("The system located the board on the Steam API");
    }

    /// <summary>
    /// This is set to the <see cref="HeathenEngineering.SteamApi.PlayerServices.SteamworksLeaderboardData.OnQueryResults"/> event in <see cref="RegisterForBoardEvents"/> method
    /// </summary>
    /// <param name="eventData">Pointer data returned from the query request. This can be used to read the data from the local cashe.</param>
    void handleOnQueryResults(HeathenEngineering.SteamApi.PlayerServices.LeaderboardScoresDownloaded eventData)
    {
        //If some error occured 
        if(eventData.bIOFailure)
        {
            Debug.LogError("A general error occured on Valve's side.");
        }
        //Else no error occured so fetch the data from the cashe
        else
        {
            //For each entry returned on this query
            for (int i = 0; i < eventData.scoreData.m_cEntryCount; i++)
            {
                //Establish a buffer to load data into
                Steamworks.LeaderboardEntry_t buffer;

                //In the event this board has no detail entries
                if (feetTraveled.MaxDetailEntries < 1)
                {
                    Steamworks.SteamUserStats.GetDownloadedLeaderboardEntry(eventData.scoreData.m_hSteamLeaderboardEntries, i, out buffer, null, 0);
                    //TODO: buffer now contains the leaderboard data for a given entry, do something with it.
                }
                //Else this board does handle detail entries ... so we have extra steps
                else
                {
                    //Build a detail buffer to hold the detail data
                    var details = new int[feetTraveled.MaxDetailEntries];
                    Steamworks.SteamUserStats.GetDownloadedLeaderboardEntry(eventData.scoreData.m_hSteamLeaderboardEntries, i, out buffer, details, feetTraveled.MaxDetailEntries);
                    //TODO: buffer now contains the leaderboard data for a given entry, do something with it.
                    //TODO: details now contains your array of details for a given entry, do something with it.
                }

                Debug.Log("Loaded entry for user with Steam ID: " + buffer.m_steamIDUser + ", Rank: " + buffer.m_nGlobalRank + ", Score: " + buffer.m_nScore);
            }
        }
    }

    /// <summary>
    /// Set the player score and overwrite any existing value
    /// </summary>
    void SetThePlayerScoreAndOverwriteOldScore()
    {
        //Sets the player's score to 42 overwriting any existing value
        feetTraveled.UploadScore(42, Steamworks.ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate);
    }

    /// <summary>
    /// Set player score keeping the best value between the existing value and the new value
    /// </summary>
    void SetThePlayerScoreButKeepTheBestValueBetweenTheCUrrentAndNewScore()
    {
        //Set the player's score to 42 but have the API keep the better value between the current and new values.
        //If this board is sorted assending then a lower number is better
        //If this board is sorted desending then a higher number is better
        feetTraveled.UploadScore(42, Steamworks.ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest);
    }

    /// <summary>
    /// Read the player's cashed score.
    /// Note that the cashe updates when the board is loaded, on every event and on every call to upload a player score.
    /// In short this should nearly always be accurate but you can also force it to refresh.
    /// </summary>
    void ReadThePlayersCurrentScoreFromCashe()
    {
        //The UserEntry is a Nullable so we first test to see if it has a value at all ... if so report its data
        if(feetTraveled.UserEntry.HasValue)
        {
            Debug.Log("The player's current rank is: " + feetTraveled.UserEntry.Value.m_nGlobalRank + ", the player's current score is: " + feetTraveled.UserEntry.Value.m_nScore + " the player has " + feetTraveled.UserEntry.Value.m_cDetails + " detail entries");
        }
        else
        {
            Debug.LogWarning("The player doesn't have a cahsed entry on this board yet.");
        }
    }

    /// <summary>
    /// Ask the system to refresh the <see cref="HeathenEngineering.SteamApi.PlayerServices.SteamworksLeaderboardData.UserEntry"/>
    /// </summary>
    void RefreshThePlayersCashedEntry()
    {
        feetTraveled.RefreshUserEntry();
    }

    /// <summary>
    /// Request the top 10 entries of this board
    /// </summary>
    void GetTop10Entries()
    {
        feetTraveled.QueryTopEntries(10);
    }

    /// <summary>
    /// Request the entry data for friends of the player that are within 5 friend positions of the local users rank
    /// </summary>
    void Get5FriendsEntriesNearThePlayerEntry()
    {
        feetTraveled.QueryFriendEntries(5);
    }

    /// <summary>
    /// Request the entry data for anyone within 20 entries of the player's entry
    /// </summary>
    void Get20EntriesNeerThePlayer()
    {
        feetTraveled.QueryPeerEntries(20);
    }
}
