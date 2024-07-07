using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelloCube.Prefabs {
    // An authoring component is just a normal MonoBehavior that has a Baker<T> class.
    public class SpawnerAuthoring : MonoBehaviour {
        [FormerlySerializedAs("Prefab")] public GameObject prefab;
        [FormerlySerializedAs("Count")] public int count = 100;

        // In baking, this Baker will run once for every SpawnerAuthoring instance in a subscene.
        // (Note that nesting an authoring component's Baker class inside the authoring MonoBehaviour class
        // is simply an optional matter of style.)
        class Baker : Baker<SpawnerAuthoring> {
            public override void Bake(SpawnerAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(
                    entity,
                    new Spawner { Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic), Count = authoring.count }
                );
            }
        }
    }

    struct Spawner : IComponentData {
        public Entity Prefab;
        public int Count;
    }
}
