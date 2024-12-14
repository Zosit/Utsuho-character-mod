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
using System.Linq;
using System.Collections.Generic;
using System.Text;
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Status;
using static Utsuho_character_mod.CardsB.DarkMatterDef;
using LBoL.Base.Extensions;
using Utsuho_character_mod.Util;
using LBoL.Core.Battle.Interactions;
using UnityEngine;

namespace Utsuho_character_mod.CardsR
{
    public sealed class EscapeVelocityDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(EscapeVelocity);
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
                Index: 13220,
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
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Any = 1 },
                UpgradedCost: new ManaGroup() { Any = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 4,
                UpgradedValue1: 4,
                Value2: 3,
                UpgradedValue2: 3,
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

        [EntityLogic(typeof(EscapeVelocityDef))]
        public sealed class EscapeVelocity : Card
        {
            public override Interaction Precondition()
            {
                List<EscapeVelocity> list = Library.CreateCards<EscapeVelocity>(2, true).ToList<EscapeVelocity>();
                EscapeVelocity first = list[0];
                EscapeVelocity escapeConsider = list[1];
                first.ChoiceCardIndicator = 1;
                escapeConsider.ChoiceCardIndicator = 2;
                first.SetBattle(base.Battle);
                escapeConsider.SetBattle(base.Battle);
                MiniSelectCardInteraction interaction = new MiniSelectCardInteraction(list, false, false, false)
                {
                    Source = this
                };
                return interaction;
            }

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                MiniSelectCardInteraction interaction = (MiniSelectCardInteraction)precondition;
                if (interaction.SelectedCard.ChoiceCardIndicator == 1)
                {
                    for (int i = 0; i < Value2; i++)
                    {
                        if (Battle.HandZone.Count != 0)
                        {
                            Card card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
                            foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                            yield return new DiscardAction(card);
                        }
                    }
                    for (int i = 0; i < Value1; i++)
                    {
                        yield return new DrawCardAction();
                    }
                }
                else
                {
                    for (int i = 0; i < Value1; i++)
                    {
                        yield return new DrawCardAction();
                    }
                    yield return new WaitForYieldInstructionAction(new WaitForSeconds(0.5f));
                    for (int i = 0; i < Value2; i++)
                    {
                        if (Battle.HandZone.Count != 0)
                        {
                            Card card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
                            foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                            yield return new DiscardAction(card);
                        }
                    }
                }
            }
        }
    }
}
