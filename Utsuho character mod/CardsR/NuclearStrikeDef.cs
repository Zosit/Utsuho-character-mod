﻿using LBoL.Base;
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
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsR
{
    public sealed class NuclearStrikeDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(NuclearStrike);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(BepinexPlugin.embeddedSource);
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
                Index: 13481,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "ERinShoot1",
                GunNameBurst: "ERinShoot1",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: false,
                FindInBattle: false,
                HideMesuem: true,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Attack,
                TargetType: TargetType.AllEnemies,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Red = 1 },
                UpgradedCost: new ManaGroup() { Red = 1 },
                MoneyCost: null,
                Damage: 0,
                UpgradedDamage: 0,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 0,
                UpgradedValue1: 0,
                Value2: null,
                UpgradedValue2: null,
                Mana: null,
                UpgradedMana: null,
                Scry: null,
                UpgradedScry: null,
                ToolPlayableTimes: null,
                Kicker: null,
                UpgradedKicker: null,

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

                Keywords: Keyword.Accuracy,
                UpgradedKeywords: Keyword.Accuracy | Keyword.Echo,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Zosit",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(NuclearStrikeDefinition))]
        public sealed class NuclearStrike : Card
        {
            public override int AdditionalDamage
            {
                get
                {
                    int level = base.GetSeLevel<HeatStatus>();
                    return level;
                }
            }

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return base.AttackAction(selector);

                if (!base.Battle.BattleShouldEnd)
                {
                    int level = base.GetSeLevel<HeatStatus>();
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player,-level, null, null, null, 0f, true);
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, new int?(base.Value1), null, null, null, 0f, true);
                    yield break;
                }
            }

        }

    }
}
