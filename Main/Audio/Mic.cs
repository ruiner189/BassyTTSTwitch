using BassyTTSTwitch.Keyboard;
using BassyTTSTwitch.TTS;
using NAudio.Wave;
using NAudio.Wave.Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace BassyTTSTwitch.Audio
{
    public static class Mic
    {
        private static Thread MicThread;

        private static WaveInEvent Microphone;
        private static bool _recordMic;
        private static bool _isRecording;
        private static bool _lockMicLoop;
        private static bool _useHotKey = true;
        private static readonly long _recordMicDelay = 100;
        private static readonly long _endMicDelay = 3000;
        private static long _lastMicHotKeyPress;

        private static SpeechToText _currentSpeechToText;

        public static EventHandler<MicEvent> MicStartEvent;
        public static EventHandler<MicEvent> MicPauseEvent;
        public static EventHandler<MicEvent> MicResumeEvent;
        public static EventHandler<MicEvent> MicStopEvent;

        public static readonly List<DeviceInfo> InputDevices = new List<DeviceInfo>();


        private static bool _initialized = false;
        private static bool _running = true;

        public static void Initialize()
        {
            if(_initialized) return;
            _initialized = true;
            _running = true;

            GetAllInputDevices();

            LoadMic();

            if(MicThread == null)
            {
                MicThread = new Thread(MicLoop);
                MicThread.IsBackground = true;
                MicThread.Start();
            }


            KeyboardManager.GetInstance().KeyPressed += OnMicHotkey;
        }

        public static void Dispose()
        {
            Microphone.Dispose();

            _running = false;
            _initialized = false;
            KeyboardManager.GetInstance().KeyPressed -= OnMicHotkey;
        }

        public static void MicLoop()
        {
            while (true)
            {
                try
                {
                    if (_lockMicLoop || !_running)
                        continue;

                    long now = Util.Now();
                    if (_recordMic && _isRecording)
                    {
                        if (now >= _lastMicHotKeyPress + _recordMicDelay)
                        {
                            Console.WriteLine("Pausing Recording");
                            PauseRecording();
                        }
                    }

                    if (_isRecording)
                    {
                        if (now >= _lastMicHotKeyPress + _endMicDelay)
                        {
                            Console.WriteLine("Stopping Recording");
                            StopRecording();
                        }
                    }
                }
                catch (Exception)
                {

                }

            }
        }

        private static void LoadMic()
        {
            Microphone = new WaveInEvent();
            Microphone.WaveFormat = new WaveFormat(16000, 16, 1);
            Microphone.DeviceNumber = 0;
            Microphone.DataAvailable += GetMicData;
        }

        private static void GetMicData(object sender, WaveInEventArgs args)
        {
            if (!_recordMic)
                return;

            if (_currentSpeechToText == null)
                return;
            if (_currentSpeechToText.IsCompleted())
            {
                _isRecording = false;
                Microphone.StopRecording();
                return;
            }

            _currentSpeechToText.AddAudioToStream(args.Buffer);
        }

        public static List<DeviceInfo> GetAllInputDevices()
        {
            InputDevices.Clear();
            for (int n = 0; n < WaveIn.DeviceCount; n++)
            {
                var caps = WaveIn.GetCapabilities(n);

                InputDevices.Add(new DeviceInfo(n, caps.ProductName));
            }

            return InputDevices;
        }

        public static SpeechToText StartRecording()
        {
            if (_currentSpeechToText != null)
            {
                if (!_currentSpeechToText.IsFinalized())
                    return null;
            }

            if (_isRecording)
                StopRecording();

            _currentSpeechToText = CloudManager.GetInstance().CreateSpeechStream();

            _isRecording = true;
            _recordMic = true;

            Microphone.StartRecording();



            MicStartEvent?.Invoke(null, new MicEvent(_currentSpeechToText));


            return _currentSpeechToText;
        }

        public static SpeechToText StartRecording(long duration)
        {
            _lastMicHotKeyPress = Util.Now() + duration;
            return StartRecording();
        }

        public static void StopRecording()
        {
            if (_currentSpeechToText != null)
            {
                _currentSpeechToText.FinalizeAudio();
                _currentSpeechToText.FinalizeIfBlank();
            }

            if (Microphone != null)
            {
                _isRecording = false;
                _recordMic = false;
                Microphone.StopRecording();
                MicStopEvent?.Invoke(null, new MicEvent(_currentSpeechToText));
            }
        }

        public static void PauseRecording()
        {
            if (_isRecording && _recordMic)
            {
                _recordMic = false;
                MicPauseEvent?.Invoke(null, new MicEvent(_currentSpeechToText));
            }
        }

        public static void ResumeRecording()
        {
            if (_isRecording && !_recordMic)
            {
                _recordMic = true;
                MicResumeEvent?.Invoke(null, new MicEvent(_currentSpeechToText));
            }
        }

        private static void OnMicHotkey(object sender, KeyPressedEventArgs e)
        {
            if (!_useHotKey)
                return;

            if (e != null)
            {
                if (e.Key == System.Windows.Forms.Keys.N)
                {
                    _lastMicHotKeyPress = Util.Now();
                    if (!_isRecording)
                    {
                        _lockMicLoop = true;
                        Console.WriteLine("Starting Recording");
                        StartRecording();
                        _lastMicHotKeyPress = Util.Now() + 500;
                        _lockMicLoop = false;
                    }
                    else if (!_recordMic)
                    {
                        _lockMicLoop = true;
                        Console.WriteLine("Resuming Recording");
                        ResumeRecording();
                        _lastMicHotKeyPress = Util.Now();
                        _lockMicLoop = false;
                    }
                }
            }
        }

        public static void SetUseHotKey(bool use)
        {
            _useHotKey = use;
        }

        public static bool IsRecording()
        {
            return _isRecording;
        }

        public static void SetMicId(int deviceID)
        {
            Microphone.DeviceNumber = deviceID;
        }
    }
}
