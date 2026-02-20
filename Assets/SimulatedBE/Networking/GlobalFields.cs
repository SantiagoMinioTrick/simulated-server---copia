namespace SimulatedBE.Networking
{
    public enum BreachType
    {
        SmallBlind,
        BigBlind,
        Boss
    }

    public enum BreachState
    {
        Skipped,
        Current,
        Locked,
        Defeated
    }

    public enum Symbols
    {
        Clubs,
        Spades,
        Diamonds,
        Hearts
    }

    public enum CardValue
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    public enum ItemType
    {
        Joker,
        Consumable,
        Voucher,
        Booster
    }

    public enum HandState
    {
        OnDiscard,
        OnScore,
        OnHeld,
        OnOtherJokers,
        AtTheMoment,
        OnPlayHand,
        OnSkipBreach,
        OnStartRound,
        OnEndRound,
        OnLose,
        OnWinAnte,
        None
    }

    public enum ConsumableType
    {
        Farm,
        Sugartown,
        MelLabs
    }

    public enum JokerRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic
    }

    public enum BoosterPackRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic
    }
    
    public enum Seal
    {
        None,
        Blue,
        Red,
        Yellow,
        Purple
    }
}
