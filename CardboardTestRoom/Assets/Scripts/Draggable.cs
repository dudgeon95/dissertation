﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

    [RequireComponent(typeof(VRInteractiveItem))]
    public class Draggable : MonoBehaviour
    {
        //Distance from the camera when dragging
        public float cameraDistance = 1;

        //Max distance for grabbing
        public float maxGrabDistance = 2;

        //Event for when the user starts to drag the object
        public event Action OnDrag;

        //Event for when the user drops the object
        public event Action OnDrop;


        //VR interactive item component
        VRInteractiveItem vrInteractive;

        //States
        enum State { Ready, Dragging, Blocked}

        State currState;

        //Keep track if something is already being dragged
        static bool isDragging = false;

       

      
        private void Awake()
        {
        
            vrInteractive = GetComponent<VRInteractiveItem>();

            currState = State.Ready;

            
        }

        public void OnEnable()
        {
            vrInteractive.OnClick += HandleClick;
        }

        void OnDisable()
        {
            vrInteractive.OnClick -= HandleClick;
        }

        

        public void HandleClick()
        {
            //if its ready and we are not already dragging an object we start to drag the object
            if (currState == State.Ready && !Draggable.isDragging)
            {
                //Check to object it within reach
                float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
                //If it is too far return
                if (dist > maxGrabDistance) return;

                //Set the state of the object to dragging
                currState = State.Dragging;

                

                // Set the isDragging flag to true
                Draggable.isDragging = true;

                //Excute the event
                if (OnDrag != null)
                {
                    OnDrag();
                }

                
            }
            //If its dragging set the state to ready i.e releasing the object
            else if (currState == State.Dragging) {
                currState = State.Ready;

                

                // Set the isDragging flag to false
                Draggable.isDragging = false;

                //Excute the event
                if (OnDrop != null)
                {
                    OnDrop();
                }
            }
        }

        private void Update()
        {
            if (currState == State.Dragging) {
                Transform camTrans = Camera.main.transform;

                transform.position = camTrans.position + camTrans.forward * cameraDistance;

                transform.LookAt(camTrans.position);
            }
        }

       

        // Block a draggable item
        public void SetBlockState(bool isblock)
        {
            if (isblock)
            {
                currState = State.Blocked;
            }
            else
            {
                currState = State.Ready;
            }
        }

    }

