using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using System;

namespace SimulatedBE.Networking.Tags
{
    public class TagManager
    {
        private readonly Dictionary<string, ITag> _myTags;
        private readonly IJsonFacade _jsonFacade;
        private readonly GameRules _gameRules;
        public event Action OnGetFirstHand;
        public event Action OnSkipBreach;

        public float Multiplier { get; set; }

        public TagManager(IJsonFacade facade, GameRules gameRules)
        {
            _jsonFacade = facade;
            _gameRules = gameRules;
            _myTags = TagFactory.CreateTags(_jsonFacade, gameRules, this);
            Multiplier = 1;
        }


        public void ApplyTag(TagDto tagToApply)
        {
            OnSkipBreach?.Invoke();
            if (!_myTags.TryGetValue(tagToApply.ID, out var tag)) return;

            var database = _jsonFacade.Database;
            database.currentTags.Add(tagToApply);
            _jsonFacade.Database = database;

            UnityEngine.Debug.Log("Tag Applied: " + tagToApply.ID);
            tag.ApplyTag(this);
        }

        public TagDto UpdateDynamicDescription(TagDto tagToUpdate)
        {
            if (!_myTags.TryGetValue(tagToUpdate.ID, out var tag)) return tagToUpdate;
            var dynamicDesc = tag.GetDynamicDescription();
            return new TagDto(tagToUpdate.ID, tagToUpdate.Name, tagToUpdate.Description, dynamicDesc, tagToUpdate.HandState);
        }

        public void GetFirstHand() => OnGetFirstHand?.Invoke();

        public void SetTagMultiplier(float multiplier)
        {
            Multiplier = multiplier;
        }
        public void DrawCards(int drawCardsCount) => _gameRules.DrawCards(drawCardsCount);
    }
}