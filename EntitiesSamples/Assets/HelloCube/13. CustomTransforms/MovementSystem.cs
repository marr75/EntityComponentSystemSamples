using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace HelloCube.CustomTransforms {
    public partial struct MovementSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { state.RequireForUpdate<LocalTransform2D>(); }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var rotation = SystemAPI.Time.DeltaTime * 180f; // Half a rotation every second (in degrees)
            var elapsedTime = (float)SystemAPI.Time.ElapsedTime;
            var xPosition = math.sin(elapsedTime) * 2f - 1f;
            var scale = math.sin(elapsedTime * 2f) + 1f;
            scale = scale <= 0.001f ? 0f : scale;

            foreach (var localTransform2D in SystemAPI.Query<RefRW<LocalTransform2D>>().WithNone<Parent>()) {
                localTransform2D.ValueRW.Position.x = xPosition;
                localTransform2D.ValueRW.Rotation = localTransform2D.ValueRO.Rotation + rotation;
                localTransform2D.ValueRW.Scale = scale;
            }
        }
    }
}
