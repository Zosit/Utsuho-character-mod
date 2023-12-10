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
    public sealed class DysonSphereDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DysonSphere);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            return UsefulFunctions.LocalizationCard(directorySource);
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: 13540,
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
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Ability,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Red = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Black = 1, Red = 1, Any = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 20,
                UpgradedValue1: 30,
                Value2: 3,
                UpgradedValue2: 5,
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

                RelativeEffects: new List<string>() { "HeatStatus", "DysonSphereStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus", "DysonSphereStatus" },
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(DysonSphereDefinition))]
        public sealed class DysonSphere : Card
        {


            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                if (!Battle.BattleShouldEnd)
                {
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, new int?(Value1), null, null, null, 0f, true);
                    if (!this.IsUpgraded)
                    {
                        yield return new AddCardsToHandAction(Library.CreateCard("DarkMatter"), Library.CreateCard("DarkMatter"), Library.CreateCard("DarkMatter"));
                    }
                    else
                    {
                        yield return new AddCardsToHandAction(Library.CreateCard("DarkMatter"), Library.CreateCard("DarkMatter"), Library.CreateCard("DarkMatter"), Library.CreateCard("DarkMatter"), Library.CreateCard("DarkMatter"));
                    }
                    yield return new ApplyStatusEffectAction<DysonSphereStatus>(Battle.Player, 1, null, null, null, 0f, true);
                    yield break;
                }
            }

        }

    }
}
