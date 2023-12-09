using BootlegEngine;
using System.Collections.Generic;
using System.ComponentModel;

public class MyViewModel : INotifyPropertyChanged
{
    private List<string> playerBanners = new();

    public MyViewModel(List<Player> players)
    {
        playerBanners.Add($"Player 1 (You) - score: {players[0].Score}");
        playerBanners.Add($"Player 2 (AI) - score: {players[1].Score}");
    }

    public string Player1
    {
        get { return playerBanners[0]; }
        set
        {
            if (playerBanners[0] != value)
            {
                playerBanners[0] = value;
                OnPropertyChanged(nameof(Player1));
            }
        }
    }

    public string Player2
    {
        get { return playerBanners[1]; }
        set
        {
            if (playerBanners[1] != value)
            {
                playerBanners[1] = value;
                OnPropertyChanged(nameof(Player2));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}