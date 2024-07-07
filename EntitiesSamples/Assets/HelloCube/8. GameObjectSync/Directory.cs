using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace HelloCube.GameObjectSync {
    // The "Directory" acts as a central place to reference GameObject prefabs and managed objects.
    // Systems can then get references to managed objects all from one place.
    // (In a large project, you may want more than one "directory" if dumping
    // all the managed objects in one place gets too unwieldy.)

    public class Directory : MonoBehaviour {
        [FormerlySerializedAs("rotatorPrefab")]
        public GameObject RotatorPrefab;

        [FormerlySerializedAs("rotationToggle")]
        public Toggle RotationToggle;
    }
}
