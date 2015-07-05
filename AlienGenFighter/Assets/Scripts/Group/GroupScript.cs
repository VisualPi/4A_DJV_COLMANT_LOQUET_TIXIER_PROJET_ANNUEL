using Assets.Scripts.Context;
using UnityEngine;

namespace Assets.Scripts.Group {
    public class GroupScript : MonoBehaviour {
        public GroupContext     GroupContext;
        public Transform        Transform;
        public GameObject       GameObject;
        public SphereCollider   Collider;

        public void Start() {
            GroupContext = new GroupContext(this);
            //GroupContext.Group = this;//
        }

        public bool EnabledCollision {
            get { return Collider.enabled; }
            set { Collider.enabled = value; }
        }

        public void OnMouseDown() {
            Debug.Log("Collider size is now : " + Collider.transform.localScale);
            Debug.Log("Member nb : " + GroupContext.Entities.Count);
        }

    }
}
