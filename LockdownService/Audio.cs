using log4net;
using NAudio.Wave;
using System;
using System.IO;

namespace LockdownService
{
    internal static class Audio
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MyWindowsService));
        private static WaveOutEvent outputDevice;
        private static AudioFileReader audioFile;
        internal static void Play()
        {
            _log.Info("PLaying sound");
            string basePath = Environment.CurrentDirectory;
            string fullPath = Path.GetFullPath("Alarm.mp3", basePath);

            audioFile = new AudioFileReader(fullPath);
            outputDevice = new WaveOutEvent();

            audioFile.Volume = 1; //100 percent volume

            outputDevice.Init(audioFile);
            outputDevice.Play();
        }

        internal static void StopPlay()
        {
            outputDevice?.Stop();

            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }




    }
}
