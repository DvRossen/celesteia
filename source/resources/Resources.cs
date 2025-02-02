using Celesteia.Resources.Collections;
using Microsoft.Xna.Framework.Content;

namespace Celesteia.Resources {
    public static class ResourceManager {
        public static ItemTypes Items = new ItemTypes();
        public static BlockTypes Blocks = new BlockTypes();
        public static CraftingRecipes Recipes = new CraftingRecipes();
        public static EntityTypes Entities = new EntityTypes();
        public static FontTypes Fonts = new FontTypes();
        public static SkyboxAssets Skybox = new SkyboxAssets();

        public const float SPRITE_SCALING = 0.125f;
        public const float INVERSE_SPRITE_SCALING = 8f;

        public static void LoadContent(ContentManager content) {
            Items.LoadContent(content);
            Blocks.LoadContent(content);
            Recipes.LoadContent(content);
            Entities.LoadContent(content);
            Fonts.LoadContent(content);
            Skybox.LoadContent(content);
        }
    }

    public struct NamespacedKey {
        public readonly string Namespace;
        public readonly string Key;

        public NamespacedKey(string ns, string key) {
            Namespace = ns;
            Key = key;
        }

        public static NamespacedKey Base(string key) {
            return new NamespacedKey("celesteia", key);
        }

        public string Qualify() {
            return $"{Namespace}:{Key}";
        }
    }

    public interface IResourceType {
        public byte GetID();
    }

    public interface IResourceCollection {
        public IResourceType GetResource(NamespacedKey namespacedKey);
    }
}