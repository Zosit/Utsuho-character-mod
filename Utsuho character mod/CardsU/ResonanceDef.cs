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
using Utsuho_character_mod.Util;
using LBoL.Core.Units;
using LBoL.Presentation.UI.Panels;
using LBoL.Presentation.UI;
using static Utsuho_character_mod.CardsU.BlueshiftDef;

namespace Utsuho_character_mod.CardsU
{
    public sealed class ResonanceDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Resonance);
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
                Index: 13620,
                Pack: null,
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
                Colors: new List<ManaColor>() { ManaColor.Blue },
                IsXCost: false,
                Cost: new ManaGroup() { Blue = 1, Any = 1 },
                UpgradedCost: new ManaGroup() { Blue = 1 },
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
                Mana: new ManaGroup() { Any = 1 },
                UpgradedMana: new ManaGroup() { Any = 1 },
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

                Keywords: Keyword.Exile | Keyword.Ethereal,
                UpgradedKeywords: Keyword.Exile,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "ResonanceStatus", "MultiKickerStatus" },
                UpgradedRelativeEffects: new List<string>() { "ResonanceStatus", "MultiKickerStatus" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Flippin'Loser",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(ResonanceDef))]
        public sealed class Resonance : Card
        {
            ManaGroup temp;
            int KickCount = 0;
            protected override void OnEnterBattle(BattleController battle)
            {
                base.ReactBattleEvent<ManaEventArgs>(base.Battle.ManaConsuming, new EventSequencedReactor<ManaEventArgs>(this.OnManaConsuming));
                base.ReactBattleEvent<ManaEventArgs>(base.Battle.ManaConsumed, new EventSequencedReactor<ManaEventArgs>(this.OnManaConsumed));
            }
            private IEnumerable<BattleAction> OnManaConsuming(ManaEventArgs args)
            {
                BattleManaPanel panel = UiManager.GetPanel<BattleManaPanel>();
                ManaGroup pool = panel.PooledMana + args.Value;
                if ((args.ActionSource == this) && (args.Cause == ActionCause.CardUse))
                {
                    if (pool.CanAfford(this.Mana))
                    {
                        temp = pool;
                        KickCount = panel.PooledMana.Amount;
                    }
                }
                return null;
            }
            private IEnumerable<BattleAction> OnManaConsumed(ManaEventArgs args)
            {
                if (KickCount > 0)
                {
                    yield return base.BuffAction<ResonanceStatus>(KickCount, 0, 0, 0, 0.2f);


                    KickCount = 0;
                    BattleManaPanel panel = UiManager.GetPanel<BattleManaPanel>();
                    ManaGroup pool = panel.PooledMana;
                    yield return new ConsumeManaAction(pool);
                }
            }

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield break;
            }
        }
    }
}
