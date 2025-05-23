using UnityEngine;

namespace Class
{
    [RequireComponent (typeof (Rigidbody))]
    public abstract class Grabbable : PropsBase
    {
        private Rigidbody rigid = null;
        private Desk desk = null;
        private Lectern lectern = null;
        private PlayerController controller = null;

        public Desk TheDeskBelow { get => desk; }
        public Lectern TheLecternBelow { get => lectern; }
        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            controller = GameObject.Find(Constants.NAME_PLAYER).GetComponent<PlayerController>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            rigid.velocity = Vector3.zero;
        }

        public void GrabObject()
        {
            
            if (controller.IsGrabbing)
            {
                return;
            }
            SoundManager.Instance.CreateAudioSource(controller.transform.position, SfxClipTypes.Grab_Object, 1.0f);
            // 만약 물건이 책상 아래에 놓여져 있었다면, 책상 위 목록에서 grabbable을 삭제.
            if (desk != null)
            {
                TheDeskBelow.props.Remove(this.PropType);
                desk = null;
            }
            else if (lectern != null)
            {
                lectern.Grabbable = null;
                lectern = null;
            }

            controller.InteractableGrabbing = this;
            controller.InteractableGrabbing.GetComponent<BoxCollider>().isTrigger = true;
            controller.IsGrabbing = true;
        }

        public void ReleaseObject()
        {
            SoundManager.Instance.CreateAudioSource(controller.transform.position, SfxClipTypes.Release_Object, 1.0f);

            Vector3 releasePosion = controller.CameraTransform.position;
            controller.InteractableGrabbing.GetComponent<BoxCollider>().isTrigger = false;

            if (controller.InteractableGrabbing == null || !controller.IsGrabbing)
            {
                return;
            }
            if (controller.RecentlyDetectedProp is Desk desk)
            {
                this.desk = desk;
                this.desk.props.Add(this.PropType);
                releasePosion = controller.RaycastHitPosition + Vector3.up * 0.8f;
            }
            else if (controller.RecentlyDetectedProp is Lectern lectern)
            {
                this.lectern = lectern;
                lectern.Grabbable = this;
                releasePosion = controller.RaycastHitPosition + Vector3.up * 1f;
            }
            else
            {
                releasePosion = controller.InteractableGrabbing.transform.position + Vector3.forward * 0.2f;
            }

            

            controller.InteractableGrabbing.transform.position = releasePosion;
            controller.InteractableGrabbing = null;

            Invoke("DelaySetFlag", 0.3f);
        }

        public void DelaySetFlag()
        {
            controller.IsGrabbing = false;
        }


    }
}
