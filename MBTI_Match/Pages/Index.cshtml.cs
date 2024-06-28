using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading;

namespace MBTI_Match.Pages
{
    public class IndexModel : PageModel
    {

        //public LogData Message { get; set; } = new LogData();

        //private readonly MetricsService metricsService;
        private static readonly Meter meter = new Meter("mbti_match"); 
        public int? ClickCount { get; set; } = 0;
        public string? PersonalityType { get; set; } = "";
        public string? Activity { get; set; } = "";
        
        /*
        public LogData ClickCount { get; set; } = 0;
        public LogData PersonalityType { get; set; } = "";
        public LogData Activity { get; set; } = "";
        */
        private readonly ILogger<IndexModel> _logger;
       // private readonly Meter _meter;
        //private readonly Counter<long> _mbtiCounter;
       // public Counter<long> clickCounter;
       // public Counter<long> personalityTypeCounter;
      //  public Counter<long> activityCounter;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            // Create a Meter instance
            //_meter = new Meter("MBTI_Match");

            
            /*
            // Create a counter for the mbtiCounter metric
            //_mbtiCounter = _meter.CreateCounter<long>("mbtiCounter");
            clickCounter = _meter.CreateCounter<long>("clickCount");
            personalityTypeCounter = _meter.CreateCounter<long>("personalityTypeCount");
            activityCounter = _meter.CreateCounter<long>("activityCount");
            */

        }

        public void OnGet()
        {
            // Populate Message from query parameters
            

            // Log the data
            
        }

        public IActionResult OnPost(int? clickCount, string personalityType, string activity)
        {
            ClickCount = clickCount+1;
            PersonalityType = personalityType;
            Activity = activity;


            _logger.LogInformation($"Button clicked {ClickCount} times");
            _logger.LogInformation($"Personality Type: {PersonalityType}");
            _logger.LogInformation($"Activity: {Activity}");

            Counter<long> clickCounter = meter.CreateCounter<long>("ButtonClicks");
            Counter<long> personalityTypeCounter = meter.CreateCounter<long>("PersonalityType");
            Counter<long> activityCounter = meter.CreateCounter<long>("Activity");

            clickCounter.Add(1);
            personalityTypeCounter.Add(1, new KeyValuePair<string, object?>("PersonalityType", personalityType));
            activityCounter.Add(1, new KeyValuePair<string, object?>("Activity", activity));

          //metricsService.UpdateMetrics(personalityType, activity);
            /*
            Message.ClickCount = ClickCount;

            Message.PersonalityType = personalityType;
            Message.Activity = activity;
            /*

            Program.clickCounter.Add(1 );
            Program.personalityTypeCounter.add(1, new("MBTI", personalityType));
            Program.activityCounter.add(1, new("Activity", activity));
            */

            // Record the metrics

            // Record the metrics
            //_mbtiCounter.Add(1, new("PersonalityType", PersonalityType), new("Activity", Activity));
            // Record the metrics
            //_mbtiCounter.Add(1, new[] { new KeyValuePair<string, object>("PersonalityType", PersonalityType), new KeyValuePair<string, object>("Activity", Activity) });


            return Page();
        }



    }


    public class MetricsService
    {
        private readonly Counter<long> _clickCounter;
        private readonly Counter<long> _personalityTypeCounter;
        private readonly Counter<long> _activityCounter;

        public MetricsService(Counter<long> clickCounter, Counter<long> personalityTypeCounter, Counter<long> activityCounter)
        {
            _clickCounter = clickCounter;
            _personalityTypeCounter = personalityTypeCounter;
            _activityCounter = activityCounter;
        }

        public void UpdateMetrics(string personalityType, string activity)
        {
            _clickCounter.Add(1);
            _personalityTypeCounter.Add(1, new KeyValuePair<string, object?>("PersonalityType", personalityType));
            _activityCounter.Add(1, new KeyValuePair<string, object?>("Activity", activity));
        }
    }


    public class LogData
    {
        public int? ClickCount { get; set; }
        public string? PersonalityType { get; set; }
        public string? Activity { get; set; }
    }
    
}
