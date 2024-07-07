using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelloCube.StateChange {
    public class ConfigAuthoring : MonoBehaviour {
        [FormerlySerializedAs("Prefab")] public GameObject prefab;
        [FormerlySerializedAs("Size")] public uint size;
        [FormerlySerializedAs("Radius")] public float radius;
        [FormerlySerializedAs("Mode")] public Mode mode;

        class Baker : Baker<ConfigAuthoring> {
            public override void Bake(ConfigAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(
                    entity,
                    new Config {
                        Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                        Size = authoring.size,
                        Radius = authoring.radius,
                        Mode = authoring.mode,
                    }
                );
                AddComponent<Hit>(entity);
                #if UNITY_EDITOR
                AddComponent<StateChangeProfilerModule.FrameData>(entity);
                #endif
            }
        }
    }

    public struct Config : IComponentData {
        public Entity Prefab;
        public uint Size;
        public float Radius;
        public Mode Mode;
    }

    public struct Hit : IComponentData {
        public float3 Value;
        public bool HitChanged;
    }

    public struct Spin : IComponentData, IEnableableComponent {
        public bool IsSpinning;
    }

    public enum Mode { Value = 1, StructuralChange = 2, EnableableComponent = 3 }
}
