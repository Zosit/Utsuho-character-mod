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
using System.Linq;
using LBoL.Base.Extensions;
using LBoL.Core.Battle.Interactions;
using Utsuho_character_mod.Status;
using Utsuho_character_mod.Util;
using static Utsuho_character_mod.CardsB.DarkMatterDef;

namespace Utsuho_character_mod.CardsMulti
{
    public sealed class CrossfeedDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Crossfeed);
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
                Index: 13390,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "火激光",
                GunNameBurst: "火激光",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Uncommon,
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Red = 1 },
                UpgradedCost: new ManaGroup() { Any = 1 },
                MoneyCost: null,
                Damage: 0,
                UpgradedDamage: 0,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 12,
                UpgradedValue1: 14,
                Value2: 10,
                UpgradedValue2: 8,
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
                ActiveCost2: null,
                UpgradedActiveCost: null,
                UpgradedActiveCost2: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus" },
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "AltAlias",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(CrossfeedDefinition))]
        public sealed class Crossfeed : Card
        {
            public override int AdditionalDamage
            {
                get
                {
                    int level = base.GetSeLevel<HeatStatus>();
                    return level;
                }
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                int level = base.GetSeLevel<HeatStatus>();
                int total = 0;
                Card[] array = base.Battle.HandZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                if (array.Length != 0)
                {
                    foreach (Card card in array)
                    {
                        if ((card is UtsuhoCard uCard) && (uCard.isMass))
                        {
                            total++;
                        }
                    }
                }
                yield return AttackAction(selector);
                if (total == 0 && level != 0)
                {
                    yield return new RemoveStatusEffectAction(Battle.Player.GetStatusEffect<HeatStatus>());
                }
                else if (total != 0)
                {
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, (total * this.Value1) - level, null, null, null, 0f, true);
                }
                if ((level/Value2) != 0)
                {
                    yield return new AddCardsToHandAction(Library.CreateCards<DarkMatter>(level/Value2));
                }

                yield break;
            }
        }
    }
}
