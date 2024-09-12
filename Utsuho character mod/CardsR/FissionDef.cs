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
using LBoL.Core.Randoms;
using LBoL.Core.Battle.Interactions;
using System.Linq;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsR
{
    public sealed class FissionDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Fission);
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
                Index: 13310,
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
                Cost: new ManaGroup() { Red = 2 },
                UpgradedCost: new ManaGroup() { Red = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 8,
                UpgradedValue1: 8,
                Value2: null,
                UpgradedValue2: null,
                Mana: new ManaGroup() { Red = 2 },
                UpgradedMana: new ManaGroup() { Philosophy = 2 },
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
                Illustrator: "Zosit",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(FissionDefinition))]
        public sealed class Fission : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                Card Attack = base.Battle.RollCard(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.Valid, CardTypeWeightTable.OnlyAttack), delegate (CardConfig config)
                {
                    if (config.Colors.Count > 0)
                    {
                        return config.Colors.All((ManaColor color) => color == ManaColor.Red);
                    }
                    return false;
                });
                Card Defense = base.Battle.RollCard(new CardWeightTable(RarityWeightTable.BattleCard, OwnerWeightTable.Valid, CardTypeWeightTable.OnlyDefense), delegate (CardConfig config)
                {
                    if (config.Colors.Count > 0)
                    {
                        return config.Colors.All((ManaColor color) => color == ManaColor.Red);
                    }
                    return false;
                });
                if (this.IsUpgraded)
                {
                    Attack.Upgrade();
                    Defense.Upgrade();
                }

                if ((Attack != null) && (Defense != null))
                {

                    Attack.SetBaseCost(ManaGroup.Anys(Attack.ConfigCost.Amount));
                    Defense.SetBaseCost(ManaGroup.Anys(Defense.ConfigCost.Amount));
                    yield return new AddCardsToHandAction(new Card[] { Attack });
                    yield return new AddCardsToHandAction(new Card[] { Defense });
                }
                yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, new int?(base.Value1), null, null, null, 0f, true);
                yield return new GainManaAction(Mana);
                yield break;
            }

        }

    }
}
