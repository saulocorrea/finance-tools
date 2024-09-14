using ClassifierApp;
using static ClassifierApp.Constants;

if (args.Length == 0)
{
    Console.WriteLine("No arguments");
    Environment.Exit(EXIT_CODE_SUCCESS);
}

//FileBaseHelper.ClearFileDescriptions();

foreach (var arg in args)
{
    var fileRow = new InputFileRow
    {
        Original = arg
    };
    
    ClassifierAppMLModel.ModelInput dataToPredict = new()
    {
        Original = fileRow.Cleanned
    };

    var predicted = ClassifierAppMLModel.Predict(dataToPredict);
    
    Console.WriteLine($"{arg}={predicted.PredictedLabel} [{predicted.Score.First()}]");
}
