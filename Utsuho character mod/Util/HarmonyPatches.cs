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
using LBoL.Core.Units;
using LBoLEntitySideloader.Entities;
using LBoL.EntityLib.Cards.Neutral.Black;
using LBoL.Base;
using LBoL.Core.Cards;
using LBoL.Base.Extensions;
using System.Linq;
using Utsuho_character_mod.Util;
using BepInEx.Logging;
using LBoL.EntityLib.Cards.Neutral.NoColor;
using LBoL.EntityLib.Cards.Neutral.Colorless;
using LBoL.EntityLib.StatusEffects.Neutral.White;
using LBoL.Core.Battle.Interactions;
using LBoL.Presentation.UI.Panels;
using System.Collections;
using System.Reflection.Emit;
using System.Reflection;

namespace PullFix.Patches
{
    public static class Helpers
    {

        public static CodeMatcher LeaveJumpFix(this CodeMatcher matcher)
        {
            matcher.Start();
            while (matcher.IsValid)
            {
                matcher.MatchEndForward(OpCodes.Leave)
                    .Advance(1);
                if (matcher.IsInvalid)
                    break;
                matcher.InsertAndAdvance(new CodeInstruction(OpCodes.Nop));
            }
            return matcher;
        }
    }
    /*[HarmonyPatch(typeof(BattleController), nameof(BattleController.ShuffleDrawPile))]
    class ShuffleDrawPile_Patch
    {
        static bool Prefix(BattleController __instance)
        {
            var bc = __instance;
            var gr = bc.GameRun;
            //var shuffledDrawZone = BattleRngs.Shuffle(gr.ShuffleRng, bc._drawZone);
            bc._drawZone.Clear();
            bc._drawZone.AddRange(shuffledDrawZone);

            return false;
        }
    }*/


    /*[HarmonyPatch(typeof(Card), MethodType.Normal, new Type[] { typeof(int), typeof(CardType) })]
    class RandomUpgrade_Patch
    {


        static List<Card> SampleManyOrAllReplacement(int count, List<Card> toRand)
        {
            return null;
            //return BattleRngs.Shuffle(rng, toShuffle);
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            return new CodeMatcher(instructions)
                 .SearchForward(ci => ci.opcode == OpCodes.Call && ci.operand is MethodInfo mi && mi.Name == "SampleManyOrAll")
                 .SetInstructionAndAdvance(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(RandomUpgrade_Patch), nameof(RandomUpgrade_Patch.SampleManyOrAllReplacement))))
                 .InsertAndAdvance(new CodeInstruction(OpCodes.Stloc_0))

                 .LeaveJumpFix().InstructionEnumeration();
        }

    }*/



}

