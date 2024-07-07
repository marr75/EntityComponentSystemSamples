using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace HelloCube.EnableableComponents {
    public class RotationSpeedAuthoring : MonoBehaviour {
        public bool startEnabled;
        public float degreesPerSecond = 360.0f;
        public float interval = 1.3f;

        public class Baker : Baker<RotationSpeedAuthoring> {
            public override void Bake(RotationSpeedAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(
                    entity,
                    new RotationSpeed {
                        radiansPerSecond = math.radians(authoring.degreesPerSecond),
                        interval = authoring.interval,
                        timeRemaining = authoring.interval,
                    }
                );
                SetComponentEnabled<RotationSpeed>(entity, authoring.startEnabled);
            }
        }
    }

    struct RotationSpeed : IComponentData, IEnableableComponent {
        public float radiansPerSecond;
        public float interval;
        public float timeRemaining;
    }
}
