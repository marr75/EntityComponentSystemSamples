using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;

namespace HelloCube.StateChange {
    public partial struct CubeSpawnSystem : ISystem {
        Config _priorConfig;

        [BurstCompile]
        public void OnCreate(ref SystemState state) {
            state.RequireForUpdate<Config>();
            state.RequireForUpdate<ExecuteStateChange>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var config = SystemAPI.GetSingleton<Config>();

            if (ConfigEquals(_priorConfig, config)) { return; }
            _priorConfig = config;

            var query = SystemAPI.QueryBuilder().WithAll<URPMaterialPropertyBaseColor>().Build();
            state.EntityManager.DestroyEntity(query);

            var entities = state.EntityManager.Instantiate(config.Prefab, (int)(config.Size * config.Size), Allocator.Temp);

            var center = (config.Size - 1) / 2f;
            var i = 0;
            foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>()) {
                transform.ValueRW.Scale = 1;
                transform.ValueRW.Position.x = (i % config.Size - center) * 1.5f;
                transform.ValueRW.Position.z = (i / config.Size - center) * 1.5f;
                i++;
            }

            var spinQuery = SystemAPI.QueryBuilder().WithAll<Spin>().Build();

            if (config.Mode == Mode.Value) { state.EntityManager.AddComponent<Spin>(query); }
            else if (config.Mode == Mode.EnableableComponent) {
                state.EntityManager.AddComponent<Spin>(query);
                state.EntityManager.SetComponentEnabled<Spin>(spinQuery, false);
            }
        }

        bool ConfigEquals(Config c1, Config c2) => c1.Size == c2.Size && c1.Radius == c2.Radius && c1.Mode == c2.Mode;
    }
}
