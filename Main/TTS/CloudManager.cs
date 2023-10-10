using BassyTTSTwitch.Audio;
using BassyTTSTwitch.Properties;
using Google.Api.Gax;
using Google.Api.Gax.Grpc;
using Google.Cloud.Speech.V1;
using Google.Cloud.TextToSpeech.V1;
using Google.Protobuf;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Google.Cloud.Speech.V1.SpeechClient;

namespace BassyTTSTwitch.TTS
{
    public class CloudManager
    {
        private static CloudManager _instance;

        public TextToSpeechClient TTSClient;
        public AudioConfig AudioConfig;
        public VoiceSelectionParams VoiceParams;
        public Voice CurrentVoice;
        public Task CurrentTask;
        public volatile AudioState AudioState;
        private StreamingRecognitionConfig _streamingRecognitionConfig;


        public WaveOut Player;
        public WaveFileReader Reader;

        public bool Running = true;


        // Speech to text
        public SpeechClient STTClient;

        public volatile SpeechToText CurrentSpeechToText;

        private readonly Thread SpeechToTextThread;



        public readonly List<Voice> Voices = new List<Voice>();

        public readonly string[] BLACKLIST_VOICES = new string[]
        {
            "en-US-Studio-M",
            "en-US-Studio-O",
            "en-US-Neural2-A",
            "en-US-Neural2-C",
            "en-US-Neural2-D",
            "en-US-Neural2-E",
            "en-US-Neural2-F",
            "en-US-Neural2-G",
            "en-US-Neural2-H",
            "en-US-Neural2-I",
            "en-US-Neural2-J",
            "en-US-Polyglot-1",
        };

        public static void Initialize()
        {
            if(_instance == null)
            {
                new CloudManager();
            }
        }

        public static CloudManager GetInstance()
        {
            return _instance;
        }

        private CloudManager()
        {
            _instance = this;

            if (!CredentialManager.HasAllCredentials())
                return;

            Player = new WaveOut();
            Player.PlaybackStopped += OnFinished;

            SetDefaultAudio();
            SetDefaultVoice();
            Login();
            GetAllVoices();

            SpeechToTextThread = new Thread(SpeechToTextLoop);
            SpeechToTextThread.IsBackground = true;
            SpeechToTextThread.Start();

            SettingsManager.OnSave += OnSettingsChanged;
            LoadSettings(Settings.Default);
        }

        private void OnSettingsChanged(object sender, SaveEvent saveEvent) {
            LoadSettings(saveEvent.Settings);
        }

        private void LoadSettings(Settings s)
        {
            SetVoice(s.CurrentVoice);
            SetVolume(s.Volume);
        }


        public async void SpeechToTextLoop()
        {
            while (Running)
            {
                if(CurrentSpeechToText != null)
                {
                    try
                    {
                        if (CurrentSpeechToText.IsFinalized())
                            continue;

                        var stream = CurrentSpeechToText.Stream;
                        await foreach (var result in stream.GetResponseStream())
                        {
                            if(result.Error != null)
                            {
                                Console.WriteLine($"ERROR {result.Error.Message}");
                            }
                            else
                            {
                                CurrentSpeechToText.AddResults(result.Results.ToArray());
                            }  
                        }

                        CurrentSpeechToText.SetFinalized();

                    }
                    catch (Exception e)
                    {

                        Console.WriteLine($"Exception Caught in SpeechToTextLoop: {e.Message}");
                    }
                }
            }
        }

        private void SetDefaultAudio()
        {
            AudioConfig = new AudioConfig();
            AudioConfig.AudioEncoding = AudioEncoding.Linear16;
        }

        private void SetDefaultVoice()
        {
            VoiceParams = new VoiceSelectionParams();
            VoiceParams.LanguageCode = "en-US";
            VoiceParams.SsmlGender = SsmlVoiceGender.Neutral;
        }

        private void OnFinished(object sender, StoppedEventArgs args)
        {
            AudioState.CurrentState = AudioState.State.FINISHED;
        }

        private void GetAllVoices()
        {
            ListVoicesRequest request = new ListVoicesRequest();
            request.LanguageCode = "en-US";

            ListVoicesResponse response = TTSClient.ListVoices(request);

            Voices.Clear();
            foreach(Voice voice in response.Voices)
            {
                bool include = true;
                foreach (string blacklist in BLACKLIST_VOICES)
                {
                    if (voice.Name == blacklist)
                    {
                        include = false;
                        break;
                    }
                }
                if(include)
                    Voices.Add(voice);
            }
        }

