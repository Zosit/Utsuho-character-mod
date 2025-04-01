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
    public sealed class NuclearDiveDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(NuclearDive);
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
                Index: 13490,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "博丽一拳",
                GunNameBurst: "博丽一拳",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Red = 3 },
                UpgradedCost: new ManaGroup() { Red = 3 },
                MoneyCost: null,
                Damage: 0,
                UpgradedDamage: 0,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: null,
                UpgradedValue1: null,
                Value2: null,
                UpgradedValue2: null,
                Mana: new ManaGroup() { Red = 4 },
                UpgradedMana: new ManaGroup() { Red = 4 },
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

                Keywords: Keyword.Accuracy | Keyword.Exile,
                UpgradedKeywords: Keyword.Accuracy | Keyword.Exile,
                EmptyDescription: false,
                RelativeKeyword: Keyword.Expel,
                UpgradedRelativeKeyword: Keyword.Expel,

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

        [EntityLogic(typeof(NuclearDiveDefinition))]
        public sealed class NuclearDive : Card
        {
            private bool enemyDied = false;
            private int tempDamage = 0;
            public override int AdditionalDamage
            {
                get
                {
                    if (tempDamage != 0)
                    {
                        return tempDamage;
                    }
                    else
                    {
                        int level = base.GetSeLevel<HeatStatus>();
                        if (!IsUpgraded)
                        {
                            return level * 2;
                        }
                        else
                        {
                            return level * 3;
                        }
                    }
                }
            }
            public int halfVent
            {
                get
                {
                    return (AdditionalDamage) / 2;
                }
            }
            protected override void OnEnterBattle(BattleController battle)
            {
                base.ReactBattleEvent<DieEventArgs>(base.Battle.EnemyDied, new EventSequencedReactor<DieEventArgs>(this.OnEnemyDied));
            }
            private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs args)
            {
                if (args.DieSource == this && !args.Unit.HasStatusEffect<Servant>())
                {
                    enemyDied = true;
                    int level = base.GetSeLevel<HeatStatus>();
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, -level, null, null, null, 0f, true);
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, level / 2, null, null, null, 0f, true);
                }
                yield break;
            }

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                int level = base.GetSeLevel<HeatStatus>();
                if (!IsUpgraded && (level != 0))
                {
                    tempDamage = level * 2;
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, level, null, null, null, 0f, true);
                }
                else
                {
                    tempDamage = level * 3;
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, level * 2, null, null, null, 0f, true);
                }

                if (!base.Battle.BattleShouldEnd)
                {
                    yield return base.AttackAction(selector.SelectedEnemy);
                }
                if (!base.Battle.BattleShouldEnd && (enemyDied == false))
                {
                    yield return new RemoveStatusEffectAction(Battle.Player.GetStatusEffect<HeatStatus>());
                }
                enemyDied = false;
                yield break;
            }
        }
    }
}
