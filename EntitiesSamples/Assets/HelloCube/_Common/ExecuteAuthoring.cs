using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace HelloCube {
    public class ExecuteAuthoring : MonoBehaviour {
        [FormerlySerializedAs("MainThread")] public bool mainThread;
        [FormerlySerializedAs("IJobEntity")] public bool jobEntity;
        [FormerlySerializedAs("Aspects")] public bool aspects;
        [FormerlySerializedAs("Prefabs")] public bool prefabs;
        [FormerlySerializedAs("IJobChunk")] public bool jobChunk;
        [FormerlySerializedAs("Reparenting")] public bool reparenting;

        [FormerlySerializedAs("EnableableComponents")]
        public bool enableableComponents;

        [FormerlySerializedAs("GameObjectSync")]
        public bool gameObjectSync;

        [FormerlySerializedAs("CrossQuery")] public bool crossQuery;
        [FormerlySerializedAs("RandomSpawn")] public bool randomSpawn;

        [FormerlySerializedAs("FirstPersonController")]
        public bool firstPersonController;

        [FormerlySerializedAs("FixedTimestep")]
        public bool fixedTimestep;

        [FormerlySerializedAs("StateChange")] public bool stateChange;

        [FormerlySerializedAs("ClosestTarget")]
        public bool closestTarget;

        class Baker : Baker<ExecuteAuthoring> {
            public override void Bake(ExecuteAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.None);

                if (authoring.mainThread) { AddComponent<ExecuteMainThread>(entity); }
                if (authoring.jobEntity) { AddComponent<ExecuteIJobEntity>(entity); }
                if (authoring.aspects) { AddComponent<ExecuteAspects>(entity); }
                if (authoring.prefabs) { AddComponent<ExecutePrefabs>(entity); }
                if (authoring.jobChunk) { AddComponent<ExecuteIJobChunk>(entity); }
                if (authoring.gameObjectSync) { AddComponent<ExecuteGameObjectSync>(entity); }
                if (authoring.reparenting) { AddComponent<ExecuteReparenting>(entity); }
                if (authoring.enableableComponents) { AddComponent<ExecuteEnableableComponents>(entity); }
                if (authoring.crossQuery) { AddComponent<ExecuteCrossQuery>(entity); }
                if (authoring.randomSpawn) { AddComponent<ExecuteRandomSpawn>(entity); }
                if (authoring.firstPersonController) { AddComponent<ExecuteFirstPersonController>(entity); }
                if (authoring.fixedTimestep) { AddComponent<ExecuteFixedTimestep>(entity); }
                if (authoring.stateChange) { AddComponent<ExecuteStateChange>(entity); }
                if (authoring.closestTarget) { AddComponent<ExecuteClosestTarget>(entity); }
            }
        }
    }

    public struct ExecuteMainThread : IComponentData { }

    public struct ExecuteIJobEntity : IComponentData { }

    public struct ExecuteAspects : IComponentData { }

    public struct ExecutePrefabs : IComponentData { }

    public struct ExecuteIJobChunk : IComponentData { }

    public struct ExecuteGameObjectSync : IComponentData { }

    public struct ExecuteReparenting : IComponentData { }

    public struct ExecuteEnableableComponents : IComponentData { }

    public struct ExecuteCrossQuery : IComponentData { }

    public struct ExecuteRandomSpawn : IComponentData { }

    public struct ExecuteFirstPersonController : IComponentData { }

    public struct ExecuteFixedTimestep : IComponentData { }

    public struct ExecuteStateChange : IComponentData { }

    public struct ExecuteClosestTarget : IComponentData { }
}
