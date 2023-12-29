using LBoL.Base.Extensions;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.EntityLib.StatusEffects.Basic;
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
        public UtsuhoCard() : base()
        {
            isMass = false;
        }
        public virtual IEnumerable<BattleAction> OnPull() 
        {
            yield break;
        }
    }
}
