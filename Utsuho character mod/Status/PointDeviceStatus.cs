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
    public sealed class PointDeviceEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(PointDeviceStatus);
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
                            HasLevel: false,
                            LevelStackType: StackType.Add,
                            HasDuration: true,
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
    [EntityLogic(typeof(PointDeviceEffect))]
    public sealed class PointDeviceStatus : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            this.CardToFree(base.Battle.EnumerateAllCards());
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(this.OnAddCardToDraw));
        }
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card.BaseName == "Dark Matter")
            {
                yield return new MoveCardAction(args.Card, CardZone.Discard);
                yield return new DrawCardAction();
            }
        }
        private void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            this.CardToFree(args.Cards);
        }

        private void OnAddCard(CardsEventArgs args)
        {
            this.CardToFree(args.Cards);
        }

        private void CardToFree(IEnumerable<Card> cards)
        {
            foreach (Card card in cards)
            {
                if (card.BaseName == "Dark Matter")
                {
                    card.FreeCost = true;
                }
            }
        }

        protected override void OnRemoved(Unit unit)
        {
            foreach (Card card in base.Battle.EnumerateAllCards())
            {
                if (card.BaseName == "Dark Matter")
                {
                    card.FreeCost = false;
                }
            }
        }
    }
}
