using System.Collections.Generic;
using System;
using SimulatedBE.Networking.DTOs;
using System.Linq;
using System.Collections;

namespace SimulatedBE.Networking
{
    public class PokerRules
    {
        private enum SymbolMatch { None, AllMatch }
        private enum ValueMatch { None, Two, DoubleTwo, Three, FullHouse, Four, Five }
        private enum ConsecutiveValue { None, Straight, RoyalStraight }
        public enum TypeToScore { Symbol, Value, Consecutive, None }

        private const string NeedsMaxCards = "MaxCards";
        private List<Rule> myRules = new List<Rule>();
        private int _straightDiff;
        private int _maxCards;
        public PokerRules()
        {
            //myRules.Add(new Rule(new PokerHandDto("Five of a Kind", 1, new ScoreDto(120, 12), 0), TypeToScore.Value ,ValueMatch.Five.ToString(), 5.ToString()));
            myRules.Add(new Rule(new PokerHandDto("Royal Flush", 1, new ScoreDto(0, 100, 8), 0), TypeToScore.Consecutive, SymbolMatch.AllMatch.ToString(), ConsecutiveValue.RoyalStraight.ToString(), NeedsMaxCards));
            myRules.Add(new Rule(new PokerHandDto("Straight Flush", 1, new ScoreDto(0, 100, 8), 0), TypeToScore.Consecutive, SymbolMatch.AllMatch.ToString(), ConsecutiveValue.Straight.ToString(), NeedsMaxCards));
            myRules.Add(new Rule(new PokerHandDto("Four of a Kind", 1, new ScoreDto(0, 60, 7), 0), TypeToScore.Value, ValueMatch.Four.ToString()));
            myRules.Add(new Rule(new PokerHandDto("Full House", 1, new ScoreDto(0, 40, 4), 0), TypeToScore.Value, ValueMatch.FullHouse.ToString()));
            myRules.Add(new Rule(new PokerHandDto("Flush", 1, new ScoreDto(0, 35, 4), 0), TypeToScore.Symbol, SymbolMatch.AllMatch.ToString(), NeedsMaxCards));
            myRules.Add(new Rule(new PokerHandDto("Straight", 1, new ScoreDto(0, 30, 4), 0), TypeToScore.Consecutive, ConsecutiveValue.Straight.ToString(), NeedsMaxCards));
            myRules.Add(new Rule(new PokerHandDto("Three of a Kind", 1, new ScoreDto(0, 30, 3), 0), TypeToScore.Value, ValueMatch.Three.ToString()));
            myRules.Add(new Rule(new PokerHandDto("Two Pair", 1, new ScoreDto(0, 20, 2), 0), TypeToScore.Value, ValueMatch.DoubleTwo.ToString()));
            myRules.Add(new Rule(new PokerHandDto("Pair", 1, new ScoreDto(0, 10, 2), 0), TypeToScore.Value, ValueMatch.Two.ToString()));
            myRules.Add(new Rule(new PokerHandDto("High Card", 1, new ScoreDto(0, 5, 1), 0), TypeToScore.None));
            _straightDiff = 1;
            _maxCards = 5;
        }

        public void ChangeStraightDiff(int newDiff)
        {
            _straightDiff = newDiff;
        }

        public void ChangeMaxCards(int maxCards)
        {
            _maxCards = maxCards;
        }

        public Tuple<PokerHandDto, List<CardDto>> CheckPokerHand(List<CardDto> cards)
        {
            if (cards.Count <= 0)
                return Tuple.Create(new PokerHandDto("", 0, new ScoreDto(), 0), new List<CardDto>());

            var symbolMatch = GetSymbolMatch(cards);
            var valueMatch = GetValueMatch(cards);
            var consecutiveMatch = GetConsecutiveMatch(cards, symbolMatch.Item1 == SymbolMatch.AllMatch);

            var scoredCards = cards.OrderByDescending(x => x.Score).ThenBy(x => x.Value);
            Dictionary<TypeToScore, List<CardDto>> dictionary = new Dictionary<TypeToScore, List<CardDto>>();
            dictionary.Add(TypeToScore.Value, valueMatch.Item2);
            dictionary.Add(TypeToScore.Consecutive, consecutiveMatch.Item2);
            dictionary.Add(TypeToScore.Symbol, symbolMatch.Item2);
            dictionary.Add(TypeToScore.None, new List<CardDto>() { scoredCards.Last() });
            List<string> cardsMatch = new List<string>()
        {
            symbolMatch.Item1.ToString(), valueMatch.Item1.ToString(), consecutiveMatch.Item1.ToString()
        };

            if(cards.Count >= _maxCards)
                cardsMatch.Add(NeedsMaxCards);

            return GetPlayedPokerHand(dictionary, cardsMatch);
        }

        private Tuple<PokerHandDto, List<CardDto>> GetPlayedPokerHand(Dictionary<TypeToScore, List<CardDto>> scoredCards, List<string> matchPredicates)
        {
            for (var i = 0; i < myRules.Count; i++)
            {
                if (myRules[i].predicates.All(x => matchPredicates.Contains(x)))
                {
                    List<CardDto> myCards = new List<CardDto>();

                    if (scoredCards.ContainsKey(myRules[i].typeToScore))
                        myCards = scoredCards[myRules[i].typeToScore];

                    return Tuple.Create(myRules[i].pokerHand, myCards);
                }
            }

            return Tuple.Create(new PokerHandDto("", 0, new ScoreDto(), 0), new List<CardDto>());
        }

