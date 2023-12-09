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
using Utsuho_character_mod.Util;
using System.Linq;

namespace Utsuho_character_mod.CardsR
{
    public sealed class WarpShieldDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(WarpShield);
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
                Index: sequenceTable.Next(typeof(CardConfig)),
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
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 2, Any = 1 },
                UpgradedCost: new ManaGroup() { Black = 1, Any = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: 18,
                UpgradedBlock: 24,
                Shield: null,
                UpgradedShield: null,
                Value1: 6,
                UpgradedValue1: 8,
                Value2: 2,
                UpgradedValue2: 2,
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

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(WarpShieldDef))]
        public sealed class WarpShield : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                DeltaBlock = 0;
                for (int i = 0; i < Value2; i++)
                {
                    if (Battle.DrawZone.Count != 0)
                    {
                        IReadOnlyList<Card> drawZoneIndexOrder = base.Battle.DrawZoneIndexOrder;
                        Card card = Util.UsefulFunctions.RandomUtsuho(drawZoneIndexOrder);
                        foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                        if (card.Id == "DarkMatter")
                        {
                            DeltaBlock += Value1;
                        }
                        yield return new MoveCardAction(card, CardZone.Hand);
                    }
                }
                yield return DefenseAction();
                yield break;
            }
        }
    }
}
