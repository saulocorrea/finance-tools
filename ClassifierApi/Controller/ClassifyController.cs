using ClassifierCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ClassifyController(ILogger<ClassifyController> logger) : ControllerBase
{
    private readonly ILogger<ClassifyController> _logger = logger;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CleanAndPredictCategory([FromBody] string[] values)
    {
        try
        {
            var lines = FileBaseHelper.ParseTextToTabularData(values);
            var returnLines = new List<string>();

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
                
                returnLines.Add(contentLine);

                Console.WriteLine(contentLine);
            }

            return Ok(string.Join(Environment.NewLine, returnLines));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to parse and predict value");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