/*[HarmonyPatch(typeof(FangxiangHeal))]
class FangxiangHeal_OnUse_Patch
{
    [HarmonyPatch(nameof(FangxiangHeal.Actions), typeof(UnitSelector), typeof(ManaGroup), typeof(Interaction))]
    static bool Prefix(FangxiangHeal __instance, ref IEnumerable<BattleAction> __result, UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
    {
        //__result = new List<BattleAction>() { };
        if (__instance.Battle.DiscardZone.Count > 0)
        {
            int count = 0;
            List<Card> list = new List<Card>();
            List<BattleAction> actions = new List<BattleAction>();
            for (int i = 0; i < __instance.Value1; i++)
            {
                Card[] discardZone = __instance.Battle.DiscardZone.Where((Card card) => !list.Contains(card)).ToArray<Card>();
                if (discardZone.Length != 0)
                {

                    Card card = UsefulFunctions.RandomUtsuho(discardZone);
                    foreach (BattleAction action in UsefulFunctions.RandomCheck(card, __instance.Battle)) { actions.Add(action); }
                    list.Add(card);
                    count++;
                }
            }
            //actions.Append(new ExileManyCardAction(list));
            //actions.Append(__instance.HealAction(count * __instance.Value2));
            actions.Add(new ExileManyCardAction(list));
            actions.Add(__instance.HealAction(count * __instance.Value2));
            __result = actions;
            return false;
        }
        return true;
    }
}

[HarmonyPatch(typeof(MoonPurify))]
class MoonPurify_OnUse_Patch
{
    [HarmonyPatch(nameof(MoonPurify.Actions), typeof(UnitSelector), typeof(ManaGroup), typeof(Interaction))]
    static bool Prefix(MoonPurify __instance, ref IEnumerable<BattleAction> __result, UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
    {
        if (!__instance.IsUpgraded)
        {
            List<BattleAction> actions = new List<BattleAction>();
            actions.Add(__instance.DefenseAction(true));

            List<Card> list1 = __instance.Battle.HandZone.Where((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass) && !card.IsPurified && card.Cost.HasTrivial).ToList<Card>();
            List<Card> list2 = __instance.Battle.HandZone.Where((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass) && !card.IsPurified).ToList<Card>();
            List<Card> list3 = __instance.Battle.HandZone.Where((Card card) => !card.IsPurified && card.Cost.HasTrivial).ToList<Card>();
            List<Card> list4 = __instance.Battle.HandZone.Where((Card card) => !card.IsPurified).ToList<Card>();
            if (list1.Count > 0)
            {
                Card card1 = UsefulFunctions.RandomUtsuho(list1);
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card1, __instance.Battle)) { actions.Add(action); }
                //Card card2 = list.Sample(__instance.GameRun.BattleRng);
                card1.NotifyActivating();
                card1.IsPurified = true;
            }
            else if (list2.Count > 0)
            {
                Card card2 = UsefulFunctions.RandomUtsuho(list2);
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card2, __instance.Battle)) { actions.Add(action); }
                //Card card3 = list2.Sample(__instance.GameRun.BattleRng);
                card2.NotifyActivating();
                card2.IsPurified = true;
            }
            else if (list3.Count > 0)
            {
                Card card3 = UsefulFunctions.RandomUtsuho(list3);
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card3, __instance.Battle)) { actions.Add(action); }
                //Card card3 = list2.Sample(__instance.GameRun.BattleRng);
                card3.NotifyActivating();
                card3.IsPurified = true;
            }
            else if (list4.Count > 0)
            {
                Card card4 = UsefulFunctions.RandomUtsuho(list4);
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card4, __instance.Battle)) { actions.Add(action); }
                //Card card3 = list2.Sample(__instance.GameRun.BattleRng);
                card4.NotifyActivating();
                card4.IsPurified = true;
            }
            __result = actions;
            return false;
        }
        return true;
    }
}

//Doesn't work, since you can't return battleactions.
[HarmonyPatch(typeof(MoonWorldSe))]
class MoonWorldSe_OnActive_Patch
{
    [HarmonyPatch(nameof(MoonWorldSe.OnTurnStarted), typeof(UnitEventArgs))]
    static bool Prefix(MoonWorldSe __instance, UnitEventArgs args)
    {
        List<BattleAction> actions = new List<BattleAction>();
        int i = 0;
        bool flag = false;
        while (i < __instance.Level)
        {
            List<Card> list1 = __instance.Battle.HandZone.Where((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass) && !card.IsPurified && card.Cost.HasTrivial).ToList<Card>();
            List<Card> list2 = __instance.Battle.HandZone.Where((Card card) => (card is UtsuhoCard uCard) && (uCard.isMass) && !card.IsPurified).ToList<Card>();
            List<Card> list3 = __instance.Battle.HandZone.Where((Card card) => !card.IsPurified && card.Cost.HasTrivial).ToList<Card>();
            List<Card> list4 = __instance.Battle.HandZone.Where((Card card) => !card.IsPurified).ToList<Card>();
            if (list1.Count > 0)
            {
                Card card1 = UsefulFunctions.RandomUtsuho(list1);
                card1.NotifyActivating();
                card1.IsPurified = true;
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card1, __instance.Battle)) { actions.Add(action); }
                if (!flag)
                {
                    __instance.NotifyActivating();
                    flag = true;
                }
            }
            else if (list2.Count > 0)
            {
                Card card2 = UsefulFunctions.RandomUtsuho(list2);
                card2.NotifyActivating();
                card2.IsPurified = true;
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card2, __instance.Battle)) { actions.Add(action); }
                if (!flag)
                {
                    __instance.NotifyActivating();
                    flag = true;
                }
            }
            else if (list3.Count > 0)
            {
                Card card3 = UsefulFunctions.RandomUtsuho(list3);
                card3.NotifyActivating();
                card3.IsPurified = true;
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card3, __instance.Battle)) { actions.Add(action); }
                if (!flag)
                {
                    __instance.NotifyActivating();
                    flag = true;
                }
            }
            else if (list4.Count > 0)
            {
                Card card4 = UsefulFunctions.RandomUtsuho(list4);
                card4.NotifyActivating();
                card4.IsPurified = true;
                foreach (BattleAction action in UsefulFunctions.RandomCheck(card4, __instance.Battle)) { actions.Add(action); }
                if (!flag)
                {
                    __instance.NotifyActivating();
                    flag = true;
                }
            } 
            else
            {
                break;
            }
            i++;
        }
        __instance.React(new Reactor(actions));
        //foreach (BattleAction action in actions) { __instance.Battle.React(action, source: __instance.Owner, cause: ActionCause.StatusEffect); }
        return false;
    }
}

[HarmonyPatch(typeof(RemiliaFate))]
class RemiliaFate_OnUse_Patch
{
    [HarmonyPatch(nameof(RemiliaFate.Actions), typeof(UnitSelector), typeof(ManaGroup), typeof(Interaction))]
    static bool Prefix(RemiliaFate __instance, ref IEnumerable<BattleAction> __result, UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
    {
        List<BattleAction> actions = new List<BattleAction>();

        SelectHandInteraction selectHandInteraction = (SelectHandInteraction)precondition;
        IReadOnlyList<Card> readOnlyList = ((selectHandInteraction != null) ? selectHandInteraction.SelectedCards : null);
        if (readOnlyList != null && readOnlyList.Count > 0)
        {
            actions.Add(new DiscardManyAction(readOnlyList));
        }
        if (__instance.Battle.DrawZone.Count > 0)
        {
            int max = (__instance.IsUpgraded ? 5 : 3);
            //RemiliaFate. 8__locals1 = new RemiliaFate.<> c__DisplayClass3_0();
            int locali = 1;
            while (locali <= max && __instance.Battle.DrawZone.Count != 0 && __instance.Battle.HandZone.Count != __instance.Battle.MaxHand)
				{
                List<Card> list = __instance.Battle.DrawZone.Where((Card card) => !card.IsForbidden && card.ConfigCost.Amount == locali).ToList<Card>();
                if (list.Count > 0)
                {
                    Card card2 = UsefulFunctions.RandomUtsuho(list);
                    actions.Add(new MoveCardAction(card2, CardZone.Hand));
                    foreach (BattleAction action in UsefulFunctions.RandomCheck(card2, __instance.Battle)) { actions.Add(action); }
                }
                int i = locali;
                locali = i + 1;
            }
            //locals1 = null;
        }
        __result = actions;
        return false;
    }
}*/
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