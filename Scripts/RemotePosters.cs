using UdonSharp;
using UnityEngine;
using VRC.SDK3.Image;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

using System;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDK3.Components.Video;
using VRC.Udon;
using VRC.Udon.Common.Enums;


[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class RemotePosters : UdonSharpBehaviour
{
    [SerializeField, Tooltip("Should the remote posters redownload automatically? (NOT RECOMMENDED)")]
    private bool AutoRenew = false;
    [SerializeField, Tooltip("Default is once per 5 minutes (300 seconds). Minimum is 1 minute (60 seconds)"), MinAttribute(60)]
    private float ReloadDelay = 300;

    [Space]

    [SerializeField, Tooltip("URLs of images to load")]
    private VRCUrl[] imageUrls;

    [Space]
    
    [SerializeField, Tooltip("Materials to show downloaded images on.")]
    private Material[] Materials;    
    private VRCImageDownloader _imageDownloader;
    private IUdonEventReceiver _udonEventReceiver;
    private VRCPlayerApi player;


    public VRCUrlInputField newImageURL;    // new URL
    //public text


    
    private void Start()
    {   
        // Crash checks:     
        if (imageUrls == null){
            Debug.LogError("[RemotePosters] No Poster URLs set!");
            return;
        }
        if(imageUrls.Length != Materials.Length){
            Debug.LogError("[RemotePosters] Not the same amount of URLs as materials set! (1 material per image)");
            return;
        }

        // It's important to store the VRCImageDownloader as a variable, to stop it from being garbage collected!
        _imageDownloader = new VRCImageDownloader();
        
        // To receive Image and String loading events, 'this' is casted to the type needed
        _udonEventReceiver = (IUdonEventReceiver)this;

        _LoadImages();
    }



    public void _LoadNewImage()
        {
            imageUrls[0] = newImageURL.GetUrl();
;
            UpdatePosters();
        }
    

    private void _LoadImages(){

        for (int i = 0; i < imageUrls.Length; i++)
        {
            Material mat = Materials[i];
            VRCUrl url = imageUrls[i];
            var rgbInfo = new TextureInfo();
            rgbInfo.GenerateMipMaps = true;

            _imageDownloader.DownloadImage(url, mat, _udonEventReceiver, rgbInfo);
        }
        if(AutoRenew){
            SendCustomEventDelayedSeconds(nameof(UpdatePosters), ReloadDelay);
        }

    }
    public void _NetworkUpdatePosters(){
        SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdatePosters));
    }
    public void UpdatePosters(){
        // Clear the old posters first
        _imageDownloader.Dispose();

        // Re-download the posters
        _LoadImages();

        RequestSerialization();
    }

    public override void OnImageLoadSuccess(IVRCImageDownload result)
    {
        Debug.Log($"[RemotePosters] Image loaded: {result.SizeInMemoryBytes} bytes.");
    }

    public override void OnImageLoadError(IVRCImageDownload result)
    {
        Debug.LogError($"[RemotePosters] Image not loaded: {result.Error.ToString()}: {result.ErrorMessage}.");
    }

    private void OnDestroy()
    {
        _imageDownloader.Dispose();
    }
}