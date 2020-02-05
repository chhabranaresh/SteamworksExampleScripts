using UnityEngine;

/// <summary>
/// Note that in this class we use the fully qualified name of each object.
/// We do this to make it very clear where everything comes from but you can save yoru self a lot of typing by using namespaces.
/// e.g. add using HeathenEngineering.SteamApi.Foundation; to the top line and you can remove all occurances of that string below.
/// </summary>
public class WorkingWithStatsAndAchievements : MonoBehaviour
{
    /// <summary>
    /// A reference to our <see cref="HeathenEngineering.SteamApi.Foundation.SteamSettings"/> object. 
    /// This is the same object that is referenced on our <see cref="HeathenEngineering.SteamApi.Foundation.SteamworksFoundationManager"/>
    /// </summary>
    public HeathenEngineering.SteamApi.Foundation.SteamSettings settings;

    /// <summary>
    /// This is a reference to our <see cref="HeathenEngineering.SteamApi.Foundation.SteamIntStatData"/> object, same one we added to our <see cref="HeathenEngineering.SteamApi.Foundation.SteamworksFoundationManager"/>
    /// </summary>
    public HeathenEngineering.SteamApi.Foundation.SteamIntStatData exampleStat1;

    /// <summary>
    /// This is a reference to our <see cref="HeathenEngineering.SteamApi.Foundation.SteamAchievementData"/> object, same one we added to our <see cref="HeathenEngineering.SteamApi.Foundation.SteamworksFoundationManager"/>
    /// </summary>
    public HeathenEngineering.SteamApi.Foundation.SteamAchievementData exampleAchievement1;

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
    /// This simply demonstrates how to set the value, you would put the code contained in this method somewhere in your game logic that made since to your game.
    /// </summary>
    void ExampleOfSettingAStat()
    {
        //This will attempt to set the stat value to 42
        //Note that this stores the entry to a local cashe ... it is not yet submited to the server
        exampleStat1.SetIntStat(42);

        //This commits any outstanding changes to stats or achievements to the server ... note this gets called automatically when the game closes.
        settings.StoreStatsAndAchievements();
    }

    /// <summary>
    /// This simply demonstrates how to unlock the achievement, you would put the code contained in this method somewhere in your game logic that made since to your game.
    /// </summary>
    void ExampleOfUnlockingAnAchievement()
    {
        //This will unlock the achievement
        //Note that this stores the change to a local cahse ... it is not yet submited to the server
        exampleAchievement1.Unlock();

        //This commits any outstanding changes to stats or achievements to the server ... note this gets called automatically when the game closes.
        settings.StoreStatsAndAchievements();
    }
}
