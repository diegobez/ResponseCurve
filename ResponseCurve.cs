using UnityEngine;
using System.Collections;
using System;

/*/////////////////////////////////////////////////////////////////////////////////////
                                                                                 
   This component implements a response curve easily configurable at the unity inspector.
   First, set inputs and outputs to define a response curve that could be visualized as
   a typical 2 axis graph. 
   Then, you can ask for the interpolated output ( second axis value ) given an input 
   ( first axis value ).

   First step : Add the component to a Game Object, and set the m_curveDefinition 
   inputs and outputs at the inspector.
   The inputs must be sortered in ascendant order : 0, 2, 6, 7 ...
   Instead of using the inspector, you can use the Initialize() function from a script.

   Second step : ask for the curve output from any given input : GetValue( input ) .
    
   You can read some exampleas and explanations at :
   http://www.gamedev.net/page/resources/_/technical/general-programming/response-curves-in-xml-for-game-parametrization-r2717

                                                                                     
///////////////////////////////////////////////////////////////////////////////////////////*/
public class ResponseCurve : MonoBehaviour
{
  [System.Serializable]
  private struct CurveDefinition { public float input; public float output; }
  [SerializeField] 
  private        CurveDefinition[] m_curveDefinition;

  //////////////////////////////////////////////////////////////////////////////////////
  //                                                                                  //
  //                                                                                  //
  //                                                                                  //  
  //////////////////////////////////////////////////////////////////////////////////////  
  void Start()
  {
    checkCurveDefinitionValid();
  }

  //////////////////////////////////////////////////////////////////////////////////////
  //                                                                                  //
  //                                                                                  //
  //                                                                                  //  
  //////////////////////////////////////////////////////////////////////////////////////  
  public void Initialize( float[] inputs, float[] outputs ) 
  {
    if ( ( inputs.Length != outputs.Length ) || ( inputs.Length < 2 ) )
    {
      Debug.LogError( "definition length error. Must be >1 and the same between inputs and outputs " );
      Debug.Break();
    }

    m_curveDefinition = new CurveDefinition[ inputs.Length ];

    for ( int i = 0; i < m_curveDefinition.Length; i++ )
    {
      m_curveDefinition[i].input  = inputs[i];
      m_curveDefinition[i].output = outputs[i];
    }

    checkCurveDefinitionValid();
  }
    
  //////////////////////////////////////////////////////////////////////////////////////
  //                                                                                  //
  //                                                                                  //
  //                                                                                  //  
  //////////////////////////////////////////////////////////////////////////////////////  
  public float GetValue( float input ) 
  {
    input = Mathf.Clamp( input, m_curveDefinition[0].input, m_curveDefinition[ m_curveDefinition.Length - 1 ].input );

    for ( int i = 1; i < m_curveDefinition.Length; i++ )
    { 
      if ( input <= m_curveDefinition[i].input )
      {
        float t = ( input - m_curveDefinition[ i - 1 ].input ) / ( m_curveDefinition[ i ].input - m_curveDefinition[ i - 1 ].input );
        return m_curveDefinition[ i - 1 ].output * ( 1.0f - t ) + m_curveDefinition[ i ].output * t;
      }
    }    
    Debug.LogError( " Error : input was not well clamped to itś values" );
    return 0;            
  }

  //////////////////////////////////////////////////////////////////////////////////////
  //                                                                                  //
  //                                                                                  //
  //                                                                                  //  
  //////////////////////////////////////////////////////////////////////////////////////  
  public bool checkCurveDefinitionValid()
  {
    bool bOk = m_curveDefinition.Length < 2 ;
    
    for ( int i = 1; i < m_curveDefinition.Length; i++ )
    {
      if ( m_curveDefinition[ i ].input < m_curveDefinition[ i - 1 ].input )
      {
        Debug.LogError( " Input definition is not in ascendant order" );
        bOk = false;
      }
    }
    return bOk;
  }

  //////////////////////////////////////////////////////////////////////////////////////
  //                                                                                  //
  //                                                                                  //
  //                                                                                  //  
  //////////////////////////////////////////////////////////////////////////////////////  
  public int GetCurveDefinitionSize()
  {
    return m_curveDefinition.Length;
  }
  
  //////////////////////////////////////////////////////////////////////////////////////
  //                                                                                  //
  //                                                                                  //
  //                                                                                  //  
  //////////////////////////////////////////////////////////////////////////////////////  
  public float GetCurveDefinitionInputAt( int index )
  {
    return m_curveDefinition[ index ].input;
  }

}
