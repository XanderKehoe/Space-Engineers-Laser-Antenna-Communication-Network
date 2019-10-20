# Space-Engineers-Laser-Antenna-Communication-Network
A Space Engineers Programmable Block Script for intergrid communication using laser antennas. 

From Space Engineers Official Steam Page (https://store.steampowered.com/app/244850/Space_Engineers/): "Space Engineers is a sandbox game about engineering, construction, exploration and survival in space and on planets. Players build space ships, space stations, planetary outposts of various sizes and uses, pilot ships and travel through space to explore planets and gather resources to survive." Space Engineers features a programmable block which offers opportunities for automation or ease of life task , which the script(s) here utilize to accomplish their task. These scripts are programmed in C#.

Intergrid communication can take place with normal antennas, however in doing so they transmit their location to not just you. Other players (who may not be so friendly) and hostile AI can see the location of the antenna if they are within range. For short range intergrid communcations, this is not an issue, however at medium-to-large ranges it is. Laser antennas allow for safe intergrid communcation, at the cost of complexity.

This script allows the user to setup a laser antenna communication network, utilizing a single laser antenna at each station and connecting them to various 'hubs' throughout space, communication using laser antennas is made easy. We can either send messages to all stations (such as an alert if a station is under attack, and specifying which station it is), or just a single station (such as "Send ship x to these provided coordinates").

This script is advanced and designed for use by myself and other scripters, and may be difficult to use for non-scripters.
