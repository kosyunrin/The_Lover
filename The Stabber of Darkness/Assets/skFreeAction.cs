using System;
using Invector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace SK
{
    [vClassHeader("Trigger Generic Action", false, iconName = "icon_ko")]
    public class skFreeAction : vMonoBehaviour
    {
        [vEditorToolbar("BaseSetting", order = 1)]
        private skStateMachine currentState = null;
        //private skStateMachine previousState = null;
        [SerializeField] Animator mAnimator=null;
        [Header("0:Nothing//1:Push")]
        [SerializeField] ActionID CurrentProcedure;




        [vEditorToolbar("Procedure_None", order = 2)]

        [Header("GenericEnvents")]
        public UnityEvent NonemEnterEnvents;
        public UnityEvent NonemExitEnvents;
        private SK.skNoneState mProcedureNone;
        [Header("ActionEnvents")]
        public UnityEvent EnterActionEnventsnone;
        public UnityEvent ExitActionEnventsnone;


        [vEditorToolbar("Procedure_Push", order = 3)]

        [Header("GenericEnvents")]
        public UnityEvent PushmEnterEnvents;
        public UnityEvent PushmExitEnvents;
        private SK.skPushState mProcedurePush;
        [Header("ActionEnvents")]
        public UnityEvent PushActionEnterEnvents;
        public UnityEvent PushActionExitEnvents;

        protected virtual void Start()
        {
            mProcedureNone = new skNoneState();
            mProcedurePush = new skPushState();
            mProcedureNone.SetUnityEvents(NonemEnterEnvents,  NonemExitEnvents);
            mProcedurePush.SetUnityEvents( PushmEnterEnvents,  PushmExitEnvents);

            mProcedureNone.SetActionEvents(EnterActionEnventsnone, ExitActionEnventsnone);
            mProcedurePush.SetActionEvents(PushActionEnterEnvents, PushActionExitEnvents);

            currentState = mProcedureNone;
        }
        protected virtual void Update()
        {
            CurrentProcedure = currentState.GetStateId;
            if (currentState == mProcedureNone) return;
            currentState.Execute(mAnimator);
        }

        public void ChangeProcedure(skStateMachine previousState)
        {
            //如果切换的状态就是本状态,就退出
            if (currentState != null && previousState.GetStateId == currentState.GetStateId)
                return;
            //退出上一个状态
            if (currentState != null)
            currentState.Exit();

            //设置进状态,进入新状态
            currentState = previousState;
            currentState.Enter();
        }
        public void ChangeProcedure(int ProcedureCode)
        {
            ActionID id = (ActionID)ProcedureCode;
            switch (id)
            {
                case ActionID.nothing:
                    ChangeProcedure(mProcedureNone);
                    break;
                case ActionID.pushing:
                    ChangeProcedure(mProcedurePush);
                    break;
            }
        }


        [System.Serializable]
        public class OnUpdateValue : UnityEvent<float>
        {

        }
    }

   

}

//private skStateMachine currentState = null;
//private skStateMachine previousState = null;
//private bool isStop;
//[vEditorToolbar("Procedure_None", order = 1)]

//public UnityEvent NonemEnterEnvents;
//public UnityEvent NonemExitEnvents;


//[vEditorToolbar("Procedure_Push", order = 2)]
//public UnityEvent PushmEnterEnvents;
//public UnityEvent PushmExitEnvents;



//[System.Serializable]
//public class OnUpdateValue : UnityEvent<float>
//{

//}