using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds;     //Array of all objects to be parallaxed
    private float[] parallaxScales;     //Proportion of cameras movement
    public float smoothing = 1f;             //How smooth the paralax is goinng to be. Make sure to set this above 0

    private Transform cam;              //Reference to the main camera's transform
    private Vector3 previousCamPosition;    //The position of the camera in the previous frame
    
    //Called before start. Great for setting references
    void Awake()
    {
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start () {
        //The previous frame had the current frame's camera position
	    previousCamPosition = cam.position;

	    parallaxScales = new float[backgrounds.Length];

	    for (int i = 0; i < backgrounds.Length; i++)
	    {
	        parallaxScales[i] = backgrounds[i].position.z*-1;
	    }

	}
	
	// Update is called once per frame
	void Update () {
	
        //foreach background
	    for (int i = 0; i < backgrounds.Length; i++)
	    {
	        //the paralax is the opposite of the camera movement because the previous frame multiplied by the scale
	        float parallaxX = (previousCamPosition.x - cam.position.x)*parallaxScales[i];

            //float parallaxY = (previousCamPosition.y - cam.position.y) * parallaxScales[i];

            //set a target x position which is the current position + the parallax
            float backgroundTargetPositionX = backgrounds[i].position.x + parallaxX;

            //float backgroundTargetPositionY = backgrounds[i].position.y + parallaxY;

            //create a target position for the background
            Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y,
	            backgrounds[i].position.z);

            //fade between current position and the target position using lerp
	        backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPosition,
	            smoothing*Time.deltaTime);
	    }

        // set previousCamPos to the camera's position at the end of the frame
	    previousCamPosition = cam.position;
	}
}
