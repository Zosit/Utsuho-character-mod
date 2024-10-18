using LBoL.Base.Extensions;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Utsuho_character_mod.Status;

namespace Utsuho_character_mod.Util
{
    public abstract class UsefulFunctions
    {
        public static Card RandomUtsuho(Card[] cards)
        {
            foreach (Card card in cards)
            {
                if((card is UtsuhoCard uCard) && (uCard.isMass))
                {
                    return card;
                }
            }
            return cards.Sample(cards[0].GameRun.BattleRng);
        }
        public static Card RandomUtsuho(IReadOnlyList<Card> cards)
        {
            foreach (Card card in cards)
            {
                if ((card is UtsuhoCard uCard) && (uCard.isMass))
                {
                    return card;
                }
            }
            return cards.Sample(cards[0].GameRun.BattleRng);
        }
        public static IEnumerable<BattleAction> RandomCheck(Card card, BattleController battle)
        {
            if ((card is UtsuhoCard uCard) && (uCard.isMass))
            {
                int totalIterations = 1;
                /*if (battle.Player.HasStatusEffect<QuantumDissonanceStatus>())
                {
                    QuantumDissonanceStatus status = battle.Player.GetStatusEffect<QuantumDissonanceStatus>();
                    totalIterations += status.Level;
                }*/
                if (battle.Player.HasStatusEffect<MassDriverStatus>())
                {
                    MassDriverStatus status = battle.Player.GetStatusEffect<MassDriverStatus>();
                    for (int i = 0; i < totalIterations; i++)
                        yield return new DamageAction(battle.Player, battle.EnemyGroup.Alives, DamageInfo.Reaction((float)status.Level), "厄运之轮", GunType.Single);
                }
                for (int i = 0; i < totalIterations; i++)
                    foreach (BattleAction action in uCard.OnPull()) { yield return action; }
            }
        }

    public static GlobalLocalization LocalizationCard(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "CardEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "CardKo.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ja, "CardJa.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationStatus(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "StatusEffectEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "StatusEffectKo.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ja, "StatusEffectJa.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationExhibit(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "ExhibitEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "ExhibitKo.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ja, "ExhibitJa.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationUlt(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "UltimateSkillEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "UltimateSkillKo.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ja, "UltimateSkillJa.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationPlayer(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "PlayerUnitEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "PlayerUnitKo.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ja, "PlayerUnitJa.yaml");
            return loc;
        }
        public static GlobalLocalization LocalizationModel(DirectorySource dirsorc)
        {
            var loc = new GlobalLocalization(dirsorc);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "UnitModelEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "UnitModelKo.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ja, "UnitModelJa.yaml");
            return loc;
        }
    }
}
