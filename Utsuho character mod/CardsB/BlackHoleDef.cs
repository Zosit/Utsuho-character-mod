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
using System.Collections;
using UnityEngine;
using LBoL.Presentation;
using Utsuho_character_mod.Util;
using static UnityEngine.UI.GridLayoutGroup;
using LBoL.Core.Randoms;

namespace Utsuho_character_mod.CardsR
{
    public sealed class BlackHoleDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(BlackHole);
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
                Index: 13180,
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
                Type: CardType.Skill,
                TargetType: TargetType.Self,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Any = 0 },
                UpgradedCost: new ManaGroup() { Any = 0 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: 8,
                UpgradedBlock: 12,
                Shield: null,
                UpgradedShield: null,
                Value1: null,
                UpgradedValue1: null,
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

                Keywords: Keyword.Retain | Keyword.Exile,
                UpgradedKeywords: Keyword.Retain | Keyword.Exile,
                EmptyDescription: false,
                RelativeKeyword: Keyword.Block,
                UpgradedRelativeKeyword: Keyword.Block,

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "AltAlias",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(BlackHoleDef))]
        public sealed class BlackHole : Card
        {
            public override IEnumerable<BattleAction> OnTurnStartedInHand()
            {
                if (base.Zone == CardZone.Hand)
                {
                    Card card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
                    yield return new WaitForYieldInstructionAction(new WaitForSeconds(0.5f));
                    yield return new DiscardAction(card);
                    foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                    card.NotifyActivating();
                    yield return DefenseAction();
                }
                yield break;
            }
        }
    }
}
