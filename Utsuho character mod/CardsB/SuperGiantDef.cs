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
using UnityEngine;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsR
{
    public sealed class SuperGiantDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(SuperGiant);
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
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Any = 4 },
                UpgradedCost: new ManaGroup() { Black = 1, Any = 3 },
                MoneyCost: null,
                Damage: 30,
                UpgradedDamage: 30,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: null,
                UpgradedValue1: null,
                Value2: null,
                UpgradedValue2: null,
                Mana: new ManaGroup() { Any = 1 },
                UpgradedMana: new ManaGroup() { Any = 1 },
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
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(SuperGiantDef))]
        public sealed class SuperGiant : Card
        {
            public override IEnumerable<BattleAction> OnDraw()
            {
                return this.EnterHandReactor();
            }

            public override IEnumerable<BattleAction> OnMove(CardZone srcZone, CardZone dstZone)
            {
                if (dstZone != CardZone.Hand)
                {
                    return null;
                }
                return this.EnterHandReactor();
            }
            private IEnumerable<BattleAction> OnCardUse(CardUsingEventArgs args)
            {
                if (args.Card.BaseName == "Dark Matter")
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield break;
                    }

                    List<Card> cards = base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>();
                    int total = cards.FindAll((Card card) => card.BaseName == "Dark Matter").Count;
                    this.SetTurnCost(this.BaseCost);
                    this.DecreaseTurnCost(new ManaGroup() { Any = total });
                }
            }
            private IEnumerable<BattleAction> OnCardMove(CardMovingEventArgs args)
            {
                if (args.Card.BaseName == "Dark Matter")
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    if (base.Zone != CardZone.Hand)
                    {
                        yield break;
                    }
                    if ((args.SourceZone == CardZone.Hand) || args.DestinationZone == CardZone.Hand)
                    {
                        List<Card> cards = base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>();
                        int total = cards.FindAll((Card card) => card.BaseName == "Dark Matter").Count;
                        this.SetTurnCost(this.BaseCost);
                        this.DecreaseTurnCost(new ManaGroup() { Any = total });
                    }
                }
            }
            private IEnumerable<BattleAction> OnCardDraw(CardEventArgs args)
            {
                if (args.Card.BaseName == "Dark Matter")
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield break;
                    }
                    if (base.Zone != CardZone.Hand)
                    {
                        yield break;
                    }
                    List<Card> cards = base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>();
                    int total = cards.FindAll((Card card) => card.BaseName == "Dark Matter").Count;
                    this.SetTurnCost(this.BaseCost);
                    this.DecreaseTurnCost(new ManaGroup() { Any = total });
                }
            }
            protected override void OnEnterBattle(BattleController battle)
            {
                /*if (base.Zone == CardZone.Hand)
                {
                    base.React(new LazySequencedReactor(this.EnterHandReactor));
                }*/
                if (this.Zone == CardZone.Hand)
                {
                    List<Card> cards = base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>();
                    int total = cards.FindAll((Card card) => card.BaseName == "Dark Matter").Count;
                    this.DecreaseTurnCost(new ManaGroup() { Any = total });
                }

                base.ReactBattleEvent<CardMovingEventArgs>(battle.CardMoved, new EventSequencedReactor<CardMovingEventArgs>(this.OnCardMove));
                base.ReactBattleEvent<CardEventArgs>(battle.CardDrawn, new EventSequencedReactor<CardEventArgs>(this.OnCardDraw));
                base.ReactBattleEvent<CardUsingEventArgs>(battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUse));
            }

            private IEnumerable<BattleAction> EnterHandReactor()
            {
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                if (base.Zone != CardZone.Hand)
                {
                    yield break;
                }
                List<Card> cards = base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>();
                int total = cards.FindAll((Card card) => card.BaseName == "Dark Matter").Count;
                this.SetTurnCost(this.BaseCost);
                this.DecreaseTurnCost(new ManaGroup() { Any = total });
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return AttackAction(selector);
                yield break;
            }
        }
    }
}
