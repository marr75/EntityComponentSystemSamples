using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelloCube.CrossQuery {
    public class PrefabCollectionAuthoring : MonoBehaviour {
        [FormerlySerializedAs("Box")] public GameObject box;

        class Baker : Baker<PrefabCollectionAuthoring> {
            public override void Bake(PrefabCollectionAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.None);

                PrefabCollection component = default;
                component.Box = GetEntity(authoring.box, TransformUsageFlags.Dynamic);

                AddComponent(entity, component);
            }
        }
    }

    public struct PrefabCollection : IComponentData {
        public Entity Box;
    }
}
