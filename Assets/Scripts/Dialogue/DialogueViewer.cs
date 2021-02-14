﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueObject;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;

public class DialogueViewer : MonoBehaviour {
    [SerializeField] Transform parentOfResponses = null;
    [SerializeField] Button prefab_btnResponse = null;
    [SerializeField] UnityEngine.UI.Text txtNodeDisplay = null;
    [SerializeField] DialogueController dialogueController = null;
    DialogueController controller;

    [DllImport( "__Internal" )]
    private static extern void openPage( string url );

    private void Start() {
        controller = dialogueController;
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue();
        
        MarkerManagerScript.S.Reset();
    }

    public static void KillAllChildren( UnityEngine.Transform parent ) {
        UnityEngine.Assertions.Assert.IsNotNull( parent );
        for ( int childIndex = parent.childCount - 1; childIndex >= 0; childIndex-- ) {
            UnityEngine.Object.Destroy( parent.GetChild( childIndex ).gameObject );
        }
    }

    private void OnNodeSelected( int indexChosen ) {
        Debug.Log( "Chose: " + indexChosen );
        controller.ChooseResponse( indexChosen );
    }

    private void OnNodeEntered( Node newNode ) {
        txtNodeDisplay.text = newNode.text;
        
        KillAllChildren( parentOfResponses );
        for ( int i = newNode.responses.Count-1; i >= 0; i-- ) 
        {
            int currentChoiceIndex = i;
            var response = newNode.responses[i];
            var responceButton = Instantiate( prefab_btnResponse, parentOfResponses );
            responceButton.GetComponentInChildren<UnityEngine.UI.Text>().text = response.displayText;
            responceButton.onClick.AddListener( delegate { OnNodeSelected( currentChoiceIndex ); } );
        }
    }
}