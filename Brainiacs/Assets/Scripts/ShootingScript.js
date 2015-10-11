var projectile : Rigidbody;
var speed = 10;

function Update () {

    if ( Input.GetButton ("Fire1")) {

        clone = Instantiate(projectile, transform.position, transform.rotation);
        clone.velocity = transform.TransformDirection( Vector3 (speed, 0, 0));

        Destroy (clone.gameObject, 3);

    }}