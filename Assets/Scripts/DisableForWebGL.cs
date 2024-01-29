using UnityEngine;

public class DisableForWebGL : MonoBehaviour
{
	void Start()
	{
#if UNITY_WEBGL
		gameObject.SetActive(false);
#endif
	}
}
