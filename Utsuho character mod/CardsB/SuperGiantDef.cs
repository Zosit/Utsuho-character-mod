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
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: 13270,
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
                Rarity: Rarity.Uncommon,
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Any = 6 },
                UpgradedCost: new ManaGroup() { Black = 1, Any = 6 },
                MoneyCost: null,
                Damage: 45,
                UpgradedDamage: 60,
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
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Zosit",
                SubIllustrator: new List<string>() { }
             )
            {

            };
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
                if ((args.Card is UtsuhoCard uCard) && (uCard.isMass))
                {
                    if (base.Battle.BattleShouldEnd)
                    {
                        yield break;
                    }

                    List<Card> cards = base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>();
                    int total = cards.FindAll((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass)).Count;
                    this.SetTurnCost(this.BaseCost);
                    this.DecreaseTurnCost(new ManaGroup() { Any = total });
                }
            }
            private IEnumerable<BattleAction> OnCardMove(CardMovingEventArgs args)
            {
                if ((args.Card is UtsuhoCard uCard) && (uCard.isMass))
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
                        int total = cards.FindAll((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass)).Count;
                        this.SetTurnCost(this.BaseCost);
                        this.DecreaseTurnCost(new ManaGroup() { Any = total });
                    }
                }
            }
            private IEnumerable<BattleAction> OnCardDraw(CardEventArgs args)
            {
                if ((args.Card is UtsuhoCard uCard) && (uCard.isMass))
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
                    int total = cards.FindAll((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass)).Count;
                    this.SetTurnCost(this.BaseCost);
                    this.DecreaseTurnCost(new ManaGroup() { Any = total });
                }
            }
            private IEnumerable<BattleAction> OnCardAdded(CardsEventArgs args)
            {
                foreach (Card card in args.Cards)
                {
                    if ((card is UtsuhoCard uCard) && (uCard.isMass))
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
                        int total = cards.FindAll((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass)).Count;
                        this.SetTurnCost(this.BaseCost);
                        this.DecreaseTurnCost(new ManaGroup() { Any = total });
                    }
                }
            }
            private IEnumerable<BattleAction> OnCardExiled(CardEventArgs args)
            {
                if ((args.Card is UtsuhoCard uCard) && (uCard.isMass))
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
                    int total = cards.FindAll((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass)).Count;
                    this.SetTurnCost(this.BaseCost);
                    this.DecreaseTurnCost(new ManaGroup() { Any = total });
                }
            }
            protected override void OnEnterBattle(BattleController battle)
            {
                if (this.Zone == CardZone.Hand)
                {
                    List<Card> cards = base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>();
                    int total = cards.FindAll((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass)).Count;
                    this.DecreaseTurnCost(new ManaGroup() { Any = total });
                }

                base.ReactBattleEvent<CardMovingEventArgs>(battle.CardMoved, new EventSequencedReactor<CardMovingEventArgs>(this.OnCardMove));
                base.ReactBattleEvent<CardEventArgs>(battle.CardDrawn, new EventSequencedReactor<CardEventArgs>(this.OnCardDraw));
                base.ReactBattleEvent<CardUsingEventArgs>(battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUse));
                base.ReactBattleEvent<CardsEventArgs>(battle.CardsAddedToHand, new EventSequencedReactor<CardsEventArgs>(this.OnCardAdded));
                base.ReactBattleEvent<CardEventArgs>(base.Battle.CardExiled, new EventSequencedReactor<CardEventArgs>(this.OnCardExiled));
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
                int total = cards.FindAll((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass)).Count;
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
