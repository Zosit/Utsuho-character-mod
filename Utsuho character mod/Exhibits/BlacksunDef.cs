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
using System.Linq;

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
            return UsefulFunctions.LocalizationExhibit(directorySource);
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
                Value1: 3,
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
            private string GunName
            {
                get
                {
                    return "无差别起火";
                }
            }
            protected override void OnEnterBattle()
            {
                base.ReactBattleEvent<GameEventArgs>(base.Battle.Reshuffling, new EventSequencedReactor<GameEventArgs>(this.Reshuffling));
            }

            private IEnumerable<BattleAction> Reshuffling(GameEventArgs args)
            {
                List<Card> cards = base.Battle.DiscardZone.ToList<Card>();
                int total = cards.FindAll((Card card) => card.Id == "DarkMatter").Count;
                yield return new DamageAction(base.Owner, base.Battle.EnemyGroup.Alives, DamageInfo.Reaction((float)(base.Value1 * total)), this.GunName, GunType.Single);

                yield break;
            }
        }
    }
}