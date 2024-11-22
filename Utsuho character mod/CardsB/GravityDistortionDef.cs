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
using LBoL.EntityLib.StatusEffects.Basic;

namespace Utsuho_character_mod.CardsB
{
    public sealed class GravityDistortionDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(GravityDistortion);
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
                Index: 13110,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "冰封噩梦",
                GunNameBurst: "冰封噩梦",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Common,
                Type: CardType.Attack,
                TargetType: TargetType.AllEnemies,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Black = 1, Any = 2 },
                MoneyCost: null,
                Damage: 17,
                UpgradedDamage: 20,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: null,
                UpgradedValue1: null,
                Value2: 1,
                UpgradedValue2: 2,
                Mana: new ManaGroup() { Black = 2 },
                UpgradedMana: new ManaGroup() { Philosophy = 2 },
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

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "Reflect", "Weak" },
                UpgradedRelativeEffects: new List<string>() { "Reflect", "Weak" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(GravityDistortionDef))]
        public sealed class GravityDistortion : UtsuhoCard
        {
            public GravityDistortion() : base()
            {
                isMass = true;
            }
            public override IEnumerable<BattleAction> OnPull()
            {
                yield return new GainManaAction(Mana);
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {

                if (!base.Battle.BattleShouldEnd)
                {
                    foreach (BattleAction battleAction in base.DebuffAction<Weak>(selector.GetUnits(base.Battle), 0, base.Value2, 0, 0, true, 0.2f))
                    {
                        yield return battleAction;
                    }
                    yield return base.AttackAction(selector);
                    yield break;
                }
            }

        }

    }
}
