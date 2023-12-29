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
            return ResourceLoader.LoadSprite("ReactorStartSe.png", BepinexPlugin.embeddedSource);
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
                            RelativeEffects: new List<string>() { "HeatStatus" },
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
            ReactOwnerEvent(Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnStarted));
            ReactOwnerEvent(Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnEnding));
        }
        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
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
        }
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
    }
}
