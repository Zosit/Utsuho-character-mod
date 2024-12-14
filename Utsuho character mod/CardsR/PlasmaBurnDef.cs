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
    public sealed class PlasmaBurnDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(PlasmaBurn);
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
                GunName: "火激光",
                GunNameBurst: "火激光",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Uncommon,
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Red = 1, Any = 1 },
                UpgradedCost: new ManaGroup() { Red = 1 },
                MoneyCost: null,
                Damage: 0,
                UpgradedDamage: 0,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 15,
                UpgradedValue1: 15,
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

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "PlasmaBurnStatus", "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "PlasmaBurnStatus", "HeatStatus" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(PlasmaBurnDef))]
        public sealed class PlasmaBurn : Card
        {
            private int tempDamage = 0;
            public override int AdditionalDamage
            {
                get
                {
                    if (tempDamage != 0)
                    {
                        return tempDamage;
                    }
                    else
                    {
                        return base.GetSeLevel<HeatStatus>() + Value1;

                    }
                }
            }

            protected override void OnEnterBattle(BattleController battle)
            {
                base.ReactBattleEvent<DamageEventArgs>(base.Battle.Player.DamageDealt, new EventSequencedReactor<DamageEventArgs>(this.OnPlayerDamageDealt));
            }

            private IEnumerable<BattleAction> OnPlayerDamageDealt(DamageEventArgs args)
            {
                if (base.Battle.BattleShouldEnd)
                {
                    yield break;
                }
                if (args.Cause == ActionCause.Card && args.ActionSource == this)
                {
                    DamageInfo damageInfo = args.DamageInfo;
                    if (damageInfo.Damage > 0f)
                    {
                        yield return new ApplyStatusEffectAction<PlasmaBurnStatus>(args.Target, ((int)damageInfo.Damage), null, null, null, 0f, true);
                    }
                }
                yield break;
            }

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                if (!base.Battle.BattleShouldEnd)
                {
                    tempDamage = base.GetSeLevel<HeatStatus>() + Value1;
                    int level = base.GetSeLevel<HeatStatus>();
                    yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, Value1, null, null, null, 0f, true);
                    yield return base.AttackAction(selector);
                    yield return new RemoveStatusEffectAction(Battle.Player.GetStatusEffect<HeatStatus>());
                    tempDamage = 0;
                    yield break;
                }
            }

        }

    }
}