        public PokerHandDto ChangePokerHandDto(PokerHandDto handDto)
        {
            for (var i = 0; i < myRules.Count; i++)
            {
                if (myRules[i].pokerHand.Name != handDto.Name)
                    continue;
                myRules[i].pokerHand = handDto;
            }
            return handDto;
        }

        public PokerHandDto UpgradePokerHand(string pokerHandToUpgrade, int addedChips, int addedMult)
        {
            for (var i = 0; i < myRules.Count; i++)
            {
                if (myRules[i].pokerHand.Name != pokerHandToUpgrade)
                    continue;


                var newPokerHand = new PokerHandDto(pokerHandToUpgrade, myRules[i].pokerHand.Level + 1,
                                                    new ScoreDto(0, myRules[i].pokerHand.Score.ScoreValue + addedChips, myRules[i].pokerHand.Score.MultiplierValue + addedMult),
                                                    myRules[i].pokerHand.AmountOfTimesPlayed);

                myRules[i].pokerHand = newPokerHand;

                return myRules[i].pokerHand;
            }

            return new PokerHandDto();
        }

        public void AddToPlayAmount(PokerHandDto handDto, int timesToAdd = 1)
        {
            ChangePokerHandDto(new PokerHandDto(handDto.Name, handDto.Level, handDto.Score, handDto.AmountOfTimesPlayed + timesToAdd));
        }

        public List<PokerHandDto> GetPokerHandDtos()
        {
            return myRules.Select(x => x.pokerHand).ToList();
        }
        public List<Rule> GetRules()
        {
            return myRules.ToList();
        }
        public void LoadRules(List<Rule> pokerHands)
        {
            myRules = pokerHands.ToList();
        }

        private Tuple<SymbolMatch, List<CardDto>> GetSymbolMatch(List<CardDto> cards)
        {
            Symbols firstSymbol = cards[0].Symbol;

            for (var i = 1; i < cards.Count; i++)
            {
                if (firstSymbol != cards[i].Symbol)
                {
                    return Tuple.Create(SymbolMatch.None, new List<CardDto>());
                }
            }

            return Tuple.Create(SymbolMatch.AllMatch, cards);
        }

        private Tuple<ValueMatch, List<CardDto>> GetValueMatch(List<CardDto> cards)
        {
            var valuesDic = new Dictionary<CardValue, List<CardDto>>();
            var scoredList = new List<CardDto>();
            
            for (int i = 0; i < cards.Count; i++)
            {
                if (!valuesDic.ContainsKey(cards[i].Value))
                    valuesDic.Add(cards[i].Value, new List<CardDto>());

                valuesDic[cards[i].Value].Add(cards[i]);
            }

            ValueMatch matchResult = ValueMatch.None;

            foreach (var item in valuesDic)
            {
                if (item.Value.Count == 5)
                {
                    scoredList = item.Value;
                    matchResult = ValueMatch.Five;
                    break;
                }

                if (item.Value.Count == 4)
                {
                    scoredList = item.Value;
                    matchResult = ValueMatch.Four;
                    break;
                }

                if (item.Value.Count == 2)
                {
                    scoredList.AddRange(item.Value);

                    if (matchResult == ValueMatch.Three) matchResult = ValueMatch.FullHouse;
                    else if (matchResult == ValueMatch.Two) matchResult = ValueMatch.DoubleTwo;
                    else matchResult = ValueMatch.Two;
                }
                else if (item.Value.Count == 3)
                {
                    scoredList.AddRange(item.Value);
                    if (matchResult == ValueMatch.Two) matchResult = ValueMatch.FullHouse;
                    else matchResult = ValueMatch.Three;
                }
            }

            return Tuple.Create(matchResult, scoredList);
        }

        private Tuple<ConsecutiveValue, List<CardDto>> GetConsecutiveMatch(List<CardDto> cards, bool allSymbolMatch)
        {
            var orderList = cards.Select(x => x.Value).OrderBy(x => x).ToList();

            int straights = 1;
            int straightDiff = _straightDiff;
            for (var i = 1; i < orderList.Count; i++)
            {
                if (UnityEngine.Mathf.Abs(orderList[i] - orderList[i - 1]) > straightDiff)
                {
                    straights += 1;
                }
            }

            if (straights > 2)
                return Tuple.Create(ConsecutiveValue.None, new List<CardDto>());

            if (orderList.Contains(CardValue.Ace) && orderList.Contains(CardValue.Ten))
                return Tuple.Create(allSymbolMatch ? ConsecutiveValue.RoyalStraight : ConsecutiveValue.Straight, cards);
            
            if (straights == 2)
                return Tuple.Create(ConsecutiveValue.None, new List<CardDto>());

            return Tuple.Create(ConsecutiveValue.Straight, cards);
        }
    }


    [Serializable]
    public class Rule
    {
        public PokerHandDto pokerHand;
        public string[] predicates;
        public PokerRules.TypeToScore typeToScore;

        public Rule(PokerHandDto pokerHand, PokerRules.TypeToScore typeToScore, params string[] predicates)
        {
            this.pokerHand = pokerHand;
            this.predicates = predicates;
            this.typeToScore = typeToScore;
        }
    }
}