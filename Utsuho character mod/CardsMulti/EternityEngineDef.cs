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
using System.Collections;
using UnityEngine;
using LBoL.Presentation;
using Utsuho_character_mod.Util;
using static UnityEngine.UI.GridLayoutGroup;
using LBoL.Core.Randoms;

namespace Utsuho_character_mod.CardsMulti
{
    public sealed class EternityEngineDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(EternityEngine);
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
                Index: 13380,
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
                Type: CardType.Skill,
                TargetType: TargetType.Self,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Any = 0 },
                UpgradedCost: new ManaGroup() { Any = 0 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 1,
                UpgradedValue1: 2,
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

                Keywords: Keyword.Retain,
                UpgradedKeywords: Keyword.Retain,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "Firepower" },
                UpgradedRelativeEffects: new List<string>() { "Firepower" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(EternityEngineDef))]
        public sealed class EternityEngine : Card
        {
            IEnumerator ResetTrigger()
            {
                yield return new WaitForSecondsRealtime(1.0f);
                NotifyChanged();
            }
            public override IEnumerable<BattleAction> OnTurnStartedInHand()
            {
                if (base.Zone == CardZone.Hand)
                {
                    Card card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
                    foreach(BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) {  yield return action; }
                    card.NotifyActivating();
                    GameMaster.Instance.StartCoroutine(ResetTrigger());
                    yield return new ExileCardAction(card);
                    yield return BuffAction<Firepower>(Value1, 0, 0, 0, 0.2f);
                }
                yield break;
            }
        }
    }
}
