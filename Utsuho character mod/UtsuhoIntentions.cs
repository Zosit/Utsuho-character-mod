using System;
using JetBrains.Annotations;
using LBoL.Core.Units;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Attributes;
using Utsuho_character_mod.Status;
using UnityEngine;
using static Utsuho_character_mod.BepinexPlugin;
using LBoL.Core.StatusEffects;
using LBoL.EntityLib.StatusEffects.Enemy;
using LBoL.Presentation;
using System.Collections.Generic;

namespace Utsuho_character_mod
{
    public sealed class VentIntentionDef : IntentionTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(VentIntention);
        }

        [DontOverwrite]
        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        public override IntentionImages LoadSprites()
        {
            //return ResourceLoader.LoadSprite("EnergyStatus.png", BepinexPlugin.embeddedSource);
            return new IntentionImages()
            {
                main = ResourceLoader.LoadSprite("VentIntention.png", BepinexPlugin.embeddedSource)
            };
        }

        [EntityLogic(typeof(VentIntentionDef))]
        public sealed class VentIntention : Intention
        {
            // this enum is never used. Intention can be identified by typeof(<intentionClass>)
            public override IntentionType Type => IntentionType.Unknown;
        }
    }

    public sealed class PullIntentionDef : IntentionTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(PullIntention);
        }

        [DontOverwrite]
        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(directorySource);
            gl.DiscoverAndLoadLocFiles(this);
            return gl;
        }

        public override IntentionImages LoadSprites()
        {
            //return ResourceLoader.LoadSprite("EnergyStatus.png", BepinexPlugin.embeddedSource);
            return new IntentionImages()
            {
                main = ResourceLoader.LoadSprite("PullIntention.png", BepinexPlugin.embeddedSource)
            };
        }

        [EntityLogic(typeof(PullIntentionDef))]
        public sealed class PullIntention : Intention
        {
            // this enum is never used. Intention can be identified by typeof(<intentionClass>)
            public override IntentionType Type => IntentionType.Unknown;
        }
    }
}
