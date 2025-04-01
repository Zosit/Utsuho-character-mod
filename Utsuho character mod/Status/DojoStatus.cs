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
    public sealed class DojoEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DojoStatus);
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
                            HasLevel: false,
                            LevelStackType: StackType.Add,
                            HasDuration: false,
                            DurationStackType: StackType.Add,
                            DurationDecreaseTiming: DurationDecreaseTiming.TurnEnd,
                            HasCount: false,
                            CountStackType: StackType.Keep,
                            LimitStackType: StackType.Keep,
                            ShowPlusByLimit: false,
                            Keywords: Keyword.Upgrade,
                            RelativeEffects: new List<string>() { },
                            VFX: "Default",
                            VFXloop: "Default",
                            SFX: "Default"
                );
            return statusEffectConfig;
        }
    }
    [EntityLogic(typeof(DojoEffect))]
    public sealed class DojoStatus : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            base.HandleOwnerEvent<CardEventArgs>(base.Battle.CardDrawn, new GameEventHandler<CardEventArgs>(this.OnCardDrawn));
        }
        private void OnCardDrawn(CardEventArgs args)
        {
            if (base.Battle.BattleShouldEnd)
            {
                return;
            }
            if (args.Cause == ActionCause.TurnStart)
            {
                return;
            }
            if (args.Card.CanUpgrade)
            {
                this.React(new UpgradeCardAction(args.Card));
            }
        }
    }
}
