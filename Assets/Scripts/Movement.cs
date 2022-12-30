using UnityEngine;

public class Movement : MonoBehaviour
{   
    float rocketThrust = 750f, rocketRotation = 150f, currentRotation = 0;
    int rotationInertia = 2;
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
            currentRotation = rocketRotation;
            Rotate();
            if(rocketRotationSound.volume == 0){
                rocketRotationSound.volume = 0.5f;
            }
        }
        else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)){
            currentRotation = -rocketRotation;
            Rotate();
            if(rocketRotationSound.volume == 0){
                rocketRotationSound.volume = 0.5f;
            }
        }
        else{
            if(rocketRotationSound.volume > 0){
                rocketRotationSound.volume = 0;
            }
            //this part imitates inertia, basically every time you release the rotation button
            //it will stop gradualy instead of stopping abruptly
            //rotantionInertia should be an integer, since it will be subtracted/added to the rotation value, 
            //it should result in a round 0
            if(currentRotation > 0){
                currentRotation -= rotationInertia;
                Rotate();
            }
            else if (currentRotation < 0){
                currentRotation += rotationInertia;
                Rotate();
            }
        }

    }

    void Rotate()
    {   
        rocketRigidBody.freezeRotation = true;
        rocketTransform.Rotate(Vector3.forward * currentRotation * Time.deltaTime);
        rocketRigidBody.freezeRotation = false;
    }
}
