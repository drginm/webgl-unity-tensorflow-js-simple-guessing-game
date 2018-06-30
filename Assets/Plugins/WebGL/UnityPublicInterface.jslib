//https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html
mergeInto(LibraryManager.library, {
  InitializeJSInterface: function() {
    var publicInterface = window.PublicInterface = window.PublicInterface || {};

    publicInterface.trainingDone = function () {
      SendMessage('PublicInterface', 'TrainingDone');
    }

    //Trigger the onUnityInitialized function if it is defined.
    if(window.PublicInterface.onUnityInitialized) {
      window.PublicInterface.onUnityInitialized();
    }
    else {
      console.log("PublicInterface: No callback for unity! - define a function for window.PublicInterface.onUnityInitialized()")
    }
  }
});
