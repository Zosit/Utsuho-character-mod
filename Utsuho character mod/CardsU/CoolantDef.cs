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

namespace Utsuho_character_mod.CardsU
{
    public sealed class CoolantDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Coolant);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(directorySource);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "Utsuho\\Localization\\CardsEn.yaml");
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
                Type: CardType.Ability,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Blue },
                IsXCost: false,
                Cost: new ManaGroup() { Blue = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Blue = 1, Any = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 4,
                UpgradedValue1: 6,
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

                RelativeEffects: new List<string>() { "Spirit" },
                UpgradedRelativeEffects: new List<string>() { "Spirit" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(CoolantDef))]
        public sealed class Coolant : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return BuffAction<Spirit>(base.Value1, 0, 0, 0, 0.2f);
                List<Card> list = base.Battle.HandZone.Where((Card card) => card.CardType != CardType.Defense).ToList<Card>();
                yield return new DiscardManyAction(list);
                yield break;
            }
        }
    }
}
