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
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Util;
using LBoL.Core.Units;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using static Utsuho_character_mod.CardsR.TokamakDefinition;
using LBoL.EntityLib.StatusEffects.Marisa;

namespace Utsuho_character_mod.Status
{
    public sealed class SunSpotEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(SunSpotStatus);
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
                            ImageId: null,
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
    [EntityLogic(typeof(SunSpotEffect))]
    public sealed class SunSpotStatus : StatusEffect
    {
        private string GunName
        {
            get
            {
                if (base.Level <= 50)
                {
                    return "无差别起火";
                }
                return "无差别起火B";
            }
        }
        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
        }
        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card.Id == "DarkMatter")
            {
                int dyLevel = base.GetSeLevel<SunSpotStatus>();

                for (int i = 0; i < dyLevel; i++)
                {
                    int level = base.GetSeLevel<HeatStatus>();
                    NotifyActivating();
                    if (level >= 5)
                    {
                        yield return new DamageAction(base.Owner, base.Battle.EnemyGroup.Alives, DamageInfo.Reaction((float)(level / 5)), this.GunName, GunType.Single);
                    }
                    if (Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                }
            }
            yield break;
        }
    }
}
