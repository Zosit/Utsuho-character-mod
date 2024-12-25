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
using HarmonyLib;

namespace Utsuho_character_mod.CardsMulti
{
    public sealed class AblativeArmorDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(AblativeArmor);
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
                Index: 13527,
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
                Type: CardType.Defense,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Red = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Any = 4 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: 20,
                UpgradedBlock: 25,
                Shield: 0,
                UpgradedShield: 0,
                Value1: 10,
                UpgradedValue1: 14,
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

                Keywords: Keyword.Exile | Keyword.Retain,
                UpgradedKeywords: Keyword.Retain,
                EmptyDescription: false,
                RelativeKeyword: Keyword.Block | Keyword.Shield | Keyword.Exile,
                UpgradedRelativeKeyword: Keyword.Block | Keyword.Shield | Keyword.Exile,

                RelativeEffects: new List<string>() { "MassStatus" },
                UpgradedRelativeEffects: new List<string>() { "MassStatus" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "AltAlias",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(AblativeArmorDef))]
        public sealed class AblativeArmor : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                if (!Battle.BattleShouldEnd)
                {
                    DeltaShield = 0;
                    if (Battle.HandZone.Count != 0)
                    {
                        Card card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
                        while((card is UtsuhoCard uCard) && (uCard.isMass))
                        {
                            yield return new ExileCardAction(card);
                            DeltaShield += Value1;
                            if (Battle.HandZone.Count != 0)
                                card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
                            else
                                break;
                        }
                    }
                    yield return base.DefenseAction();
                    DeltaShield = 0;

                    yield break;
                }
            }

        }

    }
}
