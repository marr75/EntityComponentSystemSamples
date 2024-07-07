using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace HelloCube.Reparenting {
    public partial struct ReparentingSystem : ISystem {
        bool attached;
        float timer;
        const float interval = 0.7f;
        EntityQuery query;

        public void OnCreate(ref SystemState state) {
            timer = interval;
            attached = true;
            state.RequireForUpdate<ExecuteReparenting>();
            state.RequireForUpdate<RotationSpeed>();
            query = SystemAPI.QueryBuilder().WithAll<LocalTransform>().WithNone<RotationSpeed>().Build();
        }

        public void OnUpdate(ref SystemState state) {
            timer -= SystemAPI.Time.DeltaTime;
            if (timer > 0) { return; }
            timer = interval;

            var rotatorEntity = SystemAPI.GetSingletonEntity<RotationSpeed>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            if (attached) {
                // Detach all children from the rotator by removing the Parent component from the children.
                // (The next time TransformSystemGroup updates, it will update the Child buffer and transforms accordingly.)
                var children = SystemAPI.GetBuffer<Child>(rotatorEntity);
                // A single call that removes the Parent component from all entities in the array.
                // Because the method expects a NativeArray<Entity>, we create a NativeArray<Entity> alias of the DynamicBuffer.
                ecb.RemoveComponent<Parent>(children.AsNativeArray().Reinterpret<Entity>());
            }
            else {
                // Attach all the small cubes to the rotator by adding a Parent component to the cubes.
                // (The next time TransformSystemGroup updates, it will update the Child buffer and transforms accordingly.)
                // Add a Parent value to all entities matching a query.
                ecb.AddComponent(query, new Parent { Value = rotatorEntity });
            }

            ecb.Playback(state.EntityManager);
            attached = !attached;
        }
    }
}
