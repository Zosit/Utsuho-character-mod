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
    public sealed class BunkerDownDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(BunkerDown);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(BepinexPlugin.embeddedSource);
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
                Index: 13360,
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
                Rarity: Rarity.Uncommon,
                Type: CardType.Defense,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Red = 2, Any = 2 },
                UpgradedCost: new ManaGroup() { Red = 2, Any = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: 14,
                UpgradedBlock: 18,
                Shield: null,
                UpgradedShield: null,
                Value1: 16,
                UpgradedValue1: 22,
                Value2: null,
                UpgradedValue2: null,
                Mana: new ManaGroup() { Red = 1, Any = 1 },
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

                Keywords: Keyword.Debut,
                UpgradedKeywords: Keyword.Debut,
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

        [EntityLogic(typeof(BunkerDownDef))]
        public sealed class BunkerDown : Card
        {
            protected override string GetBaseDescription()
            {
                if (base.DebutActive)
                {
                    return base.GetBaseDescription();
                }
                return this.ExtraDescription1;
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return base.DefenseAction();
                if (base.PlayInTriggered)
                {
                    yield return base.DefenseAction(0, Value1);
                    this.DecreaseBaseCost(this.Mana);
                }
                yield break;
            }

        }

    }
}
