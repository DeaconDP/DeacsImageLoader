Deac's Image Loader prefab by deac.online, a member of TheMetaVerseCrew
more good vibes at www.deac.online & www.teamepic.org




v1.0

A simple run-time image loader for VRChat worldbuilders using Udon sharp.

Image Loading allows you to download images from the internet and use them as textures in your materials. The SDK includes an easy-to-use ImageDownload script, or you can make your own script with the new VRCImageDownloader object.




NB!!!
Trusted URLs:

The following domains are allowed to be used with Image Loading. If a domain is not on the list, images will not download unless 'Allow Untrusted URLs' has been enabled in the user's settings.

Discord (cdn.discordapp.com)
Dropbox (dl.dropbox.com)
GitHub (*.github.io)
ImageBam (images4.imagebam.com)
ImgBB (i.ibb.co)
imgbox (images2.imgbox.com)
Imgur (i.imgur.com)
Postimages (i.postimg.cc)
Reddit (i.redd.it)
Twitter (pbs.twimg.com)
VRChat (assets.vrchat.com)


Notes:

The maximum resolution is 2048 × 2048 pixels.
Attempting to download larger images will result in an error.
One image can be downloaded every five seconds.
If this limit is exceeded, images downloads are queued and downloaded in a random order.
This limit applies to your entire scene, regardless of the amount of VRCImageDownload components used.
The URL must point directly at an image file.
URL redirection is not allowed and will result in an error.
Downloaded images are automatically interpreted as RGBA, RGB, or RG images.
For example, a grayscale image with an alpha channel is interpreted as an RG image.
There is a limit of 1000 elements in the queue


Credits:

Prefab & UI by deac.online (www.deac.online)
Documentation by VRChat (https://docs.vrchat.com/docs/image-loading)
1 x script by realmlist (https://realmlist.booth.pm/items/4648405)
1 x script by architechVR (https://architechvr.booth.pm/items/2536209)