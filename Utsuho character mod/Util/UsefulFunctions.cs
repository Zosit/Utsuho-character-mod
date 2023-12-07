using LBoL.Base.Extensions;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoLEntitySideloader.Resource;
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
                if (card.Id == "DarkMatter") 
                    return card; 
            }
            return cards.Sample(cards[0].GameRun.BattleRng);
        }
        public static Card RandomUtsuho(IReadOnlyList<Card> cards)
        {
            foreach (Card card in cards)
            {
                if (card.Id == "DarkMatter")
                    return card;
            }
            return cards.Sample(cards[0].GameRun.BattleRng);
        }

        public static GlobalLocalization LocalizationCard(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "CardsEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "CardsKo.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationStatus(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "StatusEffectsEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "StatusEffectsKo.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationExhibit(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "ExhibitsEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "ExhibitsKo.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationUlt(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "UltimateSkillEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "UltimateSkillKo.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationPlayer(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "PlayerUnitEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "PlayerUnitKo.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationModel(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "PlayerModelEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "PlayerModelKo.yaml");
            return loc;
        }
    }
}
