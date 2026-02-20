using SimulatedBE.Networking.Consumable;
using SimulatedBE.Networking.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimulatedBE.Networking.Tags
{
    public class TagFactory
    {
        public static Dictionary<string, ITag> CreateTags(IJsonFacade jsonFacade, GameRules rules, TagManager tagManager)
        {
            var myTags = new Dictionary<string, ITag>();
            myTags.Add(TagsNames.Mark_Focus, new Tag_Focus(jsonFacade, tagManager));
            myTags.Add(TagsNames.Mark_Fortune, new Tag_Fortune(jsonFacade, ShopManager.FarmCardsPoolName, JSONFilesNames.Preset_Shop_FarmCards));
            myTags.Add(TagsNames.Mark_Momentum, new Tag_Momentum(jsonFacade, 5));
            myTags.Add(TagsNames.Mark_Patience, new Tag_Patience());
            myTags.Add(TagsNames.Mark_Sacrifice, new Tag_Sacrifice(jsonFacade));

            return myTags;
        }
    }
}
