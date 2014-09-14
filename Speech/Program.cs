using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Speech.Synthesis;

namespace CSharpTextToSpeech
{
    class Program
    {
        static SpeechSynthesizer synth = new SpeechSynthesizer();

        static void Main(string[] args)
        {
            Console.BackgroundColor = System.ConsoleColor.DarkMagenta;
            Console.ForegroundColor = System.ConsoleColor.White;
            Console.Clear();
            while (true)
            {
            Begin:
                Console.Clear();
                Console.WriteLine("What do you want to say?\n\nType /exit to exit or /clear to clear. /changevoice to change the voice\n");
                string sayThis = Console.ReadLine();
                if (sayThis == "/exit")
                {
                    return;
                }
                else if (sayThis == "/clear")
                {
                    Console.Clear();
                    goto Begin;
                }
                else if (sayThis == "/changevoice")
                {
                    selectVoice();
                    goto Begin;
                }
                
                synth.SetOutputToDefaultAudioDevice();
                synth.Speak(sayThis);
                goto Begin;
            }
        }
        static void selectVoice()
        {
            Console.Clear();
            List<KeyValuePair<int, InstalledVoice>> voiceList = new List<KeyValuePair<int,InstalledVoice>>();
            int i = 0;
            foreach (var voice in synth.GetInstalledVoices())
            {
                if(voice.VoiceInfo.Name == synth.Voice.Name)
                    Console.WriteLine("{0}: {1} (Current)", i, voice.VoiceInfo.Name.ToString());
                else
                    Console.WriteLine("{0}: {1}", i, voice.VoiceInfo.Name.ToString());
                voiceList.Add(new KeyValuePair<int,InstalledVoice>(i, voice));
                i++;
            }
            Console.WriteLine("Input the number you want to select:\n");
            string change = Console.ReadLine();
            try
            {
                int p = int.Parse(change);
                foreach (var voice in voiceList)
                {
                    if (voice.Key == p)
                    {
                        synth.SelectVoice(voice.Value.VoiceInfo.Name);
                    }
                }
            }
            catch
            {
                selectVoice();
            }
        }
    }
}
