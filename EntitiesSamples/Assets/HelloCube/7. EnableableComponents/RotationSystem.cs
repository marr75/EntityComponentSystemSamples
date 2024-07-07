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
                speed.ValueRW.timeRemaining -= deltaTime;
                if (!(speed.ValueRW.timeRemaining <= 0.0f)) { continue; }
                enabled.ValueRW = !enabled.ValueRW;
                speed.ValueRW.timeRemaining = speed.ValueRW.interval;
            }

            // The query only matches entities whose RotationSpeed is enabled.
            foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>()) {
                transform.ValueRW = transform.ValueRW.RotateY(speed.ValueRO.radiansPerSecond * deltaTime);
            }
        }
    }
}
