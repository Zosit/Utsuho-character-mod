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
using LBoL.Core.Battle.Interactions;
using System.Linq;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsW
{
    public sealed class DisintegrateDefinition : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(Disintegrate);
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
                Index: 13570,
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
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.White },
                IsXCost: false,
                Cost: new ManaGroup() { White = 1, Any = 2 },
                UpgradedCost: null,
                MoneyCost: null,
                Damage: 18,
                UpgradedDamage: 24,
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
                UpgradedActiveCost: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.Exile,
                UpgradedKeywords: Keyword.Exile,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() {  },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;            
        }

        [EntityLogic(typeof(DisintegrateDefinition))]
        public sealed class Disintegrate : Card
        {
            protected override void OnEnterBattle(BattleController battle)
            {
                base.ReactBattleEvent<DieEventArgs>(base.Battle.EnemyDied, new EventSequencedReactor<DieEventArgs>(this.OnEnemyDied));
            }

            private IEnumerable<BattleAction> OnEnemyDied(DieEventArgs args)
            {
                if (args.DieSource == this && !args.Unit.HasStatusEffect<Servant>())
                {
                    List<Card> list = this.GameRun.BaseDeckWithOutUnremovable.ToList<Card>();
                    if (list.Count > 0)
                    {
                        SelectCardInteraction interaction = new SelectCardInteraction(1, 1, list)
                        {
                            CanCancel = false,
                            Description = "Select a card to remove."
                        };
                        yield return new InteractionAction(interaction, false);
                        if (!interaction.IsCanceled)
                        {
                            this.GameRun.RemoveDeckCards(new Card[] { interaction.SelectedCards[0] }, true);
                        }
                        interaction = null;
                    }
                }
                yield break;
            }

        }

    }
}
