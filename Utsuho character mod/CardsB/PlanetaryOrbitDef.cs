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

namespace Utsuho_character_mod.CardsR
{
    public sealed class PlanetaryOrbitDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(PlanetaryOrbit);
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
                Rarity: Rarity.Rare,
                Type: CardType.Attack,
                TargetType: TargetType.AllEnemies,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 2, Any = 2 },
                UpgradedCost: new ManaGroup() { Black = 2, Any = 1 },
                MoneyCost: null,
                Damage: 0,
                UpgradedDamage: 0,
                Block: 0,
                UpgradedBlock: 0,
                Shield: null,
                UpgradedShield: null,
                Value1: 1,
                UpgradedValue1: 1,
                Value2: 1,
                UpgradedValue2: 1,
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

                Keywords: Keyword.Accuracy,
                UpgradedKeywords: Keyword.Accuracy,
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

        [EntityLogic(typeof(PlanetaryOrbitDef))]
        public sealed class PlanetaryOrbit : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                Card[] array = base.Battle.DrawZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                Card[] array2 = base.Battle.DiscardZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                if (array.Length != 0)
                {
                    foreach (Card card in array)
                    {
                        //if (card.Id == "DarkMatter")
                        //{
                        yield return new DiscardAction(card);
                        this.DeltaDamage += Value1;
                        //}
                    }
                }
                if (array2.Length != 0)
                {
                    foreach (Card card in array2)
                    {
                        //if (card.Id == "DarkMatter")
                        //{
                        yield return new MoveCardToDrawZoneAction(card, 0);
                        this.DeltaBlock += Value2;
                        //}
                    }
                    yield return new ReshuffleAction();
                }
                //for (int i = 0; i < array.Length; i++)
                //{
                    yield return AttackAction(selector);
                //}
                //for (int i = 0; i < array.Length; i++)
                //{
                    yield return DefenseAction();
                //}
                this.DeltaDamage = 0;
                this.DeltaBlock = 0;

                yield break;
            }

        }

    }
}
