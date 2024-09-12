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
using BepInEx.Logging;
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.Status
{
    public sealed class WasteNotEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(WasteNotStatus);
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
            return ResourceLoader.LoadSprite("WasteNotSe.png", BepinexPlugin.embeddedSource);
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
    [EntityLogic(typeof(WasteNotEffect))]
    public sealed class WasteNotStatus : StatusEffect
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
            base.NotifyActivating();
            int level = base.GetSeLevel<WasteNotStatus>();
            yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, level, null, null, null, 0f, true);
            yield break;
        }
    }
}
