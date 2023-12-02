using LBoL.ConfigData;
using LBoL.Core.Cards;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using static Utsuho_character_mod.BepinexPlugin;
using UnityEngine;
using LBoL.Core;
using LBoL.Base;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Base.Extensions;
using System.Collections;
using LBoL.Presentation;
using LBoL.EntityLib.Cards.Neutral.Blue;
using LBoL.EntityLib.Exhibits.Shining;
using LBoL.EntityLib.Exhibits;
using LBoL.Core.Units;
using Utsuho_character_mod.Status;

namespace Utsuho_character_mod.Exhibits
{
    public sealed class BlackSunDefinition : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(BlackSun);
        }

        public override LocalizationOption LoadLocalization()
        {
            // creates global localization for exhibits. Each entity type needs to have their own global localization
            var globalLoc = new GlobalLocalization(embeddedSource);
            globalLoc.LocalizationFiles.AddLocaleFile(Locale.En, "ExhibitsEn.yaml");

            return globalLoc;
        }

        public override ExhibitSprites LoadSprite()
        {
            // embedded resource folders are separated by a dot
            var folder = "Resources.";
            var exhibitSprites = new ExhibitSprites();
            Func<string, Sprite> wrap = (s) => ResourceLoader.LoadSprite((folder + GetId() + s + ".png"), embeddedSource);

            exhibitSprites.main = wrap("");

            return exhibitSprites;
        }



        public override ExhibitConfig MakeConfig()
        {
            var exhibitConfig = new ExhibitConfig(
                Index: 0,
                Id: "",
                Order: 10,
                IsDebug: false,
                IsPooled: false,
                IsSentinel: false,
                Revealable: false,
                Appearance: AppearanceType.Nowhere,
                Owner: "Utsuho",
                LosableType: ExhibitLosableType.DebutLosable,
                Rarity: Rarity.Shining,
                Value1: 1,
                Value2: null,
                Value3: null,
                Mana: new ManaGroup() { Colorless = 1 },
                BaseManaRequirement: null,
                BaseManaColor: ManaColor.Black,
                BaseManaAmount: 1,
                HasCounter: false,
                InitialCounter: null,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { "DarkMatter" }
                )
            {

            };
            return exhibitConfig;
        }

        [EntityLogic(typeof(BlackSunDefinition))]
        public sealed class BlackSun : ShiningExhibit
        {
            protected override void OnEnterBattle()
            {
                ReactBattleEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnStarted));
                ReactBattleEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
                triggered = false;
            }

            private IEnumerable<BattleAction> OnPlayerTurnStarted(GameEventArgs args)
            {
                triggered = false;
                yield break;
            }


            private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
            {
                if ((args.Card.BaseName == "Dark Matter") && (triggered == false))
                {
                    NotifyActivating();
                    triggered = true;
                    GameMaster.Instance.StartCoroutine(ResetTrigger());
                    yield return new GainManaAction(Mana);
                }
                yield break;
            }


            IEnumerator ResetTrigger()
            {
                // keeps the exhibits icon fully lit for 1.5 sec after it has activated
                yield return new WaitForSeconds(1.5f);
                NotifyChanged();
            }

            private bool triggered;
        }
    }
}