using LBoL.ConfigData;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader;
using System;
using UnityEngine;
using LBoL.Core.StatusEffects;
using LBoL.Base;
using System.Collections.Generic;
using Mono.Cecil;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Presentation.UI.Panels;
using LBoL.Core.Units;
using System.Threading;
using LBoL.Core.Randoms;
using LBoL.Core.Cards;

namespace Utsuho_character_mod.Status
{
    public sealed class CautionEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(CautionStatus);
        }

        [DontOverwrite]
        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(BepinexPlugin.embeddedSource);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "StatusEffectsEn.yaml");
            return loc;
        }

        [DontOverwrite]
        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("ChargingStatus.png", BepinexPlugin.embeddedSource);
        }

        public override StatusEffectConfig MakeConfig()
        {
            var statusEffectConfig = new StatusEffectConfig(
                            Id: "",
                            Order: 10,
                            Type: StatusEffectType.Positive,
                            IsVerbose: false,
                            IsStackable: true,
                            StackActionTriggerLevel: null,
                            HasLevel: true,
                            LevelStackType: StackType.Add,
                            HasDuration: false,
                            DurationStackType: StackType.Add,
                            DurationDecreaseTiming: DurationDecreaseTiming.TurnEnd,
                            HasCount: false,
                            CountStackType: StackType.Keep,
                            LimitStackType: StackType.Keep,
                            ShowPlusByLimit: false,
                            Keywords: Keyword.None,
                            RelativeEffects: new List<string>() { },
                            VFX: "Default",
                            VFXloop: "Default",
                            SFX: "Default"
                );
            return statusEffectConfig;
        }
    }
    [EntityLogic(typeof(CautionEffect))]
    public sealed class CautionStatus : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            this.CardToHold(base.Battle.EnumerateAllCards());
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(this.OnAddCardToDraw));
        }
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            for (int i = 0; i < this.Level; i++)
            {
                Card card = base.Battle.RollCards(new CardWeightTable(RarityWeightTable.OnlyCommon, OwnerWeightTable.Valid, CardTypeWeightTable.CanBeLoot), 1, null)[0];
                if (card != null)
                {
                    card.IsRetain = true;
                    card.IsExile = true;
                    yield return new AddCardsToHandAction(new Card[] { card });
                }
            }
            yield break;
        }
        private void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            this.CardToHold(args.Cards);
        }

        private void OnAddCard(CardsEventArgs args)
        {
            this.CardToHold(args.Cards);
        }

        private void CardToHold(IEnumerable<Card> cards)
        {
            foreach (Card card in cards)
            {
                card.IsRetain = true;
                card.IsExile = true;
            }
        }
    }
}
