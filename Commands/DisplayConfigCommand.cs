﻿using LethalAPI.LibTerminal.Attributes;
using LethalAPI.LibTerminal.Interactions;
using LethalAPI.LibTerminal.Interfaces;
using RandomMoons.ConfigUtils;
using RandomMoons.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomMoons.Commands
{
    /// <summary>
    /// This command is  used to display the current user's configuration of the mod. This is mainly used to debug, and for conveniance purposes when not using
    /// Lethal config or Lethal settings
    /// </summary>
    /// 
    public class DisplayConfigCommand
    {
        [TerminalCommand("RmConfig"), CommandInfo("Displays RandomMoons config.")]
        public string DisplayConfigString()
        {
            ///<summary>
            ///This function aim's is to return all of the current config in a single string, to be later printed on the terminal 
            ///when the command is called
            ///</summary>

            string ConfigString;
            

            //We want to create a string that contain all of the current config, and the state of the syncing.

            //First, the decorium

            ConfigString = "\n \n \n " + "Heres all of the mod's configuration and their current status, with a brief description of each one. You can also so the config syncing status\n";

            // Then, the confing syncing status

            ConfigString = ConfigString + "Current config syncing status. this should be 'TRUE' when playing on multiplayer, and 'FALSE' when playing in solo. \n" +
                                          $" Current config status : {States.ConfigStatus}";





            return ConfigString;

        }
    }


}
