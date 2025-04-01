﻿using LBoL.ConfigData;
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
using System.Linq;
using Utsuho_character_mod.CardsR;

namespace Utsuho_character_mod.Status
{
    public sealed class EnergyEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(HeatStatus);
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
                            RelativeEffects: new List<string>() { },
                            VFX: "Default",
                            VFXloop: "Default",
                            SFX: "Default"
                );
            return statusEffectConfig;
        }
    }
    [EntityLogic(typeof(EnergyEffect))]
    public sealed class HeatStatus : StatusEffect
    {
        private string GunName
        {
            get
            {
                return "GuihuoExplodeR2";
            }
        }
        public int HeatDamage
        {
            get
            {
                return Level / 5;
            }
        }
        protected override void OnAdded(Unit unit)
        {
            ReactOwnerEvent(Owner.TurnEnding, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnEnding));
        }
        private IEnumerable<BattleAction> OnOwnerTurnEnding(UnitEventArgs args)
        {
            int level = this.Level;
            if (!Battle.BattleShouldEnd)
            {
                if (level >= 5)
                {
                    if (base.Owner == Battle.Player)
                    {
                        /*if (base.Battle.Player.GetStatusEffect<ConflagrationStatus>() != null)
                        {
                            yield return new DamageAction(base.Owner, base.Battle.EnemyGroup.Alives, DamageInfo.Reaction((float)(2 * (level / 5))), this.GunName, GunType.Single);
                        }
                        else*/
                        //{
                            yield return new DamageAction(base.Owner, base.Battle.EnemyGroup.Alives, DamageInfo.Reaction((float)(level / 5)), this.GunName, GunType.Single);
                        //}
                    }
                    else
                    {
                        yield return new DamageAction(base.Owner, base.Battle.Player, DamageInfo.Reaction((float)((level / 5))), this.GunName, GunType.Single);
                    }
                }
            }


            if (!Battle.BattleShouldEnd)
            {
                yield return new ApplyStatusEffectAction<HeatStatus>(base.Owner, -(level / 5), null, null, null, 0f, true);
            }
            yield break;
        }
    }
}
