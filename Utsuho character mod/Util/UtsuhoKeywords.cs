using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.CustomKeywords;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using Utsuho_character_mod.Status;
using static LBoL.Core.Units.UltimateSkill;

namespace Utsuho_character_mod.Util
{
    public static class UtsuhoKeyword
    {
        public static CardKeyword Mass = new CardKeyword(nameof(MassStatus)) { descPos = KwDescPos.First };
        //public static CardKeyword Kicker = new CardKeyword(nameof(KickerStatus), true) { descPos = KwDescPos.First };
        //public static CardKeyword MultiKicker = new CardKeyword(nameof(MultiKickerStatus), true) { descPos = KwDescPos.First };
    }

    /*public sealed class MassEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(MassStatus);
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
                            RelativeEffects: new List<string>() { },
                            VFX: "Default",
                            VFXloop: "Default",
                            SFX: "Default"
                );
            return statusEffectConfig;
        }
    }

[EntityLogic(typeof(MassEffect))]
    public sealed class MassStatus : StatusEffect
    {

    }

    [EntityLogic(typeof(DC_DLKwSEDef))]
    public sealed class DC_DLKwSE : DStatusEffect, IOnTooltipDisplay, IOverrideSEBrief
    {
        public string DLMulDesc => (DoremyEvents.defaultDLMult * 100).ToString();

        public override string Name => base.Name.RuntimeFormat(FormatWrapper);

        int dlLevel = 0;

        public void OnTooltipDisplay(Card card)
        {
            if (card.TryGetCustomKeyword(DoremyKw.dLId, out DLKeyword dl))
            {
                dlLevel = dl.DreamLevel;
            }
        }

        public string OverrideBrief(string rawBrief)
        {
            return rawBrief + $"\ndeez:{dlLevel}";
        }
    }*/
}
