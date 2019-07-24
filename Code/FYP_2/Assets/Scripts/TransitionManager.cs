using UnityEngine;
using System.Collections;
using Vuforia;

public class TransitionManager : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES
    public GameObject[] VROnlyObjects;
    public GameObject[] AROnlyObjects;

    [Range(0.1f, 5.0f)]
    public float transitionDuration = 1.5f; // seconds
    #endregion PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES
    private BlackMaskBehaviour mBlackMask;

    private float mTransitionFactor = 0;
    private bool mPlaying = false;
    private bool mBackward = false;
    private bool mCurrentARMode = false;
    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start () 
    {
        mTransitionFactor = 0;
        
        mBlackMask = FindObjectOfType<BlackMaskBehaviour>();
        SetBlackMaskVisible(false, 0);
        SetVideoBackgroundVisible(true);

        foreach (var go in VROnlyObjects)
        {
            go.SetActive(false);
        }
	}
	
	void Update () 
    {
        
        bool isVideoCurrentlyEnabled = IsVideoBackgroundRenderingEnabled();

        bool ARMode = (mTransitionFactor <= 0.5f);
        if ((ARMode != mCurrentARMode) || (ARMode != isVideoCurrentlyEnabled))
	    {
	        mCurrentARMode = ARMode;

	        
	        int targetFPS =
	            VuforiaRenderer.Instance.GetRecommendedFps(ARMode ? VuforiaRenderer.FpsHint.NONE : VuforiaRenderer.FpsHint.NO_VIDEOBACKGROUND);
	        
	        Application.targetFrameRate = targetFPS;

            if (ARMode != isVideoCurrentlyEnabled)
	        {
	            SetVideoBackgroundVisible(ARMode);
            }

           
            VuforiaBehaviour.Instance.SetSkewFrustum(ARMode);

            
            foreach (var hea in GetCameraRigRoot().GetComponentsInChildren<HideExcessAreaAbstractBehaviour>())
            {
                if (hea.enabled != ARMode)
                    hea.enabled = ARMode;
            }

            foreach (var go in VROnlyObjects)
            {
                go.SetActive(!ARMode);
            }

            foreach (var go in AROnlyObjects)
            {
                go.SetActive(ARMode);
            }
	    }

	    if (mPlaying)
        {
            SetBlackMaskVisible(true, mTransitionFactor);
            
            float delta = (mBackward ? -1 : 1) * Time.deltaTime / transitionDuration;
            mTransitionFactor += delta;

            if (mTransitionFactor <= 0 || mTransitionFactor >= 1)
            {
                
                mTransitionFactor = Mathf.Clamp01(mTransitionFactor);
                mPlaying = false;
                SetBlackMaskVisible(false, mTransitionFactor);
            }
        }
	}
    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    public void Play(bool reverse)
    {
        mPlaying = true;
        mBackward = reverse;
        mTransitionFactor = mBackward ? 1 : 0;
    }
    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS
    private void SetBlackMaskVisible(bool visible, float fadeFactor)
    {
        if (mBlackMask)
        {
            if (mBlackMask.GetComponent<Renderer>().enabled != visible)
                mBlackMask.GetComponent<Renderer>().enabled = visible;

            mBlackMask.SetFadeFactor(fadeFactor);
        }
    }

    private bool IsVideoBackgroundRenderingEnabled()
    {
        var bgPlaneBehaviour = GetCameraRigRoot().GetComponentInChildren<BackgroundPlaneAbstractBehaviour>();
        return (bgPlaneBehaviour ? bgPlaneBehaviour.GetComponent<MeshRenderer>().enabled : false);
    }

    private void SetVideoBackgroundVisible(bool visible)
    {
        Transform cameraRigRoot = GetCameraRigRoot();

        
        foreach (var vbb in cameraRigRoot.GetComponentsInChildren<VideoBackgroundAbstractBehaviour>())
        {
            if (vbb.enabled != visible)
                vbb.enabled = visible;
        }

        
        var bgPlaneBehaviour = cameraRigRoot.GetComponentInChildren<BackgroundPlaneAbstractBehaviour>();
        if (bgPlaneBehaviour)
        {
             MeshRenderer mr = bgPlaneBehaviour.GetComponent<MeshRenderer>();
             if (mr.enabled != visible)
                 mr.enabled = visible;
        }
    }

    private Transform GetCameraRigRoot()
    {
        VuforiaBehaviour vuforia = VuforiaBehaviour.Instance;
        return (vuforia.CentralAnchorPoint ? vuforia.CentralAnchorPoint.transform.root : vuforia.transform);
    }
    #endregion PRIVATE_METHODS
}
