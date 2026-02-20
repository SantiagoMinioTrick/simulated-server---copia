using SimulatedBE.Networking.DTOs;

public class TestJoker : SimulatedBE.Networking.Jokers.Joker
{
    public TestJoker(bool cardDependency) : base(cardDependency)
    {
    }

    public override bool CanUseJoker(CardDto card)
    {
        throw new System.NotImplementedException();
    }
}
