using Spectrum.API.Interfaces.Plugins;
using Spectrum.API.Interfaces.Systems;
using Spectrum.API.Configuration;
using Spectrum.Interop.Game;
using System;

namespace Spectrum.Plugins.Heat
{
    public enum Units { kph, mph };
    public enum Display { watermark, hud, car };
    public enum Activation { always, warning, toggle };
    public class Entry : IPlugin, IUpdatable
    {
        private bool toggled = false;
        private double warningThreshold;
        private Units units;
        private Display display;
        private Activation activation;
        private Settings _settings;

        public void Initialize(IManager manager, string ipcIdentifier)
        {
            _settings = new Settings("Heat.plugin");
            ValidateSettings();
            units = (Units)Enum.Parse(typeof(Units), _settings.GetItem<string>("units"));
            display = (Display)Enum.Parse(typeof(Display), _settings.GetItem<string>("display"));
            activation = (Activation)Enum.Parse(typeof(Activation), _settings.GetItem<string>("activation"));
            warningThreshold = _settings.GetItem<double>("warningThreshold");
            manager.Hotkeys.Bind(_settings.GetItem<string>("toggleHotkey"), () => { toggled = !toggled; Game.WatermarkText = ""; });
        }

        public void Update()
        {
            if (DisplayEnabled())
            {
                if (display == Display.hud)
                    SetHUDText(DisplayText());
                else
                    Game.WatermarkText = DisplayText();
            }

        }
        private string HeatPercent()
        {
            return Convert.ToInt32(GetHeatLevel() * 100).ToString() + "% Heat";
        }
        private string Speed()
        {
            if (units == Units.kph)
                return Convert.ToInt32(GetVelocityKPH()).ToString() + "KPH";

            return Convert.ToInt32(GetVelocityMPH()).ToString() + "MPH";
        }
        private string DisplayText()
        {
            return HeatPercent() + "\n" + Speed();
        }
        private bool DisplayEnabled()
        {
            try
            {
                return (activation == Activation.always) ||
                       (activation == Activation.warning && GetHeatLevel() > warningThreshold) ||
                       (activation == Activation.toggle && toggled);
            }
            catch
            {
                return false;
            }
        }

        private void ValidateSettings()
        {
            if (!_settings.ContainsKey("toggleHotkey"))
                _settings.Add("toggleHotkey", "LeftControl+H");

            if (!_settings.ContainsKey("units") || !Enum.IsDefined(typeof(Units), _settings["units"]))
                _settings.Add("units", "kph");

            if (!_settings.ContainsKey("display") || !Enum.IsDefined(typeof(Display), _settings["display"]))
                _settings.Add("display", "watermark");

            if (!_settings.ContainsKey("activation") || !Enum.IsDefined(typeof(Activation), _settings["activation"]))
                _settings.Add("activation", "always");

            if (!_settings.ContainsKey("warningThreshold"))
                _settings.Add("warningThreshold", 0.80);

            _settings.Save();
        }

        // Utilities, taken from Spectrum's LocalVehicle
        private static CarLogic GetCarLogic()
        {
            var carLogic = G.Sys.PlayerManager_?.Current_?.playerData_?.Car_?.GetComponent<CarLogic>();
            if (carLogic == null)
                carLogic = G.Sys.PlayerManager_?.Current_?.playerData_?.CarLogic_;
            return carLogic;
        }
        private static HoverScreenEmitter GetHoverScreenEmitter()
        {
            return G.Sys.PlayerManager_?.Current_?.playerData_?.Car_?.GetComponent<HoverScreenEmitter>();
        }
        private static float GetHeatLevel()
        {
            var carLogic = GetCarLogic();
            if (carLogic)
                return carLogic.Heat_;
            return 0f;
        }
        private static float GetVelocityKPH()
        {
            var carLogic = GetCarLogic();
            if (carLogic)
                return carLogic.CarStats_.GetKilometersPerHour();
            return 0f;
        }
        private static float GetVelocityMPH()
        {
            var carLogic = GetCarLogic();
            if (carLogic)
                return carLogic.CarStats_.GetMilesPerHour();
            return 0f;
        }
        private static void SetHUDText(string text)
        {
            var hoverScreen = GetHoverScreenEmitter();
            if (hoverScreen)
                hoverScreen.SetTrickText(new TrickyTextLogic.TrickText(3.0f, -1, TrickyTextLogic.TrickText.TextType.standard, text));
        }
    }
}