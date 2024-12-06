using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.CustomKeywords;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Utsuho_character_mod.Status;

namespace Utsuho_character_mod.Util
{
    public abstract class UtsuhoCard : Card
    {
        public virtual bool isMass { get; set; }
        public virtual bool isKicker { get; set; }
        public virtual bool isMultiKicker { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            if (isMass == true)
            {
                this.AddCustomKeyword(UtsuhoKeyword.Mass);
            }
            else
            {
                isMass = false;
            }
            /*if (isKicker == true)
            {
                this.AddCustomKeyword(UtsuhoKeyword.Kicker);
            }
            else
            {
                isKicker = false;
            }
            if (isMultiKicker == true)
            {
                this.AddCustomKeyword(UtsuhoKeyword.MultiKicker);
            }
            else
            {
                isMultiKicker = false;
            }*/
        }

        public virtual IEnumerable<BattleAction> OnPull() 
        {
            yield break;
        }
    }
}
