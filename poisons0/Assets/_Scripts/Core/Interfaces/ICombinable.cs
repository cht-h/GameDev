public interface ICombinable
{
    bool CanCombineWith(Ingredient other);
    Ingredient Combine(Ingredient other);
}