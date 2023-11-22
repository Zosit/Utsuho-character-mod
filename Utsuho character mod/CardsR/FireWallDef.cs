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

namespace Utsuho_character_mod
{
    public sealed class FireWallDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(FireWall);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(BepinexPlugin.embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(BepinexPlugin.embeddedSource);
            loc.LocalizationFiles.AddLocaleFile(LBoL.Core.Locale.En, "CardsEn.yaml");
            return loc;
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: sequenceTable.Next(typeof(CardConfig)),
                Id: "",
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
                Rarity: Rarity.Uncommon,
                Type: CardType.Defense,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Red = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Red = 1, Any = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: 18,
                UpgradedBlock: 24,
                Shield: null,
                UpgradedShield: null,
                Value1: 20,
                UpgradedValue1: 20,
                Value2: null,
                UpgradedValue2: null,
                Mana: new ManaGroup() { Red = 1 },
                UpgradedMana: new ManaGroup() { Red = 1 },
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

                RelativeEffects: new List<string>() { "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus" },
                //RelativeCards: new List<string>() { "AyaNews" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(FireWallDef))]
        public sealed class FireWall : Card
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
                yield return base.DefenseAction();
                int level = base.GetSeLevel<HeatStatus>();
                if (level >= Value1)
                {
                    yield return new GainManaAction(Mana);
                }
                yield break;
            }
        }
    }
}
