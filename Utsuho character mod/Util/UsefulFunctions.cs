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
                if (battle.Player.HasStatusEffect<QuantumReflectorStatus>())
                {
                    QuantumReflectorStatus status = battle.Player.GetStatusEffect<QuantumReflectorStatus>();
                    yield return new ApplyStatusEffectAction<Reflect>(battle.Player, status.Level, null, null, null, 0f, true);
                }
                if (battle.Player.HasStatusEffect<MassDriverStatus>())
                {
                    MassDriverStatus status = battle.Player.GetStatusEffect<MassDriverStatus>();
                    yield return new DamageAction(battle.Player, battle.EnemyGroup.Alives, DamageInfo.Reaction((float)status.Level), "无差别起火", GunType.Single);
                }
                foreach (BattleAction action in uCard.OnPull()) { yield return action; }
            }
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
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "UnitModelEn.yaml");
            loc.LocalizationFiles.AddLocaleFile(Locale.Ko, "UnitModelKo.yaml");
            return loc;
        }
    }
}
