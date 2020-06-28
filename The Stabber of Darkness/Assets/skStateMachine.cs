using System.Runtime.Remoting.Messaging;
using UnityEngine.Events;
using UnityEngine;
namespace SK
{
    public enum ActionID
    {
        nothing,
        pushing
    };
    public abstract class skStateMachine
    {
        protected UnityEvent mEnterEnvents;
        protected UnityEvent mExitEnvents;
        protected UnityEvent mStartActionEvents;
        protected UnityEvent mExitActionEvents;
        protected ActionID ID;
        abstract public  void SetUnityEvents( UnityEvent Entervar,  UnityEvent Exitvar);
        abstract public void SetActionEvents(UnityEvent Entervar, UnityEvent Exitvar);
        abstract public ActionID GetStateId { get; }
        abstract public void Enter();
        abstract public void Execute(Animator anim);
        abstract public void Exit();
    }
    //public class skStateMachineManager
    //{
    //    private skStateMachine currentState = null;
    //    private skStateMachine previousState = null;
    //    private bool isStop;

    //    public skStateMachine CurrentState
    //    {
    //        get
    //        {
    //            return currentState;
    //        }
    //    }
    //    public skStateMachine PreviousState
    //    {
    //        get
    //        {
    //            return previousState;
    //        }
    //    }
    //    public bool IsStop
    //    {
    //        get
    //        {
    //            return isStop;
    //        }

    //        set
    //        {
    //            isStop = value;
    //        }
    //    }
    //}
    public class skPushState : skStateMachine
    {
        const ActionID push = ActionID.pushing;
        bool pushingpose = false;
        public override ActionID GetStateId
        {
            get
            {
                return push;
            }
        }

        public override void Enter()
        {
            if (mEnterEnvents != null)
                mEnterEnvents.Invoke();
        }

        public override void Execute(Animator anim)
        {
            pushingpose = false;

            if (Input.GetKey(KeyCode.E))
            {
                anim.SetBool("IsPushPose", true);
                anim.SetBool("IsPushStop", false);
                pushingpose = true;
            }
            if (!pushingpose)
            {
                anim.SetBool("IsPushing", false);
                anim.SetBool("IsPulling", false);
                anim.SetBool("IsPushPose", false);
                anim.SetBool("IsPushStop", true);
                if(mExitActionEvents!=null)
                mExitActionEvents.Invoke();
            }

            if (pushingpose)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetBool("IsPushPose", false);
                    anim.SetBool("IsPulling", false);
                    anim.SetBool("IsPushing", true);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetBool("IsPushPose", false);
                    anim.SetBool("IsPushing", false);
                    anim.SetBool("IsPulling", true);
                }
              
            }
        }

        public override void Exit()
        {
            if (mExitEnvents != null)
                mExitEnvents.Invoke();
        }

        public override void SetActionEvents(UnityEvent Entervar, UnityEvent Exitvar)
        {
            mStartActionEvents = Entervar;
            mExitActionEvents = Exitvar;
        }

        public override void SetUnityEvents( UnityEvent Entervar,  UnityEvent Exitvar)
        {
            mEnterEnvents = Entervar;
            mExitEnvents = Exitvar;
        }
    }
    public class skNoneState : skStateMachine
    {
        const ActionID none = ActionID.nothing;
        
        public override ActionID GetStateId
        {
            get
            {
                return none;
            }
        }

        public override void Enter()
        {
            if (mEnterEnvents != null)
                mEnterEnvents.Invoke();
        }

        public override void Execute(Animator anim)
        {
        }

        public override void Exit()
        {
            if(mExitEnvents!=null)
            mExitEnvents.Invoke();
        }

        public override void SetActionEvents(UnityEvent Entervar, UnityEvent Exitvar)
        {
            mStartActionEvents = Entervar;
            mExitActionEvents = Exitvar;
        }

        public override void  SetUnityEvents( UnityEvent Entervar,  UnityEvent Exitvar)
        {
            mEnterEnvents = Entervar;
            mExitEnvents = Exitvar;
        }
    }
}
