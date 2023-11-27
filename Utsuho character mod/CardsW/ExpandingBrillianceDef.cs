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
using LBoL.Core.Battle.Interactions;
using System.Linq;

namespace Utsuho_character_mod
{
    public sealed class ExpandingBrillianceDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(ExpandingBrilliance);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(BepinexPlugin.embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(BepinexPlugin.embeddedSource);
            loc.LocalizationFiles.AddLocaleFile(LBoL.Core.Locale.En, "CardsEn.yaml");
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
                TargetType: TargetType.AllEnemies,
                Colors: new List<ManaColor>() { ManaColor.White },
                IsXCost: false,
                Cost: new ManaGroup() { White = 1 },
                UpgradedCost: new ManaGroup() { Any = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 3,
                UpgradedValue1: 4,
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

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() {  },
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

        [EntityLogic(typeof(ExpandingBrillianceDefinition))]
        public sealed class ExpandingBrilliance : Card        {
            public override Interaction Precondition()
            {
                List<Card> list = base.Battle.HandZone.Where((Card hand) => hand != this && hand.CanUpgradeAndPositive).ToList<Card>();
                if (list.Count == 1)
                {
                    this.oneTargetHand = list[0];
                }
                if (list.Count <= 1)
                {
                    return null;
                }
                return new SelectHandInteraction(1, 1, list);
            }

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                foreach (BattleAction battleAction in base.DebuffAction<TempFirepowerNegative>(selector.GetUnits(base.Battle), base.Value1, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }


                if (precondition != null)
                {
                    Card card = ((SelectHandInteraction)precondition).SelectedCards[0];
                    if (card != null)
                    {
                        yield return new UpgradeCardAction(card);
                    }
                }
                else if (this.oneTargetHand != null)
                {
                    yield return new UpgradeCardAction(this.oneTargetHand);
                    this.oneTargetHand = null;
                }
                yield return new DrawCardAction();
                yield break;
            }
            private Card oneTargetHand;
        }
    }
}
