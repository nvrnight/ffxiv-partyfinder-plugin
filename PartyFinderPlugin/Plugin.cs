using Dalamud.Game.Command;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.UI;
using PartyFinderPlugin.Windows;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace PartyFinderPlugin;

public sealed class Plugin : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static IDataManager DataManager { get; private set; } = null!;
    [PluginService] internal static IPartyFinderGui PartyFinder { get; private set; } = null!;
    [PluginService] internal static IChatGui Chat { get; private set; } = null!;
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;

    private const string CommandName = "/pf";
    private readonly Configuration _configuration = null!;

    private readonly WindowSystem _windowSystem = new("PartyFinderPlugin");
    private MainWindow MainWindow { get; init; }

    private readonly Lock _timerLock = new Lock();
    private Timer? _timer;

    public Plugin()
    {
        _configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        _configuration.LoadEncounterIds();

        MainWindow = new MainWindow(this._configuration);

        _windowSystem.AddWindow(MainWindow);
        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Open Party Finder Plugin"
        });
        
        PartyFinder.ReceiveListing += (listing, listingEventArgs) =>
        {
            if (!_configuration.On)
                return;

            var description = listing.Description.TextValue;

            if (!string.IsNullOrEmpty(description) && Regex.IsMatch(description, _configuration.SearchExpression, RegexOptions.IgnoreCase))
            {
                var dutyId = listing.Duty.Value.RowId.ToString();

                if (_configuration.EncounterIds.Contains(dutyId))
                {
                    var name = listing.Duty.Value.Name.ToString();

                    var builder = new SeStringBuilder();

                    builder
                        .Add(new PartyFinderPayload(listing.Id, PartyFinderPayload.PartyFinderLinkType.NotSpecified));

                    if (!string.IsNullOrEmpty(name))
                    {
                        builder.AddText(name + " - ");
                    }

                    builder
                        .AddText(description)
                        .Add(RawPayload.LinkTerminator);

                    Chat.Print(builder.Build());

                    lock (_timerLock)
                    {
                        _timer?.Dispose();
                        _timer = new Timer(ProcessNotification, null, TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
                    }
                }
            }
        };

        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
    }
    
    private void ProcessNotification(object? state)
    {
        lock (_timerLock)
        {
            UIGlobals.PlayChatSoundEffect(1);

            _timer?.Dispose();
            _timer = null;
        }
    }

    public void Dispose()
    {
        _windowSystem.RemoveAllWindows();

        MainWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args)
    {
        if(args == "on")
        {
            _configuration.On = true;
            _configuration.Save();
            Chat.Print("PF Plugin On");
            return;
        }
        
        if(args == "off")
        {
            _configuration.On = false;
            _configuration.Save();
            Chat.Print("PF Plugin Off");
            return;
        }

        // in response to the slash command, just toggle the display status of our main ui
        ToggleMainUI();
    }

    private void DrawUI() => _windowSystem.Draw();
    public void ToggleMainUI() => MainWindow.Toggle();
}
