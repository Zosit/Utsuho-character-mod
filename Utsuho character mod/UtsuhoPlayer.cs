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
using static LBoLEntitySideloader.Resource.PlayerImages;
using static Utsuho_character_mod.BepinexPlugin;


namespace Utsuho_character_mod
{
    public sealed class UtsuhoPlayerDef : PlayerUnitTemplate
    {
        //public static DirectorySource dir = new DirectorySource(PluginInfo.GUID, "Utsuho");

        public static string name = nameof(Utsuho);

        public override IdContainer GetId() => nameof(Utsuho);

        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }


        public override PlayerImages LoadPlayerImages()
        {
            var sprites = new PlayerImages();

            var imprint = ResourceLoader.LoadSprite("CardImprint.png", directorySource);
            var collectionIconLoading = ResourceLoader.LoadSprite("CollectionIcon.png", directorySource);
            var selectionCircleIconLoading = ResourceLoader.LoadSprite("SelectionCircleIcon.png", directorySource);
            var avatarLoading = ResourceLoader.LoadSprite("Avatar.png", directorySource);
            var standLoading = ResourceLoader.LoadSpriteAsync("Stand.png", directorySource);
            var winStandLoading = ResourceLoader.LoadSpriteAsync("WinStand.png", directorySource);
            var defeatedStandLoading = ResourceLoader.LoadSpriteAsync("DefeatedStand.png", directorySource);
            var perfectWinIconLoading = ResourceLoader.LoadSpriteAsync("PerfectWinIcon.png", directorySource);
            var winIconLoading = ResourceLoader.LoadSpriteAsync("WinIcon.png", directorySource);
            var defeatedIconLoading = ResourceLoader.LoadSpriteAsync("DefeatedIcon.png", directorySource);

            sprites.SetStartPanelStand(standLoading);
            sprites.SetDeckStand(standLoading);
            sprites.SetWinStand(winStandLoading);
            sprites.SetDefeatedStand(defeatedStandLoading);
            sprites.SetPerfectWinIcon(perfectWinIconLoading);
            sprites.SetWinIcon(winIconLoading);
            sprites.SetDefeatedIcon(defeatedIconLoading);
            sprites.SetCollectionIcon(() => collectionIconLoading);
            sprites.SetSelectionCircleIcon(() => selectionCircleIconLoading);
            sprites.SetInRunAvatarPic(() => avatarLoading);


            //sprites.AutoLoad("", (s) => ResourceLoader.LoadSprite(s, directorySource, ppu: 100, 1, FilterMode.Bilinear, generateMipMaps: true), (s) => ResourceLoader.LoadSpriteAsync(s, BepinexPlugin.directorySource), PlayerImages.UseSame.StandAndDeck);
            sprites.SetCardImprint(() => imprint);

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
            DeckA: new List<string> { "Shoot", "Shoot", "Boundary", "Boundary", "FireBurst", "FireBurst", "Gridlock", "Gridlock", "Gridlock", "HellGeyser" },
            DeckB: new List<string> { "Shoot", "Shoot", "Boundary", "Boundary", "InverseBeam", "InverseBeam", "BlowAway", "BlowAway", "BlowAway", "ShootingStar" },
            DifficultyA: 2,
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


        public override UniTask<Sprite> LoadSpellSprite() => ResourceLoader.LoadSpriteAsync("Stand.png", directorySource, ppu: 1200);


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