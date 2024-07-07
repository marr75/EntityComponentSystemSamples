using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace HelloCube.EnableableComponents {
    public partial struct RotationSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { state.RequireForUpdate<ExecuteEnableableComponents>(); }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;

            // Toggle the enabled state of every RotationSpeed
            foreach (var (enabled, speed) in SystemAPI.Query<EnabledRefRW<RotationSpeed>, RefRW<RotationSpeed>>()
                .WithOptions(EntityQueryOptions.IgnoreComponentEnabledState)) {
                speed.ValueRW.TimeRemaining -= deltaTime;
                if (!(speed.ValueRW.TimeRemaining <= 0.0f)) { continue; }
                enabled.ValueRW = !enabled.ValueRW;
                speed.ValueRW.TimeRemaining = speed.ValueRW.Interval;
            }

            // The query only matches entities whose RotationSpeed is enabled.
            foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>()) {
                transform.ValueRW = transform.ValueRW.RotateY(speed.ValueRO.RadiansPerSecond * deltaTime);
            }
        }
    }
}
