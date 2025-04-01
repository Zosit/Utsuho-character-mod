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
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Util;
using LBoL.EntityLib.StatusEffects.ExtraTurn;

namespace Utsuho_character_mod.Status
{
    public sealed class CautionEffect : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(CautionStatus);
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
                            Keywords: Keyword.None,
                            RelativeEffects: new List<string>() { },
                            VFX: "Default",
                            VFXloop: "Default",
                            SFX: "Default"
                );
            return statusEffectConfig;
        }
    }
    [EntityLogic(typeof(CautionEffect))]
    public sealed class CautionStatus : StatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            ReactOwnerEvent(Owner.TurnStarting, new EventSequencedReactor<UnitEventArgs>(OnOwnerTurnStarting));
        }
        private IEnumerable<BattleAction> OnOwnerTurnStarting(UnitEventArgs args)
        {
            if (!Battle.BattleShouldEnd)
            {
                yield return new ApplyStatusEffectAction<Burst>(Owner, 1, null, null, null, 0f, true);
                yield return new ApplyStatusEffectAction<TimeIsLimited>(Battle.Player, 1, null, null, null, 0f, true);
            }
        }
    }
}