        private void SetVoice(string voice)
        {
            VoiceParams.Name = voice;
        }

        public void SetVolume(int volumePercent)
        {
            Player.Volume = (volumePercent / 100f);
        }

        private void Login()
        {

            try
            {
                TextToSpeechClientBuilder ttscb = new TextToSpeechClientBuilder();
                ttscb.CredentialsPath = CredentialManager.GetGooglePath();
                TTSClient = ttscb.Build();

                SpeechClientBuilder scb = new SpeechClientBuilder();
                scb.CredentialsPath = CredentialManager.GetGooglePath();
                STTClient = scb.Build();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to login. {e.Message}");
            }
        }

        public SpeechToText CreateSpeechStream()
        {
            Console.WriteLine("Creating Speech Stream");
            StreamingRecognizeStream stream = STTClient.StreamingRecognize(CallSettings.FromExpiration(Expiration.FromTimeout(new TimeSpan(0,0,62))));
            
            StreamingRecognizeRequest request = new StreamingRecognizeRequest();

            _streamingRecognitionConfig = new StreamingRecognitionConfig
            {
                InterimResults = true,
                SingleUtterance = false
            };

            RecognitionConfig config = new RecognitionConfig
            {
                LanguageCode = "en_US",
                EnableAutomaticPunctuation = true,
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                AudioChannelCount = 1
            };

            _streamingRecognitionConfig.Config = config;
            
            request.StreamingConfig = _streamingRecognitionConfig;
            Console.WriteLine("Creating Speech Stream: Sending request");
            stream.WriteAsync(request);
            CurrentSpeechToText = new SpeechToText(stream, _streamingRecognitionConfig, new TimeSpan(0, 0, 60));
            return CurrentSpeechToText;
        }

        public void AddAudioToStream(StreamingRecognizeStream stream, byte[] bytearray, bool finished = false)
        {
            if (stream == null) return;
            Console.WriteLine("Adding audio to stream");

            StreamingRecognizeRequest request = new StreamingRecognizeRequest();
            request.StreamingConfig = _streamingRecognitionConfig;
            ByteString audio = ByteString.CopyFrom(bytearray);
            request.AudioContent = audio;
            Console.WriteLine("Adding audio to stream: Sending request");
            stream.WriteAsync(request);
            if (finished)
            {
                stream.WriteCompleteAsync();
            }

        }

        public SynthesizeSpeechResponse GetAudio(string prompt, string voice = null)
        {
            SynthesisInput input = new SynthesisInput();
            input.Text = prompt;

            SynthesizeSpeechRequest request = new SynthesizeSpeechRequest();
            request.AudioConfig = AudioConfig;

            if(voice != null)
            {
                VoiceSelectionParams param = VoiceParams.Clone();
                param.Name = voice;
                request.Voice = param;
            } else
            {
                request.Voice = VoiceParams;
            }

            request.Input = input;

            return TTSClient.SynthesizeSpeech(request);  
        }


        public AudioState Speak(string prompt, string voice = null)
        {

            StopSound();
            AudioState state = new AudioState(prompt);
            AudioState = state;
            

            CurrentTask = Task.Run(() => {
                try
                {
                    state.CurrentState = AudioState.State.PREPARING;

                    Console.WriteLine("Getting audio for prompt");
                    SynthesizeSpeechResponse response = GetAudio(prompt, voice);

                    MemoryStream stream = new MemoryStream(response.AudioContent.ToByteArray());
                    
                    LoadAudio(stream);
                    PlaySound();
                    Console.WriteLine("Prompt audio successfully generated.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    AudioState.CurrentState = AudioState.State.FINISHED;
                }
            });

            return state;
        }


        public void LoadAudio(Stream stream)
        {
            WaveFileReader reader = new WaveFileReader(stream);
            Player.Init(reader);
            AudioState.CurrentState = AudioState.State.READY_TO_PLAY;
        }

        public bool IsReady()
        {
            if (AudioState == null) return true;
            if (AudioState.CurrentState == AudioState.State.FINISHED) return true;
            return false;
        }

        public void PlaySound()
        {
            Player.Play();
            AudioState.CurrentState = AudioState.State.PLAYING;
        }

        public void StopSound()
        {
            if (Player != null)
            {
                Console.WriteLine("Stopping Sound");
                Player.Stop();
                Player.Dispose();

                if(AudioState != null)
                    AudioState.CurrentState = AudioState.State.FINISHED;
            }

            if(Reader != null)
            {
                Reader.Dispose();
                Reader = null;
            }
        }
    }
}
