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
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: 13465,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "狐狸",
                GunNameBurst: "狐狸",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Attack,
                TargetType: TargetType.AllEnemies,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 2, Any = 2 },
                UpgradedCost: new ManaGroup() { Black = 1, Any = 2 },
                MoneyCost: null,
                Damage: 0,
                UpgradedDamage: 0,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 1,
                UpgradedValue1: 1,
                Value2: null,
                UpgradedValue2: null,
                Mana: null,
                UpgradedMana: null,
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

                Keywords: Keyword.Accuracy,
                UpgradedKeywords: Keyword.Accuracy,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

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

        [EntityLogic(typeof(PlanetaryOrbitDef))]
        public sealed class PlanetaryOrbit : Card
        {
            public override int AdditionalDamage
            {
                get
                {
                    if (base.Battle != null)
                    {
                        Card[] array = base.Battle.DrawZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                        Card[] array2 = base.Battle.DiscardZone.ToArray<Card>();
                        return Value1 * (array.Length + array2.Length);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                Card[] array = base.Battle.DrawZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                Card[] array2 = base.Battle.DiscardZone.ToArray<Card>();
                if (array.Length != 0)
                {
                    foreach (Card card in array)
                    {
                        yield return new DiscardAction(card);
                    }
                }
                if (array2.Length != 0)
                {
                    foreach (Card card in array2)
                    {
                        yield return new MoveCardToDrawZoneAction(card, DrawZoneTarget.Top);
                    }
                }
                yield return AttackAction(selector);
                array = base.Battle.DrawZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                array2 = base.Battle.DiscardZone.ToArray<Card>();
                if (array.Length != 0)
                {
                    foreach (Card card in array)
                    {
                        foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                    }
                }
                if (array2.Length != 0)
                {
                    foreach (Card card in array2)
                    {
                        foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                    }
                }


                this.DeltaDamage = 0;

                yield break;
            }

        }

    }
}
