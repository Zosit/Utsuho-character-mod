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

namespace Utsuho_character_mod.CardsR
{
    public sealed class EverythingBurnsDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(EverythingBurns);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(BepinexPlugin.embeddedSource);
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
                Index: 13370,
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
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: true,
                Cost: new ManaGroup() { Red = 2 },
                UpgradedCost: null,
                MoneyCost: null,
                Damage: 6,
                UpgradedDamage: 9,
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

                Keywords: Keyword.Exile,
                UpgradedKeywords: Keyword.Exile,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(EverythingBurnsDefinition))]
        public sealed class EverythingBurns : Card
        {
            public override ManaGroup GetXCostFromPooled(ManaGroup pooledMana)
            {
                return pooledMana;
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                IReadOnlyList<Card> selectedCards = null;
                List<Card> list = (base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>());
                int number = base.SynergyAmount(consumingMana, ManaColor.Any, 1);
                if (!list.Empty<Card>())
                {
                    SelectHandInteraction interaction = new SelectHandInteraction(0, number, list)
                    {
                        Source = this
                    };
                    yield return new InteractionAction(interaction, false);
                    selectedCards = interaction.SelectedCards.ToList();
                }
                if (selectedCards != null)
                {
                    if (selectedCards.Count > 0)
                    {
                        yield return new ExileManyCardAction(selectedCards);
                    }
                }
                for(int i = 0; i < number; i++)
                {
                    if (selector.SelectedEnemy != null && selector.SelectedEnemy.IsAlive)
                    {
                        yield return base.AttackAction(selector.SelectedEnemy);
                    }
                }

                yield break;
            }
        }
    }
}
