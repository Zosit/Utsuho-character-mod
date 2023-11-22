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

namespace Utsuho_character_mod.CardsR
{
    public sealed class NightFallsDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(NightFalls);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(embeddedSource);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "CardsEn.yaml");
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
                Rarity: Rarity.Uncommon,
                Type: CardType.Skill,
                TargetType: TargetType.AllEnemies,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Any = 1 },
                UpgradedCost: new ManaGroup() { Black = 1, Any = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 3,
                UpgradedValue1: 3,
                Value2: 1,
                UpgradedValue2: 2,
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

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
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

        [EntityLogic(typeof(NightFallsDef))]
        public sealed class NightFalls : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                foreach (BattleAction battleAction in base.DebuffAction<Weak>(selector.GetUnits(base.Battle), 0, base.Value1, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }
                /*int num = base.Battle.MaxHand - base.Battle.HandZone.Count;
                IReadOnlyList<Card> drawZoneIndexOrder = base.Battle.DrawZoneIndexOrder;
                List<Card> cards = base.Battle.DrawZone.Where((Card card) => card != this).ToList<Card>();
                List<Card> darkMatter = drawZoneIndexOrder.FindAll((Card card) => card.BaseName == "Dark Matter");
                int totalDM = darkMatter.Count;

                if (num > Value2)
                {
                    num = Value2;
                }
                if (num > totalDM)
                {
                    num = totalDM;
                }
                for (int i = 0; i < num; i++)
                {
                    Battle.MoveCard(darkMatter[i], CardZone.Hand);
                }*/


                Card card;
                Card card2;
                if (Battle.HandZone.Count > 0)
                    card = Battle.HandZone.Sample(base.GameRun.BattleRng);
                else
                    yield break;
                if (this.IsUpgraded && Battle.HandZone.Count > 1)
                {
                    do
                    {
                        card2 = Battle.HandZone.Sample(base.GameRun.BattleRng);
                    }
                    while (card == card2);

                    card2.IsTempRetain = true;
                }                
                card.IsTempRetain = true;
                yield break;
            }

        }

    }
}
