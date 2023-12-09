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
using static Utsuho_character_mod.CardsR.ConflagrationDefinition;
using LBoL.EntityLib.StatusEffects.Marisa;

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
            return UsefulFunctions.LocalizationStatus(directorySource);
        }

        [DontOverwrite]
        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("EnergyStatus.png", BepinexPlugin.embeddedSource);
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
    [EntityLogic(typeof(EnergyEffect))]
    public sealed class HeatStatus : StatusEffect
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
            ReactOwnerEvent(Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnStarted));
        }
        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            int level = base.GetSeLevel<HeatStatus>();

            if (level >= 5)
            {
                yield return new DamageAction(base.Owner, base.Battle.EnemyGroup.Alives, DamageInfo.Reaction((float)(level / 5)), this.GunName, GunType.Single);
            }
            if (Battle.BattleShouldEnd)
            {
                yield break;
            }
            NotifyActivating();
            if (base.Battle.Player.GetStatusEffect<ConflagrationStatus>() != null)
            {
                yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, (level / 5), null, null, null, 0f, true);
            }
            else
            {
                yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, -(level / 5), null, null, null, 0f, true);
            }
            yield break;
        }
    }
}
