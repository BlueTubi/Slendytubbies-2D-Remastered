using UnityEngine;

public class MenuTerrain : MonoBehaviour
{
	public float curpos = -60f;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position = new Vector3(curpos, -3f, -70f);
		if (curpos > -1015f)
		{
			curpos -= 3f * Time.deltaTime;
		}
		else
		{
			curpos = 10f;
		}
	}
}
