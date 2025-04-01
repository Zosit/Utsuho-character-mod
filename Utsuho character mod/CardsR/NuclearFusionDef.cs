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
using static Utsuho_character_mod.CardsR.NuclearStrikeDefinition;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsR
{
    public sealed class NuclearFusionDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(NuclearFusion);
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
                Index: 13480,
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
                Type: CardType.Skill,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Red = 1, Any = 1 },
                UpgradedCost: new ManaGroup() { Red = 1, Any = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 10,
                UpgradedValue1: 15,
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

                Keywords: Keyword.Exile,
                UpgradedKeywords: Keyword.Exile,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus" },
                RelativeCards: new List<string>() { "NuclearStrike" },
                UpgradedRelativeCards: new List<string>() { "NuclearStrike" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Zosit",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(NuclearFusionDefinition))]
        public sealed class NuclearFusion : Card
        {
            public override Interaction Precondition()
            {
                List<Card> list = (base.Battle.HandZone.Where((Card card) => card != this).ToList<Card>());
                if (!list.Empty<Card>())
                {
                    return new SelectCardInteraction(1, 1, list, SelectedCardHandling.DoNothing);
                }
                return null;
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                if (precondition != null)
                {
                    IReadOnlyList<Card> selectedCards = ((SelectCardInteraction)precondition).SelectedCards;
                    if (selectedCards.Count > 0)
                    {
                        int cardValue = selectedCards[0].TurnCost.Total;
                        yield return new ExileManyCardAction(selectedCards);
                        NuclearStrike nuclearStrike = Library.CreateCard<NuclearStrike>(false);
                        nuclearStrike.DeltaValue1 = cardValue * Value1;
                        yield return new AddCardsToHandAction(nuclearStrike);
                        yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, cardValue * (new int?(base.Value1)), null, null, null, 0f, true);
                    }
                }
                yield break;
            }
        }

    }
}
