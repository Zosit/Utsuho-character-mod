using HarmonyLib;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.EntityLib.EnemyUnits.Character;
using Utsuho_character_mod;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using static Utsuho_character_mod.UtsuhoBossDef;
using LBoL.Core.Battle.BattleActions;
using LBoL.EntityLib.StatusEffects.Enemy;

/*[HarmonyPatch(typeof(Siji))]
class Siji_OnBattleStarted_Patch
{
    [HarmonyPatch(nameof(Siji.OnBattleStarted), typeof(GameEventArgs))]
    static bool Prefix(Siji __instance, ref IEnumerable<BattleAction> __result, GameEventArgs arg)
    {
        string id = __instance.GameRun.Player.Id;
        if (id == "Utsuho")
        {
            string spellcard = "净颇梨审判";
            spellcard += " -博丽灵梦-";
            Type type = typeof(Utsuho);

            string str = "Chat.Siji2".LocalizeFormat(new object[] { __instance.Battle.Player.GetName() });

            int count = __instance.Battle.Player.Exhibits.Count;
            int num = 1;
            if (count >= 20)
            {
                num = 3;
            }
            else if (count >= 10)
            {
                num = 2;
            }

            __result = new List<BattleAction>() {
                PerformAction.Chat(__instance, "Chat.Siji1".Localize(true), 3f, 0f, 3.2f, true),
                PerformAction.Animation(__instance, "shoot2", 0f, null, 0f, -1),
                PerformAction.Chat(__instance, str, 3f, 0f, 3.2f, true),
                PerformAction.Animation(__instance, "spell", 0f, null, 0f, -1),
                PerformAction.Spell(__instance, spellcard),
                new SpawnEnemyAction(__instance, type, 2, 0f, 0.3f, false),
                new ApplyStatusEffectAction<SijiZui>(__instance, new int?(num), null, null, null, 0f, true)
            };
            return false;
        }
        return true;
    }
}*/
