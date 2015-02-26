# ResponseCurve
This Unity3d component implements a response curve easily configurable at the unity inspector. 

ResponseCurve.cs     -> the Unity3D component.
ResponseCurveEditor  -> optional inspector extension to visualize the response curve as a 2 axis graphic.
ResponseCurveTest.cs -> Response Curve test ( Unity Test Tools ) .

-------

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
