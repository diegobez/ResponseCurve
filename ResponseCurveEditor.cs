using UnityEngine;
using System.Collections;
using UnityEditor;

//////////////////////////////////////////////////////////////////////////////////////
//                                                                                  //
//    Inspector painting of the ResponseCurve component.                            //
//    Place this script under an editor folder                                      //  
//////////////////////////////////////////////////////////////////////////////////////  

[CustomEditor(typeof(ResponseCurve))]
public class ResponseCurveEditor : Editor 
{
  const float GRAPH_WIDTH  = 100;
  const float GRAPH_HEIGHT = 50;

  //////////////////////////////////////////////////////////////////////////////////////
  //                                                                                  //
  //                                                                                  //
  //                                                                                  //  
  //////////////////////////////////////////////////////////////////////////////////////  
  public override void OnInspectorGUI()
  {
    Rect currentInspacetorRect;
    ResponseCurve myTarget = (ResponseCurve)target;
    float fMinInput, fMaxInput, fMinOutput, fMaxOutput;  // mins and maxs inputs and outputs.
                                                         // Will be used to normalize and paint within boundaries.
    float   fLastStepInput = 0;
    Vector2 position, size;

    currentInspacetorRect = EditorGUILayout.BeginVertical( GUILayout.Width( 200 ), GUILayout.Height( 100 ) );

    DrawDefaultInspector ();

    fMinInput = fMinOutput =  float.MaxValue;
    fMaxInput = fMaxOutput  =   float.MinValue;
    for ( int i = 0; i < myTarget.GetCurveDefinitionSize(); i++ )
    {
      float fInput = myTarget.GetCurveDefinitionInputAt( i );
      float fOutput = myTarget.GetValue( fInput );
      if ( fMinInput  > fInput   ) fMinInput  = fInput;
      if ( fMaxInput  < fInput   ) fMaxInput  = fInput;
      if ( fMinOutput > fOutput  ) fMinOutput = fOutput;
      if ( fMaxOutput < fOutput ) fMaxOutput  = fOutput;
    }

    position = new Vector2 ( 250, 70 ) + currentInspacetorRect.position;
    size     = new Vector2 ( GRAPH_WIDTH, GRAPH_HEIGHT );

    //for each input of the responsecurve, paint a BOX of output height
    for ( int i = 0; i < myTarget.GetCurveDefinitionSize(); i++ )
    {
      float fInput  = myTarget.GetCurveDefinitionInputAt( i );
      float fOutput = myTarget.GetValue( fInput );

      fInput  /= ( fMaxInput == fMinInput   ) ? 1 : ( fMaxInput - fMinInput );   // normalize input to 0-1
      fOutput /= ( fMaxOutput == fMinOutput ) ? 1 : ( fMaxOutput - fMinOutput ); // normalize output to 0-1
      //paint
      GUI.Box( new Rect( position.x + size.x * fLastStepInput ,
                         position.y ,
                         size.x * ( fInput - fLastStepInput ),
                         -size.y * ( fOutput ) ),
                "" );
      fLastStepInput  = fInput;
    }

    EditorGUILayout.EndVertical();

  }
}