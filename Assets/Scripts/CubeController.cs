 using UnityEngine;
     public class CubeController : MonoBehaviour
     {
		 public GameObject ParticalSystem;
         public float speed = 1;
         Transform target ;
	     Rigidbody rb ;
 
         public void Update(){
			 rb = GetComponent<Rigidbody>();
			 target = FindTarget();
		 }
		 
         public void FixedUpdate()
         {
			if( target != null )
			{
			
				RaycastHit hit;
				var rotation = Quaternion.LookRotation(target.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
				if(Physics.Raycast(transform.position, transform.forward, out hit) && hit.collider.gameObject.CompareTag("Sphere"))
				{
					Vector3 direction = (target.position - transform.position).normalized;
					rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
					if(Distance(FindTarget())>250)
					{
						speed+=0.5f;
					}else
					{
						if(speed>=7){
							speed= speed * 0.8f;
						}
					}		
				}
			}
				
         }
		 public float Distance(Transform t){
			 return (t.transform.position - transform.position).sqrMagnitude;
		 }
         public void OnCollisionEnter( Collision collision )
         {
             if( collision.collider.CompareTag("Sphere") )
             {
                 Destroy( collision.collider.gameObject );
				 Instantiate(ParticalSystem,transform.position,Quaternion.identity);
				 ScoreManager.instance.AddPoint();
				 
				 speed = 10;
                 target = FindTarget();
             }
         }
 
         public Transform FindTarget()
         {
             GameObject[] candidates = GameObject.FindGameObjectsWithTag("Sphere");
             float minDistance = Mathf.Infinity;
             Transform closest;
         
             if ( candidates.Length == 0 )
                 return null;
 
             closest = candidates[0].transform;
             for ( int i = 0 ; i < candidates.Length ; i++ )
             {
                 float distance = (candidates[i].transform.position - transform.position).sqrMagnitude;
 
                 if ( distance < minDistance )
                 {
                     closest = candidates[i].transform;
                     minDistance = distance;
                 }
             }    
             return closest;
         }    
     }
