using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace HelloCube {
    // An authoring component is just a normal MonoBehavior.
    public class RotationSpeedAuthoring : MonoBehaviour {
        [FormerlySerializedAs("DegreesPerSecond")]
        public float degreesPerSecond = 360.0f;

        // In baking, this Baker will run once for every RotationSpeedAuthoring instance in an entity subscene.
        // (Nesting an authoring component's Baker class is simply an optional matter of style.)
        class Baker : Baker<RotationSpeedAuthoring> {
            public override void Bake(RotationSpeedAuthoring authoring) {
                // The entity will be moved
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotationSpeed { RadiansPerSecond = math.radians(authoring.degreesPerSecond) });
            }
        }
    }

    public struct RotationSpeed : IComponentData {
        public float RadiansPerSecond;
    }
}
