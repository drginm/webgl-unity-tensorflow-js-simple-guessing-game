
var publicInterface = window.PublicInterface = window.PublicInterface || {};
window.PublicInterface.onUnityInitialized = function () {
    console.log('Unity WebGL online');
};
publicInterface.startTraining = function (xValues, yValues) {
    // Define a model for linear regression.
    var model = publicInterface.model = tf.sequential();
    model.add(tf.layers.dense({ units: 1, inputShape: [1] }));
    // Prepare the model for training: Specify the loss and the optimizer.
    model.compile({ loss: 'meanSquaredError', optimizer: 'sgd' });
    var xs = tf.tensor2d(xValues, [xValues.length, 1]);
    var ys = tf.tensor2d(yValues, [yValues.length, 1]);
    // Train the model using the data.
    model.fit(xs, ys, { epochs: 50 }).then(function () {
        publicInterface.trainingDone();
    });
};
publicInterface.getPrediction = function (valueToPredict) {
    var prediction = 0;
    if (publicInterface.model) {
        // Use the model to do inference on a data point the model hasn't seen before:
        prediction = publicInterface.model.predict(tf.tensor2d([valueToPredict], [1, 1])).get(0, 0);
    }
    return prediction;
};
