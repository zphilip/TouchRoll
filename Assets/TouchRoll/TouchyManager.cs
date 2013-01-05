using UnityEngine;
using System.Collections;
[AddComponentMenu("Touch/Touchy Manager")]

public class TouchyManager : MonoBehaviour
{
	public string groundTag = "Ground";
	public string autoRollTag = "Auto Roll";
	public int touchyCollisionLayer = 12;
	public int touchAreaLayer = 22;
	
	private LayerMask touchBegan;
	private LayerMask touchMoved;
	private Touchy[] touchies = new Touchy[10];
	private bool layersSet = false;
	
	public void Update ()
	{
		if (!layersSet)
		{
			touchBegan = 1 << touchAreaLayer;
			touchMoved = ~(1 << touchyCollisionLayer | 1 << touchAreaLayer);
			layersSet = true;
		}
		if (Input.touchCount > 0)
		{
			Ray ray;
			RaycastHit hit;
			RaycastHit lastHit = new RaycastHit();
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					ray = Camera.main.ScreenPointToRay(touch.position);
					if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchBegan))
					{
						if (hit.transform.GetComponent<Touchy>())
						{
							Touchy touchy = hit.transform.GetComponent<Touchy>();
							if (!touchy.beingTouched)
							{
								touchies[touch.fingerId] = touchy;
								if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchMoved))
								{
									if (hit.transform.tag == groundTag)
									{
										touchy.TouchBegan(hit);
									}
								}
							}
						}
					}
				}
				
				if (touch.phase == TouchPhase.Moved && touchies[touch.fingerId] != null)
				{
					ray = Camera.main.ScreenPointToRay(touch.position);
					if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchMoved))
					{
						if (hit.transform.tag == groundTag)
						{
							touchies[touch.fingerId].TouchMoved(hit);
							lastHit = hit;
						}
						if (hit.transform.tag == autoRollTag)
						{
							touchies[touch.fingerId].TouchEnded(lastHit);
							touchies[touch.fingerId] = null;
						}
					}
				}
				
				if (touch.phase == TouchPhase.Ended && touchies[touch.fingerId] != null)
				{
					ray = Camera.main.ScreenPointToRay(touch.position);
					if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchMoved))
					{
						touchies[touch.fingerId].TouchEnded(hit);
					}
					touchies[touch.fingerId] = null;
				}
			}
		}
	}
}
