using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using HarmonyLib;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.Units;
using LBoL.EntityLib.Cards.Character.Reimu;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.EntityLib.Exhibits.Common;
using LBoL.EntityLib.Exhibits.Shining;
using LBoL.EntityLib.PlayerUnits;
using LBoL.Presentation;
using LBoL.Presentation.UI;
using LBoL.Presentation.UI.Panels;
using LBoL.Presentation.UI.Widgets;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader.UIhelpers;
using LBoLEntitySideloader.Utils;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Windows;
using Utsuho_character_mod.Util;
using static Utsuho_character_mod.BepinexPlugin;


namespace Utsuho_character_mod
{
    public sealed class UtsuhoPlayerDef : PlayerUnitTemplate
    {
        public static DirectorySource dir = new DirectorySource(PluginInfo.GUID, "Utsuho");

        public static string name = nameof(Utsuho);

        public override IdContainer GetId() => nameof(Utsuho);

        public override LocalizationOption LoadLocalization()
        {
            return UsefulFunctions.LocalizationPlayer(directorySource);
        }


        public override PlayerImages LoadPlayerImages()
        {
            var sprites = new PlayerImages();

            var asyncLoading = ResourceLoader.LoadSpriteAsync("utsuho.png", directorySource);

            sprites.SetStartPanelStand(asyncLoading);
            sprites.SetWinStand(asyncLoading);
            sprites.SetDeckStand(asyncLoading);

            return sprites;
        }

        public override PlayerUnitConfig MakeConfig()
        {
            var reimuConfig = PlayerUnitConfig.FromId("Reimu").Copy();

            var config = new PlayerUnitConfig(
            Id: "",
            ShowOrder: 6,
            Order: 0,
            UnlockLevel: 0,
            ModleName: "",
            NarrativeColor: "#e58c27",
            IsSelectable: true,
            MaxHp: 80,
            InitialMana: new LBoL.Base.ManaGroup() { Red = 2, Black = 2 },
            InitialMoney: 10,
            InitialPower: 0,
            UltimateSkillA: "UtsuhoUltR",
            UltimateSkillB: "UtsuhoUltB",
            ExhibitA: "ControlRod",
            ExhibitB: "BlackSun",
            DeckA: new List<string> { "Shoot", "Shoot", "Boundary", "Boundary", "MarisaAttackR", "MarisaAttackR", "MarisaBlockB", "MarisaBlockB", "MarisaBlockB", "HellGeyser" },
            DeckB: new List<string> { "Shoot", "Shoot", "Boundary", "Boundary", "MarisaAttackB", "MarisaAttackB", "MarisaBlockR", "MarisaBlockR", "MarisaBlockR", "ShootingStar" },
            DifficultyA: 3,
            DifficultyB: 3
            );
            return config;
        }


        [EntityLogic(typeof(UtsuhoPlayerDef))]
        public sealed class Utsuho : PlayerUnit { }

    }

    public sealed class UtsuhoModelDef : UnitModelTemplate
    {


        public override IdContainer GetId() => new UtsuhoPlayerDef().UniqueId;

        //public override LocalizationOption LoadLocalization() => new DirectLocalization(new Dictionary<string, object>() { { "Default", "Utsuho Reiuji" }, { "Short", "Utsuho" } });
        public override LocalizationOption LoadLocalization()
        {
            return UsefulFunctions.LocalizationModel(directorySource);
        }

        public override ModelOption LoadModelOptions()
        {
            return new ModelOption(ResourceLoader.LoadSpriteAsync("Utsuho_Sprite.png", directorySource, ppu: 56));
        }


        public override UniTask<Sprite> LoadSpellSprite() => ResourceLoader.LoadSpriteAsync("Stand.png", UtsuhoPlayerDef.dir, ppu: 1200);


        public override UnitModelConfig MakeConfig()
        {

            var config = UnitModelConfig.FromName("Reimu").Copy();
            config.Flip = true;
            config.Type = 0;
            config.Offset = new Vector2(0, 0.04f);
            return config;

        }
    }
}