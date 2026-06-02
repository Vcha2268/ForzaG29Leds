# 🏎️ ForzaG29Leds - Sync your wheel lights with Forza

[![](https://img.shields.io/badge/Download-Release_Page-blue.svg)](https://github.com/Vcha2268/ForzaG29Leds)

This application connects your Logitech G29 or G923 racing wheel to Forza Horizon 6. It reads live data from the game and activates the gear shift indicator lights on your steering wheel in real time.

## ⚙️ System Requirements

Your computer needs the following to run this software:

*   Windows 10 or Windows 11.
*   Logitech G HUB software installed and running.
*   A Logitech G29 or G923 racing wheel connected via USB.
*   Forza Horizon 6 installed on your computer.
*   The Microsoft .NET Desktop Runtime (x64).

## 📥 How to Install

1. Visit the [official release page](https://github.com/Vcha2268/ForzaG29Leds).
2. Look for the latest version under the "Releases" section.
3. Click the file ending in .zip to download it to your computer.
4. Open your Downloads folder.
5. Right-click the downloaded file and select "Extract All."
6. Choose a folder on your computer to save these files.

## 🎮 Setting Up Forza Horizon 6

Forza requires a settings change to send data to your wheel. Follow these steps:

1. Launch Forza Horizon 6.
2. Open the "Settings" menu from the main screen.
3. Navigate to the "HUD and Gameplay" tab.
4. Scroll down until you find the "Data Out" or "Telemetry" section.
5. Set the "Data Out" option to "On."
6. Ensure the IP address is set to your local machine, which is usually 127.0.0.1.
7. Set the port to 20777.
8. Save your changes.

## 🚀 Running the Application

1. Open the folder where you extracted the files.
2. Double-click the file named "ForzaG29Leds.exe."
3. If Windows shows a security prompt, click "More info" and then "Run anyway."
4. The application window will appear on your screen.
5. Start your game. 
6. Once you start driving, the application detects the game data signal automatically.
7. The LED lights on your steering wheel will light up according to your engine speed.

## 🛠️ Troubleshooting

If the lights fail to activate, check these common items:

*   **Logitech G HUB:** Ensure your wheel shows as "Connected" in the Logitech G HUB software.
*   **Game Settings:** Verify that the "Data Out" setting in the game is still set to "On."
*   **Port Conflicts:** Close other wheel-related software that might try to read the same data port.
*   **Administrator Access:** Try right-clicking the application and selecting "Run as administrator."
*   **Connection:** Ensure your wheel connects directly to a USB port on your computer rather than a USB hub.

## ℹ️ Technical Details

This software acts as a bridge between the game data and your hardware. It listens for UDP packets sent by the game engine. These packets contain information about your car, such as speed and engine RPM. The application translates these numbers into commands that the Logitech hardware understands. This creates a responsive experience that mirrors the internal mechanics of the car. Because the software uses the native telemetry output provided by the game, it does not interfere with game files or memory.

## 🛡️ Privacy and Safety

This application does not collect your data. It operates entirely on your local machine. It does not connect to external servers or send information over the internet. You do not need an account to use it. The application only accesses the USB ports to communicate with your hardware and listens to the local network port for game data. 

## 📝 Frequently Asked Questions

**Does this work with other games?**
This specific version targets the telemetry output format of Forza Horizon 6. It may not function correctly with other titles unless they use an identical data format.

**Will this cause lag in my game?**
The application consumes a tiny amount of system resources. It should not impact your frame rate or performance while driving.

**Do I need the G HUB software?**
Yes. The Logitech drivers included in G HUB are necessary for the application to send commands to the wheel's light system.

**Can I change the brightness?**
The current version uses the standard brightness profile set in your Logitech software. Check your Logitech G HUB settings if you want to modify base lighting levels.

**Is it safe to run?**
The code is open source and intended for personal use. It is a lightweight utility that does not inject code into the game process.