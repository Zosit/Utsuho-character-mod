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
using LBoL.Core.Battle.Interactions;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsMulti
{
    public sealed class RememberDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Remember);
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
                Index: 13630,
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
                Type: CardType.Ability,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Blue },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Blue = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Black = 1, Blue = 1, Any = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 4,
                UpgradedValue1: 6,
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
                ActiveCost2: null,
                UpgradedActiveCost: null,
                UpgradedActiveCost2: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.Ethereal,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.Exile,
                UpgradedRelativeKeyword: Keyword.Exile,

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

        [EntityLogic(typeof(RememberDef))]
        public sealed class Remember : Card
        {
            public override Interaction Precondition()
            {
                if (base.Battle.ExileZone.Count <= 0)
                {
                    return null;
                }
                return new SelectCardInteraction(1, 12, base.Battle.ExileZone, SelectedCardHandling.DoNothing);
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                SelectCardInteraction selectCardInteraction = (SelectCardInteraction)precondition;
                foreach(Card card in selectCardInteraction.SelectedCards)
                {
                    if (card != null)
                    {
                        yield return new MoveCardAction(card, CardZone.Hand);
                        //card.SetTurnCost(base.Mana);
                    }
                }

                yield break;
            }
        }
    }
}
