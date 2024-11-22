using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Status;
using static Utsuho_character_mod.CardsB.DarkMatterDef;
using LBoL.Base.Extensions;
using JetBrains.Annotations;
using System.Linq;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsU
{
    public sealed class ResonanceDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Resonance);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: 13620,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "Simple1",
                GunNameBurst: "Simple1",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Blue },
                IsXCost: false,
                Cost: new ManaGroup() { Blue = 1 },
                UpgradedCost: new ManaGroup() { Blue = 1 },
                MoneyCost: null,
                Damage: 6,
                UpgradedDamage: 10,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 3,
                UpgradedValue1: 5,
                Value2: null,
                UpgradedValue2: null,
                Mana: null,
                UpgradedMana: null,
                Scry: null,
                UpgradedScry: null,
                ToolPlayableTimes: null,

                Loyalty: null,
                UpgradedLoyalty: null,
                PassiveCost: null,
                UpgradedPassiveCost: null,
                ActiveCost: null,
                ActiveCost2: null,
                UpgradedActiveCost: null,
                UpgradedActiveCost2: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.Exile | Keyword.Ethereal,
                UpgradedKeywords: Keyword.Exile | Keyword.Ethereal,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(ResonanceDef))]
        public sealed class Resonance : Card
        {
            public override int AdditionalDamage
            {
                get
                {
                    if (base.Battle == null)
                    {
                        return 0;
                    }
                    if (!this.IsUpgraded)
                    {
                        return base.GetSeLevel<ResonanceStatus>();
                    }
                    else
                    {
                        return base.GetSeLevel<ResonancePlusStatus>();
                    }
                }
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return AttackAction(selector);
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                if (!this.IsUpgraded)
                {
                    yield return base.BuffAction<ResonanceStatus>(base.Value1, 0, 0, 0, 0.2f);
                }
                else
                {
                    yield return base.BuffAction<ResonancePlusStatus>(base.Value1, 0, 0, 0, 0.2f);
                }
                yield break;
            }
        }
    }
}
