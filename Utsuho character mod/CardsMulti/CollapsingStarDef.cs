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

namespace Utsuho_character_mod.CardsMulti
{
    public sealed class CollapsingStarDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(CollapsingStar);
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
                Index: 13670,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "Simple1",
                GunNameBurst: "Simple1",
                DebugLevel: 0,
                Revealable: true,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Ability,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red, ManaColor.Colorless },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 2, Red = 2, Colorless = 2 },
                UpgradedCost: new ManaGroup() { Black = 2, Red = 2, Colorless = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 1,
                UpgradedValue1: 2,
                Value2: null,
                UpgradedValue2: null,
                Mana: new ManaGroup() { Philosophy = 1 },
                UpgradedMana: new ManaGroup() { Philosophy = 2 },
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

                Keywords: Keyword.Ethereal,
                UpgradedKeywords: Keyword.Ethereal,
                EmptyDescription: false,
                RelativeKeyword: Keyword.Philosophy,
                UpgradedRelativeKeyword: Keyword.Philosophy,

                RelativeEffects: new List<string>() { "CollapsingStarStatus" },
                UpgradedRelativeEffects: new List<string>() { "CollapsingStarStatus" },
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "AltAlias",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(CollapsingStarDefinition))]
        public sealed class CollapsingStar : Card
        {


            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                if (!Battle.BattleShouldEnd)
                {
                    yield return new ApplyStatusEffectAction<CollapsingStarStatus>(Battle.Player, Value1, null, 0, null, 0f, true);
                }
            }

        }

    }
}
