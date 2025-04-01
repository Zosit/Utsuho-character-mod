﻿using LBoL.Base;
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
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsMulti
{
    public sealed class SunCrowDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(SunCrow);
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
                Index: 13550,
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
                Type: CardType.Ability,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 2, Red = 2, Any = 1 },
                UpgradedCost: new ManaGroup() { Black = 2, Red = 2, Any = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 7,
                UpgradedValue1: 10,
                Value2: 20,
                UpgradedValue2: 20,
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

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.Battlefield,
                UpgradedRelativeKeyword: Keyword.Battlefield,

                RelativeEffects: new List<string>() { "HeatStatus", "MassStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus", "MassStatus" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Flippin'Loser",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(SunCrowDef))]
        public sealed class SunCrow : Card
        {
            /*public int RemoveCount
            {
                get
                {
                    if (base.Battle != null)
                    {
                        List<Card> cards = base.Battle.EnumerateAllCards().Where((Card card) => card != this).ToList<Card>();
                        if (cards.Count < Value2)
                        {
                            return cards.Count * Value1;
                        }
                        else
                        {
                            return Value1 * Value2;
                        }

                    }
                    else
                    {
                        return 0;
                    }
                }
            }*/
            public int doubleValue
            {
                get
                {
                    return Value1 * 2;
                }
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                int count = 0;
                for (int i = 0; i < Value2; i++)
                {
                    //List<Card> cards = base.Battle.EnumerateAllCards().Where((Card card) => card != this).ToList<Card>();
                    List<Card> cards = base.Battle.EnumerateAllCards().Where((Card card) => card != this).ToList<Card>();
                    if (cards.Count != 0)
                    {
                        Card card = Util.UsefulFunctions.RandomUtsuho(cards);
                        foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }

                        yield return new RemoveCardAction(card);
                        if (card is UtsuhoCard)
                        {
                            count++;
                        }
                        count++;
                    }
                }
                yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, new int?(base.Value1) * count, null, null, null, 0f, true);


                yield break;
            }
        }
    }
}
