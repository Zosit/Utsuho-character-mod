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
using System.Linq;
using LBoL.Base.Extensions;
using LBoL.EntityLib.StatusEffects.Neutral.TwoColor;

namespace Utsuho_character_mod.Status
{
    public sealed class HeatVisorEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(HeatVisorStatus);
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
    [EntityLogic(typeof(HeatVisorEffect))]
    public sealed class HeatVisorStatus : StatusEffect
    {
        private int ActiveTimes { get; set; }

        protected override void OnAdded(Unit unit)
        {
            ReactOwnerEvent(Owner.TurnStarting, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnStarting));
            base.HandleOwnerEvent<DamageEventArgs>(base.Battle.Player.DamageTaking, new GameEventHandler<DamageEventArgs>(this.OnPlayerDamageTaking));
            base.ReactOwnerEvent<DamageEventArgs>(base.Battle.Player.DamageReceived, new EventSequencedReactor<DamageEventArgs>(this.OnPlayerDamageReceived));
        }
        private IEnumerable<BattleAction> OnOwnerTurnStarting(UnitEventArgs args)
        {
            yield return new RemoveStatusEffectAction(this, true, 0.1f);
            yield break;
        }
        private void OnPlayerDamageTaking(DamageEventArgs args)
        {
            DamageInfo damageInfo = args.DamageInfo;
            if ((this.ActiveTimes < base.Level) && !damageInfo.IsGrazed)
            {
                base.NotifyActivating();
                int num2 = this.ActiveTimes + 1;
                this.ActiveTimes = num2;
                damageInfo.Damage = 0;
                damageInfo.DamageBlocked = 0;
                damageInfo.DamageShielded = 0;
                args.DamageInfo = damageInfo;
                args.AddModifier(this);
            }
        }
        private IEnumerable<BattleAction> OnPlayerDamageReceived(DamageEventArgs args)
        {
            base.Level -= this.ActiveTimes;
            this.ActiveTimes = 0;
            if (base.Level <= 0)
            {
                yield return new RemoveStatusEffectAction(this, true, 0.1f);
            }
            yield break;
        }
    }
}
