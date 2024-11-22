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
using LBoL.Presentation.UI.Panels;
using LBoL.Presentation.UI;
using LBoL.Core.Battle.Interactions;
using LBoL.EntityLib.Cards.Character.Marisa;

namespace Utsuho_character_mod.CardsMulti
{
    public sealed class CoolantDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Coolant);
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
                Index: 13610,
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
                Type: CardType.Ability,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Blue, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Blue = 1, Any = 2 },
                UpgradedCost: new ManaGroup() { Blue = 1, Any = 2 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 4,
                UpgradedValue1: 6,
                Value2: 4,
                UpgradedValue2: 6,
                Mana: new ManaGroup() { Red = 1, Any = 1 },
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

                RelativeEffects: new List<string>() { "Spirit", "Firepower" },
                UpgradedRelativeEffects: new List<string>() { "Spirit", "Firepower" },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(CoolantDef))]
        public sealed class Coolant : Card
        {
            ManaGroup temp;
            bool isKicked = false;
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
                    //check pool size and check kick colored mana in pool
                    if (pool.CanAfford(this.Mana + this.TurnCost))
                    {
                        //save the pool state
                        temp = pool;
                        isKicked = true;
                    }
                    else
                    {
                        isKicked = false;
                    }
                }
                return null;
            }
            private IEnumerable<BattleAction> OnManaConsumed(ManaEventArgs args)
            {
                if (isKicked)
                {
                    isKicked = false;
                    BattleManaPanel panel = UiManager.GetPanel<BattleManaPanel>();
                    ManaGroup pool = panel.PooledMana;
                    yield return new ConsumeManaAction(pool);
                    yield return BuffAction<Firepower>(base.Value2, 0, 0, 0, 0.2f);
                }
            }


            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                yield return BuffAction<Spirit>(base.Value1, 0, 0, 0, 0.2f);
                List<Card> list = base.Battle.HandZone.Where((Card card) => card.CardType != CardType.Defense).ToList<Card>();
                yield return new DiscardManyAction(list);
                yield break;
            }
        }
    }
}
