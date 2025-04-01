using Cysharp.Threading.Tasks;
using HarmonyLib;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.Core.Intentions;
using LBoL.EntityLib.Cards.Enemy;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoL.EntityLib.Exhibits.Shining;
using LBoL.EntityLib.JadeBoxes;
using LBoL.EntityLib.StatusEffects.Others;
using LBoL.Presentation.UI.Panels;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.ReflectionHelpers;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;
using Utsuho_character_mod.CardsB;
using Utsuho_character_mod.Util;
using static Utsuho_character_mod.BepinexPlugin;
using static Utsuho_character_mod.UtsuhoPlayerDef;
using static Utsuho_character_mod.CardsB.DarkMatterDef;
using Utsuho_character_mod.Status;
using LBoL.Base.Extensions;
using UnityEngine.UI;
using LBoL.EntityLib.EnemyUnits.Normal;
using LBoL.EntityLib.StatusEffects.Basic;
using Spine;
using LBoL.EntityLib.StatusEffects.Enemy;

namespace Utsuho_character_mod
{
    public sealed class UtsuhoGroupDef : EnemyGroupTemplate
    {
        public override IdContainer GetId() => nameof(Utsuho);


        public override EnemyGroupConfig MakeConfig()
        {
            var config = new EnemyGroupConfig(
                Id: "",
                Name: "UtsuhoBoss",
                FormationName: VanillaFormations.Single,
                Enemies: new List<string>() { nameof(Utsuho) },
                EnemyType: EnemyType.Boss,
                Hidden: false,
                Environment: null,
                DebutTime: 1f,
                RollBossExhibit: true,
                PlayerRoot: new Vector2(-4f, 0.5f),
                PreBattleDialogName: "",
                PostBattleDialogName: "",
                IsSub: false,
                Subs: new List<string>() { }
            );
            return config;
        }
    }
    public sealed class UtsuhoBossDef : EnemyUnitTemplate
    {
        public override IdContainer GetId() => nameof(Utsuho);
        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }
        public override EnemyUnitConfig MakeConfig()
        {
            var config = new EnemyUnitConfig(
            Id: "",
            RealName: true,
            OnlyLore: false,
            BaseManaColor: new List<LBoL.Base.ManaColor>() { ManaColor.Red, ManaColor.Black },
            Order: 10,
            ModleName: "",
            NarrativeColor: "#cc0000",
            Type: EnemyType.Boss,
            IsPreludeOpponent: true,
            HpLength: null,
            MaxHpAdd: null,
            MaxHp: 240,
            Damage1: 6,
            Damage2: null,
            Damage3: 12,
            Damage4: 10,
            Power: 1,
            Defend: 15,
            Count1: 4,
            Count2: 5,
            MaxHpHard: 250,
            Damage1Hard: 8,
            Damage2Hard: null,
            Damage3Hard: 14,
            Damage4Hard: 10,
            PowerHard: 1,
            DefendHard: 18,
            Count1Hard: 5,
            Count2Hard: 6,
            MaxHpLunatic: 260,
            Damage1Lunatic: 10,
            Damage2Lunatic: null,
            Damage3Lunatic: 16,
            Damage4Lunatic: 15,
            PowerLunatic: 2,
            DefendLunatic: 20,
            Count1Lunatic: 6,
            Count2Lunatic: 7,
            PowerLoot: new MinMax(100, 100),
            BluePointLoot: new MinMax(100, 100),
            Gun1: new List<string> { "Sunny1" },
            Gun2: new List<string> { "火激光" },
            Gun3: new List<string> { "病气A" },
            Gun4: new List<string> { "陨星锤" }
            );
            if (act1BossConfig.Value == false)
                config.IsPreludeOpponent = false;
            return config;
        }
        [EntityLogic(typeof(UtsuhoBossDef))]
        public sealed class Utsuho : EnemyUnit
        {
            private string SpellCardName
            {
                get
                {
                    return base.GetSpellCardName(new int?(5), 6);
                }
            }
            public enum MoveType
            {
                Reactor,
                Aggregate,
                Bunker,
                Flare, 
                Night,
                Wait,
                Giant
            }
            int _spellTimes = 0;
            MoveType Next;
            protected override void OnEnterBattle(BattleController battle)
            {

                base.ReactBattleEvent<CardEventArgs>(battle.CardDrawn, new Func<GameEventArgs, IEnumerable<BattleAction>>(this.OnCardTouched));
                base.ReactBattleEvent<CardUsingEventArgs>(battle.CardUsed, new Func<GameEventArgs, IEnumerable<BattleAction>>(this.OnCardTouched));
                base.ReactBattleEvent<CardsEventArgs>(battle.CardsAddedToHand, new Func<GameEventArgs, IEnumerable<BattleAction>>(this.OnCardTouched));
                base.ReactBattleEvent<CardEventArgs>(base.Battle.CardExiled, new Func<GameEventArgs, IEnumerable<BattleAction>>(this.OnCardTouched));
                //base.ReactBattleEvent<GameEventArgs>(base.Battle.BattleStarted, new Func<GameEventArgs, IEnumerable<BattleAction>>(this.OnBattleStarted));
            }
            public override void OnSpawn(EnemyUnit spawner)
            {
                this.React(new ApplyStatusEffectAction<MirrorImage>(this, null, null, null, null, 0f, true));
            }
            private IEnumerable<BattleAction> OnCardTouched(GameEventArgs arg)
            {
                if(this.Next == MoveType.Giant)
                    base.UpdateTurnMoves();
                yield break;
            }
            private IEnumerable<BattleAction> OkuuDefend()
            {
                yield return new EnemyMoveAction(this, base.GetMove(2), true);
                yield return new CastBlockShieldAction(this, base.Defend, base.Defend, BlockShieldType.Normal, true);
                yield break;
            }
            protected override IEnumerable<IEnemyMove> GetTurnMoves()
            {
                switch (this.Next)
                {
                    case MoveType.Reactor:
                        GameDifficulty difficulty = base.GameRun.Difficulty;
                        if (difficulty == GameDifficulty.Lunatic)
                        {
                            this.React(new ApplyStatusEffectAction(typeof(ChargingStatus), this, Count1, null, null, null, 0f, true));
                            this.React(new ApplyStatusEffectAction(typeof(HeatStatus), this, Count1, null, null, null, 0f, true));

                            //yield return new ApplyStatusEffectAction<ChargingStatus>(this, Count1, null, null, null, 0f, true);
                            //yield return new ApplyStatusEffectAction<HeatStatus>(this, Count1, null, null, null, 0f, true);
                            this.Next = MoveType.Aggregate;
                            yield return base.AttackMove(base.GetMove(1), base.Gun1, base.Damage1, 1, false, "Instant", true);
                            yield return base.DefendMove(this, null, base.Damage1, 0, 0, false, null);
                            break;
                        }
                        else
                        {
                            yield return base.PositiveMove(base.GetMove(0), typeof(ChargingStatus), Count1, null, true, null);
                            break;
                        }
                    case MoveType.Aggregate:
                        yield return base.AttackMove(base.GetMove(1), base.Gun1, base.Damage1, 1, false, "Instant", true);
                        yield return base.DefendMove(this, null, base.Damage1, 0, 0, false, null);
                        //yield return base.PositiveMove(base.GetMove(0), typeof(HeatStatus), Damage1, null, true, null);
                        break;
                    case MoveType.Bunker:
                        yield return new SimpleEnemyMove(Intention.Defend().WithMoveName(base.GetMove(2)), this.OkuuDefend());
                        break;
                    case MoveType.Flare:
                        yield return base.AttackMove(base.GetMove(3), base.Gun2, this.GetStatusEffect<HeatStatus>().Level, 1, false, "Instant", true);
                        yield return new SimpleEnemyMove(IntentionTemplate.CreateIntention(typeof(VentIntentionDef.VentIntention)), PositiveActions(null, typeof(HeatStatus), -(this.GetStatusEffect<HeatStatus>().Level), 0, 0f));
                        Type typeFromHandle = typeof(Weak);
                        Type typeFromHandle2 = typeof(Vulnerable);
                        yield return base.NegativeMove(null, typeFromHandle, null, base.Power, false, false, null);
                        yield return base.NegativeMove(null, typeFromHandle2, null, base.Power, false, false, null);
                        break;
                    case MoveType.Night:
                        yield return base.AttackMove(base.GetMove(4), base.Gun3, base.Damage3, 1, false, "Instant", true);
                        yield return base.AddCardMove(null, typeof(DarkMatter), Count2, EnemyUnit.AddCardZone.Hand, null, false);
                        break;
                    case MoveType.Wait:
                        yield return new SimpleEnemyMove(Intention.CountDown(1));
                        break;
                    case MoveType.Giant:
                        Card[] array = base.Battle.HandZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                        int mass = 0;
                        foreach (Card card in array)
                        {
                            if ((card is UtsuhoCard uCard) && (uCard.isMass))
                            {
                                mass++;
                            }
                        }
                        yield return new SimpleEnemyMove(Intention.SpellCard(this.SpellCardName, new int?(base.Damage4 + mass * 5 + _spellTimes * 4), true), this.AttackActions(this.SpellCardName, base.Gun4, base.Damage4 + mass * 5, 1, true, "Instant"));
                        yield return new SimpleEnemyMove(IntentionTemplate.CreateIntention(typeof(PullIntentionDef.PullIntention)));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            protected override void UpdateMoveCounters()
            {
                MoveType moveType;

                switch (Next)
                {
                    case MoveType.Reactor:
                        moveType = MoveType.Aggregate;
                        break;
                    case MoveType.Aggregate:
                        moveType = MoveType.Bunker;
                        break;
                    case MoveType.Bunker:
                        moveType = MoveType.Flare;
                        break;
                    case MoveType.Flare:
                        moveType = MoveType.Night;
                        break;
                    case MoveType.Night:
                        moveType = MoveType.Wait;
                        break;
                    case MoveType.Wait:
                        moveType = MoveType.Giant;
                        break;
                    case MoveType.Giant:
                        this._spellTimes++;
                        moveType = MoveType.Aggregate;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                this.Next = moveType;
            }
        }
    }
}