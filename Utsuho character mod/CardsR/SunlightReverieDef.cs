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
using LBoL.Base.Extensions;
using LBoL.Core.Battle.Interactions;
using System.Linq;
using LBoL.Core.Units;
using LBoL.EntityLib.StatusEffects.Neutral;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsR
{
    public sealed class SunlightReverieDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(SunlightReverie);
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
                Index: 13290,
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
                Value1: 2,
                UpgradedValue1: 2,
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

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.Exile,
                UpgradedRelativeKeyword: Keyword.Exile,

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { "RManaCard" },
                UpgradedRelativeCards: new List<string>() { "RManaCard" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Zosit",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(SunlightReverieDef))]
        public sealed class SunlightReverie : Card
        {
            public override Interaction Precondition()
            {
                if (this.IsUpgraded)
                {
                    List<SunlightReverie> list = Library.CreateCards<SunlightReverie>(2, true).ToList<SunlightReverie>();
                    SunlightReverie first = list[0];
                    SunlightReverie discardConsider = list[1];
                    first.ChoiceCardIndicator = 1;
                    discardConsider.ChoiceCardIndicator = 2;
                    first.SetBattle(base.Battle);
                    discardConsider.SetBattle(base.Battle);
                    MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(list, false, false, false)
                    {
                        Source = this
                    };
                    return interaction;
                }
                else
                    return null;
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return new AddCardsToHandAction(Library.CreateCards<RManaCard>(Value1, false));
                if (!this.IsUpgraded)
                {
                    if (Battle.DrawZone.NotEmpty())
                    {
                        Card card = UsefulFunctions.RandomUtsuho(Battle.DrawZone);
                        foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                        if (card != null)
                            yield return new ExileCardAction(card);
                    }
                }
                else
                {
                    MiniSelectCardInteraction interaction = (MiniSelectCardInteraction)precondition;
                    if (interaction.SelectedCard.ChoiceCardIndicator == 1)
                    {
                        if (Battle.DrawZone.NotEmpty())
                        {
                            Card card = UsefulFunctions.RandomUtsuho(Battle.DrawZone);
                            if (card != null)
                                yield return new ExileCardAction(card);
                            foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                        }
                    }
                    else
                    {
                        if (Battle.DiscardZone.NotEmpty())
                        {
                            Card card = UsefulFunctions.RandomUtsuho(Battle.DiscardZone);
                            if (card != null)
                                yield return new ExileCardAction(card);
                            foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                        }
                    }
                }


                yield break;
            }           
        }
    }
}
