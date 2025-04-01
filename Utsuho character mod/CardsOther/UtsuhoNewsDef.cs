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
using Utsuho_character_mod.Util;
using HarmonyLib;

namespace Utsuho_character_mod.CardsOther
{
    public sealed class UtsuhoNewsDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(UtsuhoNews);
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
                Index: 13527,
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
                IsPooled: false,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: false,
                Rarity: Rarity.Rare,
                Type: CardType.Status,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { },
                IsXCost: false,
                Cost: new ManaGroup() { Any = 5 },
                UpgradedCost: null,
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 2,
                UpgradedValue1: null,
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

                RelativeEffects: new List<string>() { "Spirit" },
                UpgradedRelativeEffects: new List<string>() {  },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "",
                Unfinished: false,
                Illustrator: "Camellia",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(UtsuhoNewsDef))]
        public sealed class UtsuhoNews : Card
        {
            protected override void OnEnterBattle(BattleController battle)
            {
                base.ReactBattleEvent<UnitEventArgs>(battle.Player.TurnEnding, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnEnding));
            }

            private IEnumerable<BattleAction> OnPlayerTurnEnding(UnitEventArgs args)
            {
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                if (base.Zone == CardZone.Hand)
                {
                    base.NotifyActivating();
                    yield return base.DebuffAction<SpiritNegative>(base.Battle.Player, base.Value1, 0, 0, 0, true, 0.2f);
                }
                yield break;
            }

        }

    }
}
