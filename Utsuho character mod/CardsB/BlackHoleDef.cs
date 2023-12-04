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
                Type: CardType.Skill,
                TargetType: TargetType.Self,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Any = 0 },
                UpgradedCost: new ManaGroup() { Any = 0 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: 0,
                UpgradedBlock: 0,
                Shield: null,
                UpgradedShield: null,
                Value1: 8,
                UpgradedValue1: 12,
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

        [EntityLogic(typeof(BlackHoleDef))]
        public sealed class BlackHole : Card
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
                    //Card card = Battle.HandZone.Sample(base.GameRun.BattleRng);
                    card.NotifyActivating();
                    GameMaster.Instance.StartCoroutine(ResetTrigger());
                    yield return new DiscardAction(card);
                    yield return DefenseAction(Value1, 0);
                }
                yield break;
            }
        }
    }
}
