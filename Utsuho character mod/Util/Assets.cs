using HarmonyLib;
using LBoL.Core.Cards;
using LBoL.Presentation.UI.Widgets;
using LBoLEntitySideloader.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace Utsuho_character_mod.Util
{
    public static class AssetManager
    {
        public static ExtraAssets Assets { get; private set; } = null;

        public static void Load()
        {
            Assets = new ExtraAssets();
            Assets.massBorder = ResourceLoader.LoadSprite("MassBorder.png", BepinexPlugin.embeddedSource);
        }
    }

    public class ExtraAssets
    {
        public Sprite massBorder;
    }

    [HarmonyPatch]
    public static class CardOverlay
    {
        private static readonly string OverlayName = "CardOverlayOkuu";
        [HarmonyPatch(typeof(CardWidget), nameof(CardWidget.LazySetCard)), HarmonyPostfix]
        static void AddOverlay(CardWidget __instance)
        {
            if (__instance.Card != null)
            {
                Transform overlay = __instance.root.Find(OverlayName);

                if (null == overlay)
                {
                    GameObject go = GameObject.Instantiate(__instance.cardMainBg.gameObject, __instance.cardMainBg.transform.parent, worldPositionStays: true);
                    go.name = OverlayName;
                    overlay = go.transform;
                    overlay.SetSiblingIndex(__instance.cardSubBg.transform.GetSiblingIndex() + 1);

                    RawImage img = go.GetComponent<RawImage>();
                    img.texture = AssetManager.Assets.massBorder.texture;
                }

                overlay.gameObject.SetActive((__instance.Card is UtsuhoCard uCard) && (uCard.isMass));
            }
        }
    }
}
