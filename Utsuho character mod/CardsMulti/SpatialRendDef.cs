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
    public sealed class SpatialRendDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(SpatialRend);
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
                Index: 13524,
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
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Red = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Black = 1, Red = 1, Any = 2 },
                MoneyCost: null,
                Damage: 20,
                UpgradedDamage: 20,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 3,
                UpgradedValue1: 6,
                Value2: 20,
                UpgradedValue2: 20,
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

                Keywords: Keyword.Accuracy | Keyword.Exile,
                UpgradedKeywords:  Keyword.Accuracy,
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

        [EntityLogic(typeof(SpatialRendDef))]
        public sealed class SpatialRend : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                if (!base.Battle.BattleShouldEnd)
                {
                    this.DeltaDamage = 0;
                    for (int i = 0; i < Value1; i++)
                    {
                        if ((Battle.ExileZone.Count != 0) && (Battle.HandZone.Count != Battle.MaxHand))
                        {
                            IReadOnlyList<Card> exileZone = Battle.ExileZone;
                            Card card = Util.UsefulFunctions.RandomUtsuho(exileZone);
                            foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                            if ((card is UtsuhoCard uCard) && (uCard.isMass))
                            {
                                this.DeltaDamage += Value2;
                                yield return new MoveCardAction(card, CardZone.Hand);
                            }
                        }
                    }

                    yield return base.AttackAction(selector);
                    this.DeltaDamage = 0;

                    yield break;
                }
            }

        }

    }
}
