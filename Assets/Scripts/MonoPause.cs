using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoPause : MonoBehaviour
{
	// Start is called before the first frame update
	public bool pause = false;


	private IEnumerator Update()
	{
		while (Application.isPlaying)
		{
			if (!pause)
				DoUpdate();

			yield return null;

			// You could even have a time variable here
		}
	}

	// That's where it happens !
	protected abstract void DoUpdate();
}
