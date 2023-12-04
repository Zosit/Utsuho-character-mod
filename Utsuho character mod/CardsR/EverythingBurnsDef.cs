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
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: true,
                Cost: new ManaGroup() { Red = 2 },
                UpgradedCost: null,
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 7,
                UpgradedValue1: 10,
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
                if (!list.Empty<Card>())
                {
                    int number = base.SynergyAmount(consumingMana, ManaColor.Any, 1);
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
                        yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, base.SynergyAmount(consumingMana, ManaColor.Any, 1) * (new int?(base.Value1)), null, null, null, 0f, true);
                    }
                }
                yield break;
            }
        }
    }
}
