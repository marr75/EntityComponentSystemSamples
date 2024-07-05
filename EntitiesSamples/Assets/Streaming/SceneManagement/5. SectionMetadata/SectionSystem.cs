using Streaming.SceneManagement.Common;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Scenes;
using Unity.Transforms;
using UnityEngine;

namespace Streaming.SceneManagement.SectionMetadata {
    // Loads and unloads each sections as the relevant entities enter and leave the circles.
    partial struct SectionSystem : ISystem {
        [BurstCompile]
        public void OnCreate(ref SystemState state) { state.RequireForUpdate<Circle>(); }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var toLoad = new NativeHashSet<Entity>(1, Allocator.Temp);

            var sectionQuery = SystemAPI.QueryBuilder().WithAll<Circle, SceneSectionData>().Build();
            var sectionEntities = sectionQuery.ToEntityArray(Allocator.Temp);
            var circles = sectionQuery.ToComponentDataArray<Circle>(Allocator.Temp);

            // Find all the sections that should be loaded based on the distances to the sphere
            foreach (var transform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Relevant>()) {
                for (var index = 0; index < circles.Length; ++index) {
                    var distance = transform.ValueRO.Position - circles[index].Center;
                    distance.y = 0;
                    var radiusSq = circles[index].Radius;
                    var debugColor = new Color(1f, 0f, 0f);
                    if (math.lengthsq(distance) < radiusSq * radiusSq) {
                        toLoad.Add(sectionEntities[index]);
                        debugColor = new Color(0f, 0.5f, 0f);
                    }

                    DrawCircleXZ(circles[index].Center + new float3(0f, 0.2f, 0f), circles[index].Radius, debugColor);
                }
            }

            foreach (var sectionEntity in sectionEntities) {
                var sectionState = SceneSystem.GetSectionStreamingState(state.WorldUnmanaged, sectionEntity);
                if (toLoad.Contains(sectionEntity)) {
                    if (sectionState == SceneSystem.SectionStreamingState.Unloaded) {
                        // Load the section
                        state.EntityManager.AddComponent<RequestSceneLoaded>(sectionEntity);
                    }
                }
                else {
                    if (sectionState != SceneSystem.SectionStreamingState.Unloaded) {
                        // Unload the section
                        state.EntityManager.RemoveComponent<RequestSceneLoaded>(sectionEntity);
                    }
                }
            }
        }

        public static void DrawCircleXZ(float3 position, float radius, Color color, float divisions = 8f) {
            var angle = 0f;
            var step = math.PI / divisions;
            var PI2 = math.PI * 2f;
            while (angle < PI2) {
                var begin = new float3(math.sin(angle), 0f, math.cos(angle)) * radius + position;
                angle += step;
                var end = new float3(math.sin(angle), 0f, math.cos(angle)) * radius + position;
                Debug.DrawLine(begin, end, color);
            }
        }
    }
}
