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

namespace Utsuho_character_mod
{
    public sealed class BlastBurnDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(BlastBurn);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(BepinexPlugin.embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(BepinexPlugin.embeddedSource);
            loc.LocalizationFiles.AddLocaleFile(LBoL.Core.Locale.En, "CardsEn.yaml");
            return loc;
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
                Rarity: Rarity.Common,
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
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
                Value1: 2,
                UpgradedValue1: 2,
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
                UpgradedActiveCost: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus" },
                //RelativeCards: new List<string>() { "AyaNews" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(BlastBurnDef))]
        public sealed class BlastBurn : Card
        {
            public override int AdditionalDamage
            {
                get
                {
                    HeatStatus statusEffect = base.Battle.Player.GetStatusEffect<HeatStatus>();
                    if (statusEffect != null)
                    {
                        return statusEffect.Level;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {

                if (!base.Battle.BattleShouldEnd)
                {
                    HeatStatus statusEffect = base.Battle.Player.GetStatusEffect<HeatStatus>();
                    if (statusEffect != null)
                    {
                        bool canDebuff = false;
                        if (selector.SelectedEnemy.IsAlive && (statusEffect.Level >= 10))
                        {
                            canDebuff = true;
                        }
                        if (canDebuff && this.IsUpgraded)
                        {
                            yield return base.DebuffAction<Weak>(selector.SelectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
                            yield return base.DebuffAction<Vulnerable>(selector.SelectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
                        }
                        yield return base.AttackAction(selector.SelectedEnemy);
                        yield return base.BuffAction<HeatStatus>(-(statusEffect.Level) + base.Value1, 0, 0, 0, 0.2f);
                        if (canDebuff && !this.IsUpgraded)
                        {
                            yield return base.DebuffAction<Weak>(selector.SelectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
                            yield return base.DebuffAction<Vulnerable>(selector.SelectedEnemy, 0, base.Value1, 0, 0, true, 0.2f);
                        }
                    }
                    else
                    {
                        yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, new int?(base.Value1), null, null, null, 0f, true);
                    }
                    yield break;
                }
            }

        }

    }
}
