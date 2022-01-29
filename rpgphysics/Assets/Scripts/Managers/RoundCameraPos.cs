using UnityEngine;
using Cinemachine;

public class RoundCameraPos : CinemachineExtension
{
    //Displaying 32 pixels in one world unit
    public float PixelsPerUnit = 32;
    //Method required by all classes inherited from CinemachineExtension
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        //Checking what stage  of the camera's post-processing we're in
        if (stage == CinemachineCore.Stage.Body)
        {
            //Retrieve the Virtual Camera's final position
            Vector3 pos = state.FinalPosition;
            //Call the rounding method to round position, create new Vector with the results. THis is our new, pixel bounded position.
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);
            // Set the VC's new position to the difference between the old position and the new rounded position that we just calculated
            state.PositionCorrection += pos2 - pos;
        }
    }
    //Method that rounds the input value, making sure the camera stays on a pixel position
    float Round(float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
