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
    public sealed class CollapsingStarEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(CollapsingStarStatus);
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
    [EntityLogic(typeof(CollapsingStarEffect))]
    public sealed class CollapsingStarStatus : StatusEffect
    {
        public ManaGroup Mana { get; set; } = ManaGroup.Philosophies(1);
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToDiscard, new EventSequencedReactor<CardsEventArgs>(this.OnAddCard));
            base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new EventSequencedReactor<CardsEventArgs>(this.OnAddCard));
            base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToExile, new EventSequencedReactor<CardsEventArgs>(this.OnAddCard));
            base.ReactOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddedToDrawZone, new EventSequencedReactor<CardsAddingToDrawZoneEventArgs>(this.OnAddCardToDraw));
            base.ReactOwnerEvent<StatusEffectApplyEventArgs>(base.Battle.Player.StatusEffectAdding, new EventSequencedReactor<StatusEffectApplyEventArgs>(this.OnStatusApplied));
        }
        private IEnumerable<BattleAction> OnStatusApplied(StatusEffectApplyEventArgs args)
        {
            StatusEffect se = args.Effect;
            if (se is HeatStatus)
            {
                int addCount = (args.Level.Value / 10);
                addCount *= this.Level;
                ManaGroup mana = new ManaGroup() { Philosophy = addCount };
                yield return new GainManaAction(mana);
            }
            yield break;
        }
        private IEnumerable<BattleAction> OnAddCard(CardsEventArgs args)
        {
            foreach (Card card in args.Cards)
            {
                if (card is DarkMatter)
                {
                    yield return new DrawManyCardAction(this.Level);
                }
            }
            yield break;
        }
        private IEnumerable<BattleAction> OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            foreach (Card card in args.Cards)
            {
                if (card is DarkMatter)
                {
                    yield return new DrawManyCardAction(this.Level);
                }
            }
            yield break;
        }
    }
}
