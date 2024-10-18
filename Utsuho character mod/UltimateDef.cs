using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.Core;
using LBoL.Presentation;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine;
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Status;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod
{
    public sealed class UtsuhoUltRDef : UltimateSkillTemplate
    {
        public override IdContainer GetId() => nameof(UtsuhoUltR);

        public override LocalizationOption LoadLocalization()
        {
            return UsefulFunctions.LocalizationUlt(directorySource);
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Nuclear.png", embeddedSource);
        }

        public override UltimateSkillConfig MakeConfig()
        {
            var config = new UltimateSkillConfig(
                Id: "",
                Order: 10,
                PowerCost: 100,
                PowerPerLevel: 100,
                MaxPowerLevel: 2,
                RepeatableType: UsRepeatableType.OncePerTurn,
                Damage: 30,
                Value1: 30,
                Value2: 0,
                Keywords: Keyword.Accuracy,
                RelativeEffects: new List<string>() { "HeatStatus" },
                RelativeCards: new List<string>() { }
                );

            return config;
        }
    }

    [EntityLogic(typeof(UtsuhoUltRDef))]
    public sealed class UtsuhoUltR : UltimateSkill
    {
        public UtsuhoUltR()
        {
            base.TargetType = TargetType.SingleEnemy;
            base.GunName = "Simple1";
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector)
        {
            /*var bgGo = StageTemplate.TryGetEnvObject(NewBackgrounds.ghibliDeez);

            bgGo.SetActive(true);
            GameMaster.Instance.StartCoroutine(DeactivateDeez(bgGo));

            yield return PerformAction.Spell(Owner, new UtsuhoUltRDef().UniqueId);*/
            yield return PerformAction.Spell(Battle.Player, "UtsuhoUltR");
            yield return new DamageAction(base.Owner, selector.GetEnemy(base.Battle), this.Damage, base.GunName, GunType.Single);
            if (!base.Battle.BattleShouldEnd)
            {
                yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, Value1, null, null, null, 0f, true);
            }
        }
        IEnumerator DeactivateDeez(GameObject go)
        {
            yield return new WaitForSeconds(5f);
            go.SetActive(false);
        }
    }


    public sealed class UtsuhoUltBDef : UltimateSkillTemplate
    {
        public override IdContainer GetId() => nameof(UtsuhoUltB);

        public override LocalizationOption LoadLocalization()
        {
            return UsefulFunctions.LocalizationUlt(directorySource);
        }

        public override Sprite LoadSprite()
        {
            return ResourceLoader.LoadSprite("Nuclear.png", embeddedSource);
        }

        public override UltimateSkillConfig MakeConfig()
        {
            var config = new UltimateSkillConfig(
                Id: "",
                Order: 10,
                PowerCost: 100,
                PowerPerLevel: 100,
                MaxPowerLevel: 2,
                RepeatableType: UsRepeatableType.OncePerTurn,
                Damage: 30,
                Value1: 0,
                Value2: 0,
                Keywords: Keyword.Accuracy,
                RelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { "DarkMatter" }
                );

            return config;
        }
    }

    [EntityLogic(typeof(UtsuhoUltBDef))]
    public sealed class UtsuhoUltB : UltimateSkill
    {
        public UtsuhoUltB()
        {
            base.TargetType = TargetType.AllEnemies;
            base.GunName = "Simple1";
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector)
        {
            /*var bgGo = StageTemplate.TryGetEnvObject(NewBackgrounds.ghibliDeez);

            bgGo.SetActive(true);
            GameMaster.Instance.StartCoroutine(DeactivateDeez(bgGo));

            yield return PerformAction.Spell(Owner, new UtsuhoUltRDef().UniqueId);*/
            yield return PerformAction.Spell(Battle.Player, "UtsuhoUltB");
            yield return new DamageAction(base.Owner, base.Battle.EnemyGroup.Alives, Damage, this.GunName, GunType.Single);
            if (!base.Battle.BattleShouldEnd) {
                Card[] cards = { Library.CreateCard("DarkMatter") };
                yield return new AddCardsToDrawZoneAction(cards, DrawZoneTarget.Random);
                yield return new AddCardsToHandAction(Library.CreateCard("DarkMatter"));
                yield return new AddCardsToDiscardAction(Library.CreateCard("DarkMatter"));
            }
        }
        IEnumerator DeactivateDeez(GameObject go)
        {
            yield return new WaitForSeconds(5f);
            go.SetActive(false);
        }
    }
}
