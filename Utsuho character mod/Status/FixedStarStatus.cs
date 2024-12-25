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
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Util;
using LBoL.Core.Cards;
using LBoL.Core.Battle.Interactions;
using LBoL.Base.Extensions;
using System.Linq;
using static Utsuho_character_mod.CardsB.DarkMatterDef;

namespace Utsuho_character_mod.Status
{
    public sealed class FixedStarEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(FixedStarStatus);
        }

        [DontOverwrite]
        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        [DontOverwrite]
        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite(GetId() + ".png", BepinexPlugin.embeddedSource);
        }

        public override StatusEffectConfig MakeConfig()
        {
            var statusEffectConfig = new StatusEffectConfig(
                            Id: "",
                            Index: 0,
                            Order: 10,
                            Type: StatusEffectType.Positive,
                            IsVerbose: false,
                            IsStackable: true,
                            StackActionTriggerLevel: null,
                            HasLevel: true,
                            LevelStackType: StackType.Add,
                            HasDuration: false,
                            DurationStackType: StackType.Add,
                            DurationDecreaseTiming: DurationDecreaseTiming.Custom,
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
    [EntityLogic(typeof(FixedStarEffect))]
    public sealed class FixedStarStatus : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            //ReactOwnerEvent(Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnStarted));
            ReactOwnerEvent(Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnEnding));

            /*foreach (Card card in base.Battle.EnumerateAllCards())
            {
                if (card is DarkMatter)
                {
                    card.IsAutoExile = true;
                }
            }*/
            base.HandleOwnerEvent<CardEventArgs>(base.Battle.CardDrawn, new GameEventHandler<CardEventArgs>(this.OnDrawCard));
            /*base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(this.OnAddCardToDraw));*/
        }
        /*private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            if (Battle.BattleShouldEnd)
            {
                yield break;
            }
            NotifyActivating();
            for (int i = 0; i < this.Level; i++)
            {
                Card card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                yield return new DiscardAction(card);
            }
            yield break;
        }*/
        private IEnumerable<BattleAction> OnOwnerTurnEnding(UnitEventArgs args)
        {
            if (Battle.BattleShouldEnd)
            {
                yield break;
            }
            NotifyActivating();
            IReadOnlyList<Card> selectedCards = null;
            List<Card> list = (base.Battle.HandZone.ToList<Card>());
            if (!list.Empty<Card>())
            {
                SelectHandInteraction interaction = new SelectHandInteraction(0, this.Level, list)
                {
                    Source = this
                };
                yield return new InteractionAction(interaction, false);
                selectedCards = interaction.SelectedCards.ToList();
            }
            if (selectedCards != null)
            {
                foreach (Card card in selectedCards)
                {
                    card.IsTempRetain = true;
                }
            }
            yield break;
        }
        private void OnDrawCard(CardEventArgs args)
        {
            if (args.Card is DarkMatter)
            {
                args.Card.IsAutoExile = true;
            }
        }
        /* void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            foreach (Card card in args.Cards)
            {
                if (card is DarkMatter)
                {
                    card.IsAutoExile = true;
                }
            }
        */
    }
}
