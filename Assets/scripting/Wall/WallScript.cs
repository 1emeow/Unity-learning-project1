using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField]
    private Animator _wallAnimator;
    public bool IsRotating;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _wallAnimator.SetBool("IsRotating", IsRotating);
    }
}
