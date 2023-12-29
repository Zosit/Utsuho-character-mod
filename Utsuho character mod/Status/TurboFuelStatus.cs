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
using System.Threading;
using LBoL.Core.Randoms;
using LBoL.Core.Cards;
using LBoL.Presentation;
using System.Collections;
using LBoL.Base.Extensions;
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.Status
{
    public sealed class TurboFuelEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(TurboFuelStatus);
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
                            DurationDecreaseTiming: DurationDecreaseTiming.TurnEnd,
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
    [EntityLogic(typeof(TurboFuelEffect))]
    public sealed class TurboFuelStatus : StatusEffect
    {
        public ManaGroup manatype
        {
            get
            {
                return new ManaGroup() { Philosophy = 1 };
            }
        }
        protected override void OnAdded(Unit unit)
        {
            ReactOwnerEvent(Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnStarted));
        }
        IEnumerator ResetTrigger()
        {
            yield return new WaitForSecondsRealtime(1.0f);
            NotifyChanged();
        }
        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            yield return new DrawManyCardAction(this.Level);
            yield return new GainManaAction(new ManaGroup() { Philosophy = this.Level });

            Card card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
            foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
            card.NotifyActivating();
            GameMaster.Instance.StartCoroutine(ResetTrigger());
            yield return new ExileCardAction(card);

            yield break;
        }
    }
}
