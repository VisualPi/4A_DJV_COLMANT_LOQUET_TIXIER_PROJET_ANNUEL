using System;
using Assets.Scripts.Context;
using Assets.Scripts.GUI;
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
        public float TimeBorn;
        private bool _isStarted;
        private bool created = false;
        public void Init()
        {
            GroupContext = new GroupContext(this);
            TimeEllapsed = 0f;
            created = true;
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

            if ( created && GroupContext.Entities.Count > 5)
            {
                TimeBorn += Time.deltaTime;
                if (TimeBorn >= 30)
                {
                    var en = EntityManagerScript.GetFromQueue();
                    en.Init();
                    foreach (var e in GroupContext.Entities)
                    {
                        en.DNA.Mutate(e.DNA);
                    }
                    en.DNA.SetGeneAt(ECharateristic.Skincolor, GroupContext.Leader.DNA.GetGeneAt(ECharateristic.Skincolor));
                    en.InitFromDna();
                    en.IsInGroup = true;
                    GroupContext.AddEntity(en);
                    Debug.Log(en.DNA.ToString());
                    en.Transform.position = new Vector3(GroupContext.Leader.transform.position.x + 10, GroupContext.Leader.transform.position.y, GroupContext.Leader.transform.position.z + 5);
                    GameEventMessage.AddEventMessage(EIconEventMessage.Baby, en.name +" is born !", "Naissance d'une entité", DateTime.Now);
                    TimeBorn = 0;
                }
            }
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
