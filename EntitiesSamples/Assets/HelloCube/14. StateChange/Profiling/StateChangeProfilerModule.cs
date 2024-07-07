#if UNITY_EDITOR
using Unity.Entities;
using Unity.Profiling;
using Unity.Profiling.Editor;

namespace HelloCube.StateChange {
    [ProfilerModuleMetadata("StateChange")]
    public class StateChangeProfilerModule : ProfilerModule {
        public struct FrameData : IComponentData {
            public long SpinPerf;
            public long SetStatePerf;
        }

        static readonly string SSpinPerfCounterLabel = "Spin System";
        static readonly string SSetStatePerfCounterLabel = "SetState System";

        static readonly ProfilerCounterValue<long> SSpinPerfCounterValue = new(
            ProfilerCategory.Scripts,
            SSpinPerfCounterLabel,
            ProfilerMarkerDataUnit.TimeNanoseconds,
            ProfilerCounterOptions.FlushOnEndOfFrame
        );

        static readonly ProfilerCounterValue<long> SSetStatePerfCounterValue = new(
            ProfilerCategory.Scripts,
            SSetStatePerfCounterLabel,
            ProfilerMarkerDataUnit.TimeNanoseconds,
            ProfilerCounterOptions.FlushOnEndOfFrame
        );

        static readonly ProfilerCounterDescriptor[] KChartCounters = {
            new(SSpinPerfCounterLabel, ProfilerCategory.Scripts), new(SSetStatePerfCounterLabel, ProfilerCategory.Scripts),
        };

        internal static long SpinPerf { set => SSpinPerfCounterValue.Value = value; }

        internal static long UpdatePerf { set => SSetStatePerfCounterValue.Value = value; }

        public StateChangeProfilerModule() : base(KChartCounters, ProfilerModuleChartType.StackedTimeArea) { }
    }
}

#endif
