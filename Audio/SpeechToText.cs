using Google.Cloud.Speech.V1;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using static Google.Cloud.Speech.V1.SpeechClient;

namespace BassyTTSTwitch.Audio
{
    public class SpeechToText
    {
        public readonly StreamingRecognizeStream Stream;
        public readonly StreamingRecognitionConfig Config;
        public List<StreamingRecognitionResult> Results;
        private long StopTime;
        private bool _isFinished = false;
        private bool _isFinalized = false;

        private StreamingRecognitionResult _lastNonFinishedResult;

        public SpeechToText(StreamingRecognizeStream stream, StreamingRecognitionConfig config, TimeSpan duration)
        {
            Stream = stream;
            Config = config;
            Results = new List<StreamingRecognitionResult>();

            StopTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() + (long) duration.TotalMilliseconds;
            
        }

        public void AddResults(params StreamingRecognitionResult[] results)
        {

            if (results.Length == 0)
                return;

            Console.WriteLine($"{results[0].Alternatives[0].Transcript}");

            bool foundFinal = false;
            foreach(StreamingRecognitionResult result in results)
            {
                if (result.IsFinal)
                {
                    Results.Add(result);
                    foundFinal = true;
                }
            }

            if (!foundFinal)
            {
                _lastNonFinishedResult = results[0];
            } else
            {
                _lastNonFinishedResult = null;
            }
        }

        public string GetCurrentResultString()
        {
            if (Results.Count == 0 && _lastNonFinishedResult == null)
                return "";

            string currentString = "";

            for(int i = 0; i < Results.Count; i++) {
                currentString += $" {Results[i].Alternatives[0].Transcript}";
            }

            if(_lastNonFinishedResult != null)
            {
                currentString += $" {_lastNonFinishedResult.Alternatives[0].Transcript}";
            }

            return currentString;
        }


        public bool IsCompleted()
        {
            return _isFinished;
        }

        public bool IsFinalized()
        {
            return _isFinalized;
        }

        public void SetFinalized()
        {
            _isFinalized = true;
        }

        public void FinalizeIfBlank()
        {
            if(GetCurrentResultString() == "")
            {
                SetFinalized();
            }
        }


        public void AddAudioToStream(byte[] bytearray)
        {
            if (_isFinished) 
                return;

            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > StopTime)
            {
                FinalizeAudio();
                return;
            }

            StreamingRecognizeRequest request = new StreamingRecognizeRequest();
            request.StreamingConfig = Config;
            ByteString audio = ByteString.CopyFrom(bytearray);
            request.AudioContent = audio;
            Stream.WriteAsync(request);
        }

        public void FinalizeAudio()
        {
            if(!_isFinished) {
                Stream.WriteCompleteAsync();
                _isFinished = true;
            }
        }

    }
}
