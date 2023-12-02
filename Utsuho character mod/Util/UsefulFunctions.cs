using LBoL.Base.Extensions;
using LBoL.Core.Cards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Utsuho_character_mod.Util
{
    public abstract class UsefulFunctions
    {
        public static Card RandomUtsuho(Card[] cards)
        {
            foreach (Card card in cards)
            {
                if (card.BaseName == "Dark Matter") 
                    return card; 
            }
            return cards.Sample(cards[0].GameRun.BattleRng);
        }
        public static Card RandomUtsuho(IReadOnlyList<Card> cards)
        {
            foreach (Card card in cards)
            {
                if (card.BaseName == "Dark Matter")
                    return card;
            }
            return cards.Sample(cards[0].GameRun.BattleRng);
        }
    }
}
