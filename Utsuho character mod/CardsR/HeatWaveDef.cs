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

namespace Utsuho_character_mod.CardsR
{
    public sealed class HeatWaveDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(HeatWave);
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
                Index: 13165,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "GuihuoExplodeR2",
                GunNameBurst: "GuihuoExplodeR2",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Common,
                Type: CardType.Skill,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Red = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Red = 1, Any = 2 },
                MoneyCost: null,
                Damage: 0,
                UpgradedDamage: 0,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 30,
                UpgradedValue1: 40,
                Value2: 2,
                UpgradedValue2: 2,
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
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Camellia",
                SubIllustrator: new List<string>() { "Kimmchu" }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(HeatWaveDef))]
        public sealed class HeatWave : Card
        {
            public int HeatDamage
            {
                get
                {
                    int level = base.GetSeLevel<HeatStatus>();

                    return (level + Value1) / 5;
                }
            }
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, new int?(base.Value1), null, null, null, 0f, true);
                int level = base.GetSeLevel<HeatStatus>();
                yield return new DamageAction(base.Battle.Player, base.Battle.EnemyGroup.Alives, DamageInfo.Reaction((float)(level / 5)), this.GunName, GunType.Single);
                yield return new DamageAction(base.Battle.Player, base.Battle.EnemyGroup.Alives, DamageInfo.Reaction((float)(level / 5)), this.GunName, GunType.Single);
                //yield return new ApplyStatusEffectAction<HeatWaveStatus>(Battle.Player, null, new int?(base.Value2), null, null, 0f, true);
                yield break;
            }

        }

    }
}
