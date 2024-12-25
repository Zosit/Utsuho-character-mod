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
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsW
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
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: 13560,
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
                UpgradedValue1: 5,
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
                ActiveCost2: null,
                UpgradedActiveCost: null,
                UpgradedActiveCost2: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.Upgrade,
                UpgradedRelativeKeyword: Keyword.Upgrade,

                RelativeEffects: new List<string>() { "TempFirepowerNegative" },
                UpgradedRelativeEffects: new List<string>() { "TempFirepowerNegative" },
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

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return new DrawCardAction();
                foreach (BattleAction battleAction in base.DebuffAction<TempFirepowerNegative>(selector.GetUnits(base.Battle), base.Value1, 0, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }

                List<Card> list = base.Battle.HandZone.ToList<Card>();
                if (list.Count > 0)
                {
                    SelectHandInteraction interaction = new SelectHandInteraction(1, 1, base.Battle.HandZone.Where((Card hand) => hand != this && hand.CanUpgradeAndPositive).ToList<Card>())
                    {
                        Source = this
                    };
                    yield return new InteractionAction(interaction, false);
                    Card card = interaction.SelectedCards.FirstOrDefault<Card>();
                    if (card != null)
                    {
                        yield return new UpgradeCardAction(card);
                    }
                    interaction = null;
                }
                yield break;
            }
        }
    }
}
