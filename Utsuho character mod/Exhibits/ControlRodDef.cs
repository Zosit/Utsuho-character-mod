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
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.Exhibits
{
    public sealed class ControlRodDefinition : ExhibitTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(ControlRod);
        }

        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        public override ExhibitSprites LoadSprite()
        {
            // embedded resource folders are separated by a dot
            var folder = "Resources.";
            var exhibitSprites = new ExhibitSprites();
            Func<string, Sprite> wrap = (s) => ResourceLoader.LoadSprite(folder + GetId() + s + ".png", embeddedSource);

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
                Mana: new ManaGroup() { Red = 1 },
                BaseManaRequirement: null,
                BaseManaColor: ManaColor.Red,
                BaseManaAmount: 1,
                HasCounter: false,
                InitialCounter: null,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { "HeatStatus" },
                // example of referring to UniqueId of an entity without calling MakeConfig
                RelativeCards: new List<string>() { }
                )
            {

            };
            return exhibitConfig;
        }

        [EntityLogic(typeof(ControlRodDefinition))]
        public sealed class ControlRod : ShiningExhibit
        {
            private bool triggered;
            protected override void OnEnterBattle()
            {
                ReactBattleEvent(Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(OnCardUsed));
            }
            private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
            {
                NotifyActivating();
                triggered = true;
                GameMaster.Instance.StartCoroutine(ResetTrigger());
                yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, new int?(Value1), null, null, null, 0f, true);
            }


            IEnumerator ResetTrigger()
            {
                // keeps the exhibits icon fully lit for 1.5 sec after it has activated
                yield return new WaitForSeconds(1.5f);
                triggered = false;
                NotifyChanged();
            }
        }
    }
}