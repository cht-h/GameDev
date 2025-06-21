using System.Collections.Generic;

public static class DangerSystem
{
    private static readonly Dictionary<IngredientType, List<IngredientType>> dangerousPairs = 
        new Dictionary<IngredientType, List<IngredientType>>
    {
        { IngredientType.PhoenixAsh, new List<IngredientType> { IngredientType.TrollBlood } },
        { IngredientType.MandrakeMilk, new List<IngredientType> { IngredientType.SwampToadEggs } }
    };

    public static bool IsDangerousCombination(Ingredient a, Ingredient b)
    {
        return dangerousPairs.TryGetValue(a.type, out var dangerousForA) && dangerousForA.Contains(b.type) ||
               dangerousPairs.TryGetValue(b.type, out var dangerousForB) && dangerousForB.Contains(a.type);
    }
}