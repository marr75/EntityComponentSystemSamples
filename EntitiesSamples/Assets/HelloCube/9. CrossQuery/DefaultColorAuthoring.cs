using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelloCube.CrossQuery {
    public class DefaultColorAuthoring : MonoBehaviour {
        [FormerlySerializedAs("WhenNotColliding")]
        public Color whenNotColliding;

        class Baker : Baker<DefaultColorAuthoring> {
            public override void Bake(DefaultColorAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.None);

                var component = default(DefaultColor);
                component.Value = (Vector4)authoring.whenNotColliding;

                AddComponent(entity, component);
            }
        }
    }

    public struct DefaultColor : IComponentData {
        public float4 Value;
    }
}
