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
using Utsuho_character_mod.Util;
using System.Linq;
using LBoL.Base.Extensions;
using LBoL.Core.Units;

namespace Utsuho_character_mod.CardsB
{
    public sealed class SubterraneanSunDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(SubterraneanSun);
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
                Index: 13470,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "元鬼玉",
                GunNameBurst: "元鬼玉",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Skill,
                TargetType: TargetType.RandomEnemy,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { },
                UpgradedCost: new ManaGroup() { },
                MoneyCost: null,
                Damage: 10,
                UpgradedDamage: 14,
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

                Keywords: Keyword.Retain | Keyword.Forbidden,
                UpgradedKeywords: Keyword.Retain | Keyword.Forbidden,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Flippin'Loser",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(SubterraneanSunDef))]
        public sealed class SubterraneanSun : Card
        {
            public int count = 0;
            public int RemoveCount
            {
                get
                {
                    if (base.Battle != null)
                    {
                        return count + 1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            public override IEnumerable<BattleAction> OnTurnStartedInHand()
            {
                count++;
                for (int i = 0; i < count; i++)
                {
                    //List<Card> cards = base.Battle.EnumerateAllCards().Where((Card card) => card != this).ToList<Card>();
                    List<Card> cards = base.Battle.EnumerateAllCards().Where((Card card) => card.Zone != CardZone.Exile).ToList<Card>();
                    if (cards.Count != 0)
                    {
                        Card card = Util.UsefulFunctions.RandomUtsuho(cards);
                        foreach (BattleAction action in UsefulFunctions.RandomCheck(card, base.Battle)) { yield return action; }
                        if (Battle.EnemyGroup.Alives.Count() != 0)
                        {
                            EnemyUnit target = Battle.EnemyGroup.Alives.Sample(GameRun.BattleRng);
                            if (target != null && target.IsAlive)
                            {
                                yield return AttackAction(target);
                            }
                        }
                        yield return new RemoveCardAction(card);
                        if (card == this)
                        {
                            break;
                        }
                    }
                }
                yield break;
            }
        }
    }
}
