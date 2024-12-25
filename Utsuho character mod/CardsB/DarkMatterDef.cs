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
using LBoLEntitySideloader.CustomKeywords;

namespace Utsuho_character_mod.CardsB
{
    public sealed class DarkMatterDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(DarkMatter);
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
                Index: 13060,
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
                FindInBattle: false,
                HideMesuem: false,
                IsUpgradable: false,
                Rarity: Rarity.Common,
                Type: CardType.Status,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Any = 1 },
                UpgradedCost: null,
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: null,
                UpgradedValue1: null,
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

                Keywords: Keyword.Replenish | Keyword.Exile,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.TempRetain,
                UpgradedRelativeKeyword: Keyword.TempRetain,

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Flippin'Loser",
                SubIllustrator: new List<string>() { "AltAlias" }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(DarkMatterDef))]
        public sealed class DarkMatter : UtsuhoCard
        {
            public override void Initialize()
            {
                isMass = true;
                base.Initialize();
            }
            public override bool Triggered
            {
                get
                {
                    return this.IsTempRetain;
                }
            }
            public override IEnumerable<BattleAction> OnDraw()
            {
                return this.EnterHandReactor();
            }
            public override IEnumerable<BattleAction> OnMove(CardZone srcZone, CardZone dstZone)
            {
                if (dstZone != CardZone.Hand)
                {
                    return null;
                }
                return this.EnterHandReactor();
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                this.IsTempRetain = false;
                yield break;
            }
            private IEnumerable<BattleAction> EnterHandReactor()
            {
                this.IsTempRetain = true;
                yield break;
            }
            protected override void OnEnterBattle(BattleController battle)
            {
                if (this.Zone == CardZone.Hand)
                {
                    this.IsTempRetain = true;
                }
            }
        }

    }
}
