using ClassifierApp;
using static ClassifierApp.Constants;

if (args.Length == 0)
{
    Console.WriteLine("No arguments");
    Environment.Exit(EXIT_CODE_SUCCESS);
}

//FileBaseHelper.ClearFileDescriptions();

var lines = FileBaseHelper.ParseTextToTabularData(args);

foreach (var arg in lines)
{
    var fileRow = new InputFileRow
    {
        Original = arg.Split('\t')[1],
    };
    
    ClassifierAppMLModel.ModelInput dataToPredict = new()
    {
        Original = fileRow.Cleanned
    };

    var predicted = ClassifierAppMLModel.Predict(dataToPredict);

    var contentLine = $"{arg}\t{predicted.PredictedLabel}\tcheck [{Math.Round((decimal)(predicted.Score.First() * 100))}]";

    Console.WriteLine(contentLine);
}
