using System;
using System.Collections.Generic;
using System.Diagnostics;
using Celesteia.Game.ECS;
using Celesteia.Game.Components;
using Celesteia.Game.Components.Player;
using Celesteia.Game.Input;
using Celesteia.Resources.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.TextureAtlases;
using Celesteia.Game.Components.Physics;
using Celesteia.Game.Components.Items;

namespace Celesteia.Resources.Collections {
    public class EntityTypes {
        public EntityType PLAYER;

        public List<EntityType> Types;

        public void LoadContent(ContentManager Content) {
            Debug.WriteLine($"Loading entity types...");

            Types = new List<EntityType>();

            Types.Add(PLAYER = new EntityType(0, "Player",
                (entity) => {
                    entity.Attach(new Transform2());

                    entity.Attach(new TargetPosition());

                    entity.Attach(new EntityFrames(
                        TextureAtlas.Create("player", Content.Load<Texture2D>("sprites/entities/player/astronaut"), 24, 24),
                        0, 1,
                        ResourceManager.SPRITE_SCALING
                    ));

                    entity.Attach(new EntityInventory(new ItemStack(8, 1), new ItemStack(0, 10)));

                    entity.Attach(new PhysicsEntity(1f, true));

                    entity.Attach(new CollisionBox(1.5f, 3f));

                    entity.Attach(new PlayerInput()
                        .AddHorizontal(new KeyDefinition(Keys.A, Keys.D, KeyDetectType.Held))
                        .AddVertical(new KeyDefinition(Keys.W, Keys.S, KeyDetectType.Held))
                        .AddRun(new KeyDefinition(null, Keys.LeftShift, KeyDetectType.Held))
                        .AddJump(new KeyDefinition(null, Keys.Space, KeyDetectType.Held))
                        .AddOpenInventory(new KeyDefinition(null, Keys.B, KeyDetectType.Down))
                        .AddOpenCrafting(new KeyDefinition(null, Keys.C, KeyDetectType.Down))
                    );

                    entity.Attach(new LocalPlayer());

                    entity.Attach(new CameraFollow());

                    entity.Attach(new EntityAttributes(new EntityAttributes.EntityAttributeMap()
                        .Set(EntityAttribute.MovementSpeed, 5f)
                        .Set(EntityAttribute.JumpForce, 10f)
                        .Set(EntityAttribute.BlockRange, 7f)
                    ));
                }
            ));
            
            Debug.WriteLine("Entities loaded.");
        }
    }

    public struct EntityType {
        public readonly byte EntityID;
        public readonly string EntityName;
        private readonly Action<Entity> InstantiateAction;

        public EntityType(byte id, string name, Action<Entity> instantiate) {
            EntityID = id;
            EntityName = name;
            InstantiateAction = instantiate;

            Debug.WriteLine($"  Entity '{name}' loaded.");
        }

        public void Instantiate(Entity entity) {
            InstantiateAction.Invoke(entity);
        }
    }
}