namespace Battleship.Core.Contracts
{
    public interface IRandomValueGenerator
    {
        bool GetRandomBool();
        int GetRandomIntValue(int minValue, int maxValue);
    }
}