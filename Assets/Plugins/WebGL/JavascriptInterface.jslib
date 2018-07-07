mergeInto(LibraryManager.library, {
  StartTraining: function (xValues, yValues, size) {
    var xValuesArray = [],
        yValuesArray = [];

    for(var i = 0; i < size; i++){
      xValuesArray.push(HEAPF32[(xValues >> 2) + i]);
      yValuesArray.push(HEAPF32[(yValues >> 2) + i]);

      console.log(HEAPF32[(xValues >> 2) + i]);
    }

    window.PublicInterface.startTraining(xValuesArray, yValuesArray);
  },
  GetPrediction: function (valueToPredict) {
    return window.PublicInterface.getPrediction(valueToPredict);
  }
});
