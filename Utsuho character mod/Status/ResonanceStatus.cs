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
using LBoL.Core.Cards;
using Utsuho_character_mod.CardsR;
using static Utsuho_character_mod.CardsU.ResonanceDef;
using LBoL.EntityLib.Cards.Character.Cirno;
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.Status
{
    public sealed class ResonanceEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(ResonanceStatus);
        }

        [DontOverwrite]
        public override LocalizationOption LoadLocalization()
        {
            return UsefulFunctions.LocalizationStatus(directorySource);
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
    [EntityLogic(typeof(ResonanceEffect))]
    public sealed class ResonanceStatus : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            ReactOwnerEvent<CardEventArgs>(base.Battle.CardExiled, new EventSequencedReactor<CardEventArgs>(this.OnCardExiled));
        }
        private IEnumerable<BattleAction> OnCardExiled(CardEventArgs args)
        {
            if (Battle.BattleShouldEnd)
            {
                yield break;
            }
            if ((args.Cause != ActionCause.AutoExile) && (args.Card.BaseName != "Resonance"))
            {
                base.NotifyActivating();
                yield return new AddCardsToHandAction(Library.CreateCards<Resonance>(1, false));
            }
            yield break;
        }
    }
}
