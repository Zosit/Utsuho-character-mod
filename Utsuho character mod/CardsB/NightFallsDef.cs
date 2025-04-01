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
using System.Linq;
using Utsuho_character_mod.Util;
using LBoL.Core.Randoms;
using HarmonyLib;

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
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: 13200,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "病气A",
                GunNameBurst: "病气A",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: true,
                FindInBattle: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Uncommon,
                Type: CardType.Attack,
                TargetType: TargetType.SingleEnemy,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Any = 1 },
                UpgradedCost: new ManaGroup() { Black = 1, Any = 1 },
                MoneyCost: null,
                Damage: 0,
                UpgradedDamage: 0,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 5,
                UpgradedValue1: 7,
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

                Keywords: Keyword.Accuracy,
                UpgradedKeywords: Keyword.Accuracy,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "MassStatus" },
                UpgradedRelativeEffects: new List<string>() { "MassStatus" },
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "AltAlias",
                SubIllustrator: new List<string>() { "Zosit" }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(NightFallsDef))]
        public sealed class NightFalls : Card
        {
            public override int AdditionalDamage
            {
                get
                {
                    int mass = 0;
                    if (base.Battle != null)
                    {
                        Card[] array = base.Battle.HandZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                        if (array.Length != 0)
                        {
                            foreach (Card card in array)
                            {
                                if ((card is UtsuhoCard uCard) && (uCard.isMass))
                                {
                                    mass++;
                                }
                                if (card == this)
                                {
                                    mass++;
                                }
                            }
                        }
                        /*if(this.Zone == CardZone.PlayArea)
                        {
                            mass--;
                        }*/
                        return (Value1 * (AddCount + mass));
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            public int AddCount
            {
                get
                {
                    if (base.Battle != null)
                    {
                        return base.Battle.MaxHand - base.Battle.HandZone.Count;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                /*foreach (BattleAction battleAction in base.DebuffAction<Weak>(selector.GetUnits(base.Battle), 0, base.Value1, 0, 0, true, 0.2f))
                {
                    yield return battleAction;
                }*/
                int num = AddCount;
                //this.DeltaDamage += Value1 * num;
                yield return new AddCardsToHandAction(Library.CreateCards<DarkMatter>(num));
                yield return AttackAction(selector);
                //this.DeltaDamage = 0;
                yield break;
            }

        }

    }
}
