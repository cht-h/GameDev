using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Alchemy/Ingredient", fileName = "ING_New")]
public class Ingredient : ScriptableObject, ICombinable
{
    [Header("Basic Info")]
    public string displayName;
    public IngredientType type;
    public IngredientCategory category;
    [TextArea] public string description;
    public Sprite icon;
    public Color tint = Color.white;
    public float baseValue;

    [Header("Effects")]
    public List<AlchemicalEffect> effects = new List<AlchemicalEffect>();

    [Header("Danger Properties")]
    public bool isExplosive = false;
    public float instabilityFactor = 0f;

    public enum IngredientCategory
    {
        GarlandHerbs,
        JarIngredients,
        DresserCuriosities
    }

    [System.Serializable]
    public struct AlchemicalEffect
    {
        public EffectType type;
        public float potency;

        public enum EffectType
        {
            Healing,
            Poison,
            ManaRestore,
            StrengthBoost,
            Invisibility,
            Explosion
        }
    }

    public bool CanCombineWith(Ingredient other)
    {
        return !(this.isExplosive && other.isExplosive);
    }

    public Ingredient Combine(Ingredient other)
    {
        if (CheckExplosiveCombination(other))
        {
            HandleExplosion(this, other);
            return null;
        }
        return CreateCombinationResult(other);
    }

    private bool CheckExplosiveCombination(Ingredient other)
    {
        return (this.type == IngredientType.PhoenixAsh && other.type == IngredientType.TrollBlood) ||
               (this.type == IngredientType.MandrakeMilk && other.type == IngredientType.SwampToadEggs);
    }

    private void HandleExplosion(Ingredient a, Ingredient b)
    {
        Debug.LogWarning($"DANGER! Explosive combination: {a.displayName} + {b.displayName}");
        // GameManager.Instance.PlayerTakeDamage(30);
        // VFXManager.Instance.PlayExplosion();
    }

    private Ingredient CreateCombinationResult(Ingredient other)
    {
        Ingredient result = CreateInstance<Ingredient>();
        result.displayName = $"{this.displayName} + {other.displayName}";
        result.category = this.category;
        result.tint = Color.Lerp(this.tint, other.tint, 0.5f);
        result.icon = this.icon;
        result.baseValue = (this.baseValue + other.baseValue) * 0.8f;
        result.effects = CombineEffects(this.effects, other.effects);
        return result;
    }

    private List<AlchemicalEffect> CombineEffects(List<AlchemicalEffect> a, List<AlchemicalEffect> b)
    {
        List<AlchemicalEffect> newEffects = new List<AlchemicalEffect>();
        newEffects.AddRange(a);
        newEffects.AddRange(b);
        return newEffects;
    }
}