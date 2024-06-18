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
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsR
{
    public sealed class FlareUpDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(FlareUp);
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
                Index: 13130,
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
                Rarity: Rarity.Common,
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
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
                Value1: 2,
                UpgradedValue1: 2,
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
                UpgradedActiveCost: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "HeatStatus", "Weak", "Vulnerable" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus", "Weak", "Vulnerable" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(FlareUpDef))]
        public sealed class FlareUp : Card
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

                if (!base.Battle.BattleShouldEnd)
                {
                    int level = base.GetSeLevel<HeatStatus>();
                    if (this.IsUpgraded)
                    {
                        yield return base.DebuffAction<Weak>(selector.SelectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
                        yield return base.DebuffAction<Vulnerable>(selector.SelectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
                    }
                    yield return base.AttackAction(selector.SelectedEnemy);
                    if (level != 0) {
                        yield return new RemoveStatusEffectAction(Battle.Player.GetStatusEffect<HeatStatus>());
                    }
                    if (!this.IsUpgraded)
                    {
                        yield return base.DebuffAction<Weak>(selector.SelectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
                        yield return base.DebuffAction<Vulnerable>(selector.SelectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
                    }
                    yield break;
                }
            }

        }

    }
}
