using ClassifierApp;
using System.Text;
using static ClassifierApp.Constants;

if (args.Length == 0)
{
    Console.WriteLine("No arguments");
    Environment.Exit(EXIT_CODE_SUCCESS);
}

var fileLines = File.ReadAllLines($"{Environment.CurrentDirectory}\\Extrato-2024-08-classificado.tsv");

var newFileLines = new List<string>();

foreach (var line in fileLines)
{
    var lineParts = line.Split('\t');
    
    var extratoItemDescription = lineParts[0];
    var extratoItemClassification = lineParts[1];

    var descriptionParts = extratoItemDescription.Split(" ");

    var descr = new StringBuilder();

    for (int i = 1; i < descriptionParts.Length -2; i++)
    {
        descr.Append(" " + descriptionParts[i]);
    }

    var description = descr.ToString().Trim();

    newFileLines.Add($"{description}\t{extratoItemClassification}");
}

File.WriteAllLines($"{Environment.CurrentDirectory}\\file-cleaned.tsv", newFileLines);

foreach (var arg in args)
{
    ClassifierAppMLModel.ModelInput dataToPredict = new()
    {
        Original = arg
    };

    var predicted = ClassifierAppMLModel.Predict(dataToPredict);

    Console.WriteLine($"{arg}={predicted.PredictedLabel}");
}
