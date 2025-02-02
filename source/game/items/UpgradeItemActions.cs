using System;
using Celesteia.Game.Components;
using Celesteia.Game.Worlds;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace Celesteia.Game {
    public class UpgradeItemActions : ItemActions {
        private float _increase;
        private EntityAttribute _attribute;
        private float _max;

        public UpgradeItemActions(float increase, EntityAttribute attr, float max) {
            UseTime = 0.2;
            _increase = increase;
            _attribute = attr;
            _max = max;
        }
        
        public override bool OnLeftClick(GameTime gameTime, GameWorld world, Vector2 cursor, Entity user) {
            return Check(gameTime, user) && Use(user);
        }

        // Check if the conditions to use this item's action are met.
        public bool Check(GameTime gameTime, Entity user) {
            if (!CheckUseTime(gameTime)) return false;

            // If the user has no attributes, the rest of the function will not work, so check if they're there first.
            if (!user.Has<EntityAttributes>()) return false;

            EntityAttributes.EntityAttributeMap attributes = user.Get<EntityAttributes>().Attributes;

            // If the attribute is maxed out, don't upgrade.
            if (attributes.Get(_attribute) >= _max) return false;

            UpdateLastUse(gameTime);
            
            return true;
        }


        public bool Use(Entity user) {
            // Use the upgrade.
            EntityAttributes.EntityAttributeMap attributes = user.Get<EntityAttributes>().Attributes;
            attributes.Set(_attribute, Math.Clamp(attributes.Get(_attribute) + _increase, 0f, _max));

            return true;
        }
    }
}