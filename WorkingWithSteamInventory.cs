using UnityEngine;

public class WorkingWithSteamInventory : MonoBehaviour
{
    public HeathenEngineering.SteamApi.PlayerServices.Demo.ExampleInventoryItemDefinition IronSword;
    public HeathenEngineering.SteamApi.PlayerServices.Demo.ExampleInventoryItemDefinition IronBar;
    public HeathenEngineering.SteamApi.PlayerServices.CraftingRecipe FiftyIronBarsRecipie;

    /// <summary>
    /// Prints the number of swords and bars to the log
    /// </summary>
    public void PrintTheQuantities()
    {
        Debug.Log("Player owns: " + IronSword.Count.ToString() + " Iron Swords");
        Debug.Log("Player owns: " + IronBar.Count.ToString() + " Iron Bars");
    }

    /// <summary>
    /// Attempts to craft an Iron Sword using the 50 Iron Bars Recipie ... this will fail if insufficent bars
    /// if it succeeds the IronSword count will increase.
    /// </summary>
    public void CraftAnIronSword()
    {
        IronSword.Craft(FiftyIronBarsRecipie);
    }

    /// <summary>
    /// Attempts to start a purchase for 1 Iron Sword ... if pricing is not set up correctly then this will not work
    /// </summary>
    public void BuyAnIronSword()
    {
        IronSword.StartPurchase(1);
    }

    /// <summary>
    /// Consumes e.g. destroys 1 Iron Sword
    /// </summary>
    public void ConsumeAnIronSword()
    {
        IronSword.Consume(1);
    }
}
