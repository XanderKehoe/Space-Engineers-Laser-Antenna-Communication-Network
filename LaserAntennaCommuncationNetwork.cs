using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {

        string delimiter = ",";

        List<IMyTerminalBlock> tempLA = new List<IMyTerminalBlock>();
        List<IMyLaserAntenna> laserAntenna = new List<IMyLaserAntenna>();
        List<String> tempArg = new List<String>();

        IMyTextPanel textPanelDES;

        int displaySwitch = 0;

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update100;

            GridTerminalSystem.GetBlocksOfType<IMyLaserAntenna>(tempLA);
            for (int n = 0; n < tempLA.Count; n++)
            {
                laserAntenna.Add((IMyLaserAntenna)tempLA[n]);
            }
            textPanelDES = GridTerminalSystem.GetBlockWithName("DESdata_LCD") as IMyTextPanel;
        }

        public void Main(string argument, UpdateType updateSource)
        {
            Display();
            //insert sending commands here
            if (argument == "dinosaur")
                Send(3, "rawr");

            else if (argument == "SendAlert")
                Send(5, "ALERT");

            //end of sending commands
            else
                if (argument == "") { } //do nothing
                else
                    tempArg.Add(argument);

            if (tempArg.Count > 0)
                ProcessCommand(tempArg[0]);
        }

        public void ProcessCommand(string arg)
        {
            string[] command = arg.Split(new string[] { delimiter }, StringSplitOptions.None);
            int parse0 = -1;
            try {
                parse0 = int.Parse(command[0]);
            }
            catch {
                Echo("Non Numerical Value Detected");
                Echo(command[0]+"");
            }

            if (parse0 > 0)
            {
                parse0--;
                command[0] = parse0.ToString();
                if (command[1] == "ABC(*&^%$#@!")
                {
                    //Do Nothing
                }
                //insert receiving commands here
                /*else if (command[1] == "rawr")
                {
                    IMyTextPanel textPanel = GridTerminalSystem.GetBlockWithName("Text panel") as IMyTextPanel;
                    textPanel.ShowPublicTextOnScreen();
                    textPanel.WritePublicText("Raawwrr");
                    textPanel.WritePublicText(textPanel.GetPublicText() + "\n" + tempArg[0]);
                    tempArg.RemoveAt(0);
                }*/
                else if (command[1] == "ALERT")
                {
                    IMyBlockGroup group = GridTerminalSystem.GetBlockGroupWithName("Warning Lights");
                    List<IMyInteriorLight> lights = new List<IMyInteriorLight>();

                    group.GetBlocksOfType(lights);
                    foreach (IMyInteriorLight light in lights)
                    {
                        light.ApplyAction("OnOff_On");
                    }
                    Retransmit(command, parse0);
                    tempArg.RemoveAt(0);
                }
                else if (command[1] == "DESdata")
                {
                    IMyProgrammableBlock desComp = GridTerminalSystem.GetBlockWithName("DES Computer") as IMyProgrammableBlock;
                    desComp.TryRun(arg);
                    tempArg.RemoveAt(0);
                }

                //end of receiving commands
                else
                {
                    //retransmit
                    Retransmit(command, parse0);


                    tempArg.RemoveAt(0);

                }

            }
        }

        public void Display()
        {
            if (displaySwitch == 0)
            {
                Echo("Laser Antenna Communication Script .");
                displaySwitch = 1;
            }
            else if (displaySwitch == 1)
            {
                Echo("Laser Antenna Communication Script ..");
                displaySwitch = 2;
            }
            else
            {
                Echo("Laser Antenna Communication Script ...");
                displaySwitch = 0;
            }
        }

        public void Send(int i, string message)
        {
            foreach (IMyLaserAntenna laserAntenna in laserAntenna)
                laserAntenna.TransmitMessage(i + delimiter + message);
           
            Echo("Sent: " + i + delimiter + message);
        }
        public void Send(string message)
        {
            foreach (IMyLaserAntenna laserAntenna in laserAntenna)
                laserAntenna.TransmitMessage(message);
           
            Echo("Sent: " + message);
        }

        public void Retransmit(string[] command, int parse0)
        {
            string compressedCommand = "";
            for (int k = 0; k < command.Length; k++)
                if (k == command.Length - 1)
                    compressedCommand += command[k];
                else
                    compressedCommand += command[k] + delimiter;

            Send(compressedCommand);
        }
    }
}