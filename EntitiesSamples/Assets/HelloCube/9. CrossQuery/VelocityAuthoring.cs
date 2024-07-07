using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelloCube.CrossQuery {
    public class VelocityAuthoring : MonoBehaviour {
        [FormerlySerializedAs("Value")] public Vector3 value;

        class Baker : Baker<VelocityAuthoring> {
            public override void Bake(VelocityAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.None);

                var component = default(Velocity);
                component.Value = authoring.value;

                AddComponent(entity, component);
            }
        }
    }

    public struct Velocity : IComponentData {
        public float3 Value;
    }
}
