using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelloCube.RandomSpawn {
    public class ConfigAuthoring : MonoBehaviour {
        [FormerlySerializedAs("Prefab")] public GameObject prefab;

        class Baker : Baker<ConfigAuthoring> {
            public override void Bake(ConfigAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Config { Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic) });
            }
        }
    }

    public struct Config : IComponentData {
        public Entity Prefab;
    }
}
