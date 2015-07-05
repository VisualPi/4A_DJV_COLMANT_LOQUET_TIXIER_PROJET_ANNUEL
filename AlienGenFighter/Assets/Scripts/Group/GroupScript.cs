using Assets.Scripts.Context;
using UnityEngine;

namespace Assets.Scripts.Group
{
    public class GroupScript : MonoBehaviour
    {
        public GroupContext GroupContext;
        public Transform Transform;
        public GameObject GameObject;
        public SphereCollider Collider;

        public float TimeEllapsed;
        private bool _isStarted;

        public void Start()
        {
            GroupContext = new GroupContext(this);
            TimeEllapsed = 0f;
            //GroupContext.Group = this;//
        }

        public void OnMouseDown()
        {
            Debug.Log("Collider size is now : " + Collider.transform.localScale);
            Debug.Log("Member nb : " + GroupContext.Entities.Count);
        }

        public void Update()
        {
            if (_isStarted)
                TimeEllapsed += Time.deltaTime;
        }

        public void StartTimer()
        {
            _isStarted = true;
        }

        public void StopTimer()
        {
            _isStarted = false;
        }

        public bool EnabledCollision
        {
            get { return Collider.enabled; }
            set { Collider.enabled = value; }
        }
    }
}
