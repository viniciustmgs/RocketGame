using UnityEngine;

public class Movement : MonoBehaviour
{   
    float rocketThrust = 750f, rocketRotation = 150f;
    Rigidbody rocketRigidBody;
    Transform rocketTransform;
    AudioSource rocketThrustSound, rocketRotationSound;
    
    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        rocketTransform = GetComponent<Transform>();
        rocketThrustSound = GameObject.Find("ThrustSound").GetComponentInChildren<AudioSource>();
        rocketRotationSound = GameObject.Find("RotationSound").GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput(){
        
        //manage the thrust
        if(Input.GetKey(KeyCode.W)){
            rocketRigidBody.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
            if(rocketThrustSound.volume == 0){
                rocketThrustSound.volume = 0.8f;
            }
        }
        else if(rocketThrustSound.volume > 0){
            rocketThrustSound.volume = 0;
        }
    
        //manage the rotation
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Rotate(rocketRotation);
            if(rocketRotationSound.volume == 0){
                rocketRotationSound.volume = 0.5f;
            }
        }
        else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)){
            Rotate(-rocketRotation);
            if(rocketRotationSound.volume == 0){
                rocketRotationSound.volume = 0.5f;
            }
        }
        else{
            if(rocketRotationSound.volume > 0){
                rocketRotationSound.volume = 0;
            }
        }
    }

    void Rotate(float rotationPerFrame)
    {   
        rocketRigidBody.freezeRotation = true;
        rocketTransform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rocketRigidBody.freezeRotation = false;
    }
}
