// TODO:
// By default, screenshot files are placed next to the executable bundle -- we don't want this in a
// shipping game, as it will fail if the user doesn't have write access to the Applications folder.
// Instead we should place the screenshots on the user's desktop. However, the ~/ notation doesn't
// work, and Unity doesn't have a mechanism to return special paths. Therefore, the correct way to
// solve this is probably with a plug-in to return OS specific special paths.

// Mono/.NET has functions to get special paths... see discussion page. --Aarku

using UnityEngine;
using System.Collections;

public class TakeScreenshot : MonoBehaviour
{    

}